using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Renderer))]
public class Flower : MonoBehaviour {

    private FlowerSpawnPoint point;
    private Renderer ren;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetPoint(FlowerSpawnPoint p)
    {
        point = p;
    }

    public void Grab()
    {
        if (point != null)
        {
            point.Free();
            point = null;
            ren.enabled = false;
        }
    }
}
