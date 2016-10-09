using UnityEngine;
using System.Collections;

public class ForDisplay : MonoBehaviour {

	public GameObject FX;
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Jump")){
			Instantiate (FX, transform.position+Vector3.down*0.5f,Quaternion.identity);
		}
	}
}
