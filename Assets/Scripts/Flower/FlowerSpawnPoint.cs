using UnityEngine;

public class FlowerSpawnPoint : MonoBehaviour {

    private bool haveFlower = false;
    public bool HaveFlower
    {
        get
        {
            return haveFlower;
        }
    }

    void Start()
    {
        Renderer r = GetComponent<Renderer>();
        if (r != null)
        {
            r.enabled = false;
        }
    }

    public void Spawn(GameObject prefab)
    {
        haveFlower = true;
        GameObject flower = Instantiate(prefab, transform.position, Quaternion.identity) as GameObject;
        flower.GetComponent<Flower>().SetPoint(this);
    }

    public void Free()
    {
        haveFlower = false;
    }
}
