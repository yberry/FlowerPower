using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Flower : MonoBehaviour {

    private FlowerSpawnPoint point;
    public bool IsFixed
    {
        get
        {
            return point != null;
        }
    }

    private Renderer ren;
    private Collider2D col;
    private Rigidbody2D rig;

    private bool grabable = true;
    public bool Grabable
    {
        get
        {
            return grabable;
        }
    }

	// Use this for initialization
	void Start () {
        ren = GetComponent<Renderer>();
        col = GetComponent<Collider2D>();
        rig = GetComponent<Rigidbody2D>();
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
        if (!grabable)
        {
            return;
        }
        if (IsFixed)
        {
            point.Free();
            point = null;
        }
        ren.enabled = false;
        col.enabled = false;
        grabable = false;
    }

    public void Throw(bool underGod)
    {
        transform.SetParent(null);
        ren.enabled = true;
        grabable = !underGod;

        float angle = Random.Range(Mathf.PI / 6f, 5f * Mathf.PI / 6f);
        Vector2 velocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        rig.velocity = velocity;
    }

    public void Launch()
    {

    }
}
