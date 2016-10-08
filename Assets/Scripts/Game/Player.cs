using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour {

    private const int maxFlowers = 5;

    private List<Flower> triggers;
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
    private bool grabing = false;
    private bool attacked = false;
    private bool attacking = false;    

	// Use this for initialization
	void Start () {
        triggers = new List<Flower>();
        flowers = new List<Flower>();
	}
	
	// Update is called once per frame
	void Update () {
        if (grabing || attacked || attacking)
        {
            return;
        }

        /*if (Input.GetButtonDown("Grab"))
        {
            grabing = true;
            if (underGod && flowers.Count > 0)
            {
                LaunchFlower();
            }
            else
            {
                foreach (Flower flower in triggers)
                {
                    GrabFlower(flower);
                }
                triggers.Clear();
            }
        }

        else if (Input.GetButtonDown("Attack") && flowers.Count > 0)
        {
            attacking = true;
            Attack();
        }*/
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Player" && col.transform.GetComponent<Player>().attacking)
        {
            if (!attacking)
            {
                attacked = false;
            }
            attacking = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Flower")
        {
            Flower flower = col.GetComponent<Flower>();
            if (flower.IsFixed)
            {
                triggers.Add(flower);
            }
            else
            {
                GrabFlower(flower);
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Flower")
        {
            Flower flower = col.GetComponent<Flower>();
            if (flower.IsFixed)
            {
                triggers.Remove(flower);
            }
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
    }

    void Attack()
    {
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
