using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    private const int maxFlowers = 5;

    private int nbFlowers = 0;
    public bool complete
    {
        get
        {
            return nbFlowers == maxFlowers;
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GrabFlower()
    {
        if (!complete)
        {
            nbFlowers++;
        }
    }
}
