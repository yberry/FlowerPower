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

    private Player owner;

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

    private bool godAttracted = false;

    [Tooltip("Vitesse de chute de la fleur")]
    public float fallSpeed = 1f;
    [Tooltip("Vitesse de lancer de la fleur vers Dieu")]
    public float lauchSpeed = 1f;

	// Use this for initialization
	void Start () {
        ren = GetComponent<Renderer>();
        col = GetComponent<Collider2D>();
        rig = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (!godAttracted)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, God.Get.transform.position, lauchSpeed * Time.deltaTime);
        transform.rotation = Quaternion.AngleAxis(lauchSpeed * Time.deltaTime, Vector3.forward);
	}

    public void SetPoint(FlowerSpawnPoint p)
    {
        point = p;
    }

    public bool IsOwner(Player player)
    {
        return owner == player;
    }

    public void Grab(Player player)
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
        owner = player;
        godAttracted = false;
        ren.enabled = false;
        col.enabled = false;
        grabable = false;
        rig.gravityScale = 0f;
    }

    public void Throw(bool underGod)
    {
        transform.SetParent(null);
        ren.enabled = true;
        grabable = !underGod;
        rig.gravityScale = 1f;
        owner = null;

        float angle = Random.Range(Mathf.PI / 6f, 5f * Mathf.PI / 6f);
        Vector2 velocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        rig.velocity = velocity;
    }

    public void Launch()
    {
        grabable = true;
        godAttracted = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "God")
        {
            owner.AddKarma();
            Destroy(gameObject);
        }
    }
}
