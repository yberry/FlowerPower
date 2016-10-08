using UnityEngine;

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
	    //Définir mouvement
	}

    public bool IsUnderGodView(Vector3 pos)
    {
        Vector3 direction = pos - sight.transform.position;
        Vector3 forward = sight.transform.forward;

        if (Vector3.Angle(direction, forward) > sight.spotAngle)
        {
            return false;
        }

        RaycastHit hit;
        if (Physics.Raycast(sight.transform.position, direction, out hit))
        {
            return hit.transform.tag != "platform";
        }
        else
        {
            return true;
        }
    }
}
