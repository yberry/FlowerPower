using UnityEngine;

public class CreateMenu : MonoBehaviour {

    public GameObject prefabMenu;

	// Use this for initialization
	void Start () {
        GameObject obj = Instantiate(prefabMenu) as GameObject;
	}
}
