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

    private float time = 0f;
    private bool block = false;
    private Quaternion minRotation;
    private Quaternion maxRotation;

    [Tooltip("Lumière divine")]
    public Light sight;
    [Tooltip("Position de départ")]
    public Transform startPosition;
    [Tooltip("Position de fin")]
    public Transform endPosition;
    [Tooltip("Vitesse de déplacement")]
    public float moveSpeed = 1f;
    [Tooltip("Angle minimum")]
    public float minAngle = -30f;
    [Tooltip("Angle maximum")]
    public float maxAngle = 30f;
    [Tooltip("Vitesse de rotation")]
    public float rotationSpeed = 3f;

	// Use this for initialization
	void Start () {
        if (instance == null)
        {
            instance = this;
        }
        sight.type = LightType.Spot;

        Renderer renStart = startPosition.GetComponent<Renderer>();
        if (renStart != null)
        {
            renStart.enabled = false;
        }
        Renderer renEnd = endPosition.GetComponent<Renderer>();
        if (renEnd != null)
        {
            renEnd.enabled = false;
        }

        minRotation = Quaternion.AngleAxis(minAngle, Vector3.forward);
        maxRotation = Quaternion.AngleAxis(maxAngle, Vector3.forward);

        transform.position = (startPosition.position + endPosition.position) / 2f;
        transform.rotation = Quaternion.identity;
	}
	
	// Update is called once per frame
	void Update () {
        if (block)
        {

        }
        else
        {
            float delta = Time.deltaTime;
            time += delta;
            float coefPosition = (1 + Mathf.Sin(moveSpeed * time)) / 2f;
            float coefRotation = (1 + Mathf.Sin(rotationSpeed * time)) / 2f;

            transform.position = Vector3.Lerp(startPosition.position, endPosition.position, coefPosition);
            transform.rotation = Quaternion.Lerp(minRotation, maxRotation, coefRotation);
        }
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
