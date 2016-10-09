using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

    private Animator animator;

    private const int maxFlowers = 5;

    private List<Flower> flowers;
    public bool complete
    {
        get
        {
            return flowers.Count == maxFlowers;
        }
    }

    private bool underGod
    {
        get
        {
            return God.Get.IsUnderGodView(transform.position);
        }
    }

    private const float coolDownAttacked = 0.5f;

    public bool attacked { get; set; }
    public bool attacking { get; set; }
    public bool launching { get; set; }

    [Tooltip("Joueur 1 ?")]
    public bool player1;
    [Tooltip("Affichage des coeurs")]
    public Inventory inventory;
    [Tooltip("Affichage du karma")]
    public Karma karma;

    public string[] input;
    PlayerController player;

	// Use this for initialization
	void Start () {
        attacked = false;
        attacking = false;
        launching = false;

        animator = GetComponent<Animator>();
        player = GetComponent<PlayerController>();
        flowers = new List<Flower>();

        Renderer ren = GetComponent<Renderer>();
        ren.shadowCastingMode = ShadowCastingMode.On;
        ren.receiveShadows = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (attacked || attacking || flowers.Count == 0)
        {
            return;
        }

        if (Input.GetButtonDown(input[1]) && underGod && player.onGround)
        {
            if(tag == "Player")
            {
                animator.Play("p1_offrande");
            }
            if(tag == "Player2")
            {
                animator.Play("P2_offrande");
            }
            LaunchFlower();
        }
        else if (Input.GetButtonDown(input[0]))
        {
            Attack();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag.StartsWith("Player") && col.transform.GetComponent<Player>().attacking)
        {
            Attacked(false);
            attacking = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Flower" && !attacked)
        {
            Flower flower = col.GetComponent<Flower>();
            if (flower.Grabable && !flower.IsOwner(this))
            {
                GrabFlower(flower);
            }
        }
    }

    void GrabFlower(Flower flower)
    {
        if (complete)
        {
            return;
        }
        flower.Grab(this);
        flowers.Add(flower);
        flower.transform.SetParent(transform);
        flower.transform.localPosition = Vector3.zero;
        inventory.SetNbFlowers(flowers.Count);
        SoundEffectController.Instance.MakePickFlowerSound();
    }

    void Attack()
    {

        if (underGod)
        {
            Attacked(true);
        }
        else
        {
            if(tag == "Player")
            {
                if(player.onGround)
                    animator.Play("P1_attaque_sol");
                else
                    animator.Play("P1_attaque_air");
            }
            if (tag == "Player2")
            {
                if (player.onGround)
                    animator.Play("P2_attaque_sol");
                else
                    animator.Play("P2_attaque_air");
            }
            SoundEffectController.Instance.MakeAttackSound();
            attacking = true;
            StartCoroutine(CoolDownAttacked());
        }
    }

    void Attacked(bool init)
    {
        if(tag == "Player")
        {
            animator.Play("P1_hurt");
        }
        if (tag == "Player2")
        {
            animator.Play("P2_hurt");
        }
        attacked = true;
        foreach (Flower flower in flowers)
        {
            flower.Throw(init);
        }
        if (init)
        {
            StartCoroutine(God.Get.Angry());
        }
        flowers.Clear();
        //inventory.SetNbFlowers(0);
        SoundEffectController.Instance.MakeHurtSound();
        if (player.onGround)
        {
            StartCoroutine(CoolDownAttacked());
        }
    }

    IEnumerator CoolDownAttacked()
    {
        yield return new WaitForSeconds(coolDownAttacked);
        attacked = false;
        launching = false;
        attacking = false;
    }

    void LaunchFlower()
    {
        if (flowers.Count == 0)
        {
            return;
        }
        flowers[0].Launch();
        flowers.RemoveAt(0);
        inventory.SetNbFlowers(flowers.Count);
        StartCoroutine(CoolDownAttacked());
    }

    public void AddKarma()
    {
        karma.AddHeart();
    }
}
