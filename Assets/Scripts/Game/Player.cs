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

    private float coolDownAttacked;

    public bool attacked { get; set; }
    public bool attacking { get; set; }
    public bool launching { get; set; }

    [Tooltip("Joueur 1 ?")]
    public bool player1;
    [Tooltip("Affichage des coeurs")]
    public Inventory inventory;
    [Tooltip("Affichage du karma")]
    public Karma karma;

	// Use this for initialization
	void Start () {
        attacked = false;
        attacking = false;
        launching = false;

        animator = GetComponent<Animator>();
        RuntimeAnimatorController ac = animator.runtimeAnimatorController;
        foreach (AnimationClip clip in ac.animationClips)
        {
            if (clip.name == "P" + (player1 ? "1" : "2") + "_hurt")
            {
                coolDownAttacked = clip.length;
            }
        }

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

        if (Input.GetButtonDown("Launch") && underGod && !animator.GetBool("inAir"))
        {
            animator.SetTrigger("offer");
            LaunchFlower();
        }

        else if (Input.GetButtonDown("Attack"))
        {
            Attack();
        }
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Player" && col.transform.GetComponent<Player>().attacking)
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
            animator.SetTrigger("attack");
            SoundEffectController.Instance.MakeAttackSound();
            attacking = true;
        }
    }

    void Attacked(bool init)
    {
        animator.SetTrigger("hurt");
        attacked = true;
        foreach (Flower flower in flowers)
        {
            flower.Throw(init);
        }
        flowers.Clear();
        inventory.SetNbFlowers(0);
        SoundEffectController.Instance.MakeHurtSound();
        if (!animator.GetBool("inAir"))
        {
            StartCoroutine(CoolDownAttacked());
        }
    }

    IEnumerator CoolDownAttacked()
    {
        yield return new WaitForSeconds(coolDownAttacked);
        attacked = false;
        launching = false;
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
