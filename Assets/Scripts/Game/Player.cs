using UnityEngine;
using UnityEngine.Rendering;
using System.Collections.Generic;

public class Player : MonoBehaviour {

    SoundEffectController sounds;

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
    private bool attacked = false;
    private bool attacking = false;    

	// Use this for initialization
	void Start () {
        flowers = new List<Flower>();

        Renderer ren = GetComponent<Renderer>();
        ren.shadowCastingMode = ShadowCastingMode.On;
        ren.receiveShadows = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (attacked || attacking)
        {
            return;
        }

        if (Input.GetButtonDown("Launch") && underGod && flowers.Count > 0)
        {
            LaunchFlower();
        }

        else if (Input.GetButtonDown("Attack") && flowers.Count > 0)
        {
            attacking = true;
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
        sounds.MakeAttackSound();
        if (underGod)
        {
            Attacked(true);
        }
        else
        {
            attacking = true;
        }
    }

    void Attacked(bool init)
    {
        attacked = true;
        foreach (Flower flower in flowers)
        {
            flower.Throw(init);
        }
        flowers.Clear();
        sounds.MakeHurtSound();
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
