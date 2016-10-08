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

    private GameObject flower;

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
        flower = Instantiate(prefab, transform.position, Quaternion.identity) as GameObject;
    }
}
