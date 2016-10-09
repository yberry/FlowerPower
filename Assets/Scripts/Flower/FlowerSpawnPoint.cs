using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FlowerSpawnPoint : MonoBehaviour {

    private bool haveFlower = false;
    public bool HaveFlower
    {
        get
        {
            return haveFlower;
        }
    }

    private AudioSource source;

    void Start()
    {
        Renderer r = GetComponent<Renderer>();
        if (r != null)
        {
            r.enabled = false;
        }
        source = GetComponent<AudioSource>();
    }

    public void Spawn(GameObject prefab, AudioClip clip)
    {
        haveFlower = true;
        GameObject flower = Instantiate(prefab, transform.position, Quaternion.identity) as GameObject;
        flower.GetComponent<Flower>().SetPoint(this);
        source.PlayOneShot(clip);
    }

    public void Free()
    {
        haveFlower = false;
    }
}
