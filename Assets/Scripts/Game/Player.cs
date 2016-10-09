using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

    SoundEffectController sounds;

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

    [Tooltip("Joueur 1 ?")]
    public bool player1;

	// Use this for initialization
	void Start () {
        attacked = false;
        attacking = false;

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

        if (Input.GetButtonDown("Launch") && underGod)
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
            GrabFlower(col.GetComponent<Flower>());
        }
    }

    void GrabFlower(Flower flower)
    {
        if (complete)
        {
            return;
        }
        flower.Grab();
        flowers.Add(flower);
        flower.transform.SetParent(transform);
        flower.transform.localPosition = Vector3.zero;
        sounds.MakePickFlowerSound();
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
            sounds.MakeAttackSound();
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
        sounds.MakeHurtSound();
        if (!animator.GetBool("inAir"))
        {
            StartCoroutine(CoolDownAttacked());
        }
    }

    IEnumerator CoolDownAttacked()
    {
        yield return new WaitForSeconds(coolDownAttacked);
        attacked = false;
    }

    void LaunchFlower()
    {
        if (flowers.Count == 0)
        {
            return;
        }
        flowers[0].Launch();
        flowers.RemoveAt(0);
    }
}
