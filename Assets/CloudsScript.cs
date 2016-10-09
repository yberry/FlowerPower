using UnityEngine;
using System.Collections;

public class CloudsScript : MonoBehaviour {
	//Yes i'm 2 lazy to make an animation


	public int Xmin = -20;
	public int Xmax = 20;
	public float speed = 0.2f;


	// Update is called once per frame
	void FixedUpdate () {
		if (transform.position.x < Xmin) {
			transform.position = new Vector3 (Xmax,
				transform.position.y,
				transform.position.z);
		} else {
			transform.position += Vector3.left * speed;
		}
	}
}
