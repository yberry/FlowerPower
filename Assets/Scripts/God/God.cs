using UnityEngine;
using System.Collections;

public class God : MonoBehaviour {

    private static God instance;
    public static God Get
    {
        get
        {
            return instance;
        }
    }

    public Light sight;

	// Use this for initialization
	void Start () {
        if (instance == null)
        {
            instance = this;
        }
        sight.type = LightType.Spot;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool IsUnderGodView(Vector3 pos)
    {
        Vector3 direction = pos - sight.transform.position;
        Vector3 forward = sight.transform.forward;

        return Vector3.Angle(direction, forward) <= sight.spotAngle;
    }
}
