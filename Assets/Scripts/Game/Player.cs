using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour {

    private const int maxFlowers = 5;

    private List<Flower> flowers;
    public bool complete
    {
        get
        {
            return flowers.Count == maxFlowers;
        }
    }

    private bool underGod = false;

	// Use this for initialization
	void Start () {
        flowers = new List<Flower>();
	}
	
	// Update is called once per frame
	void Update () {
        underGod = God.Get.IsUnderGodView(transform.position);
	}

    public void GrabFlower(Flower flower)
    {
        if (complete)
        {
            return;
        }
        flowers.Add(flower);
        flower.transform.SetParent(transform);
        flower.transform.localPosition = Vector3.zero;
    }
}
