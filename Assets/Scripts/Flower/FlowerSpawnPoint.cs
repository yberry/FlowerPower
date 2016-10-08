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

    public void Spawn(GameObject prefab)
    {
        haveFlower = true;
        flower = Instantiate(prefab) as GameObject;
        flower.transform.SetParent(transform);
        flower.transform.localPosition = Vector3.zero;
    }
}
