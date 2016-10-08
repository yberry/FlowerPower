using UnityEngine;

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

    private bool underGod = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        underGod = God.Get.IsUnderGodView(transform.position);
	}

    public void GrabFlower()
    {
        if (!complete)
        {
            nbFlowers++;
        }
    }
}
