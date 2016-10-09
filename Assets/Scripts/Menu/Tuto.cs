using UnityEngine;
using UnityEngine.SceneManagement;

public class Tuto : MonoBehaviour {

    public Object scene;
	
	// Update is called once per frame
	void Update () {
	    if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(scene.name);
        }
	}
}
