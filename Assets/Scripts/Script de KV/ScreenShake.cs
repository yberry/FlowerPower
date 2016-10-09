using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour {


	[Header("Amplitude du Shake")]
	public float Hor_Amplitude = 5;
	public float Ver_Amplitude = 5;
	[Header("Durée du Shake")]
	public float Shake_Time = 10;

	private Vector3 StartPos;
	float timer = 0;
	bool shake = false;



	// Use this for initialization
	void Start () {
		StartPos = transform.position;	
	}
	
	// Update is called once per frame
	void Update () {
		if (shake && timer > 0) {
			Vector3 shake_pos = new Vector3 (StartPos.x + Random.Range (-Hor_Amplitude, Hor_Amplitude),
				StartPos.y + Random.Range (-Ver_Amplitude, Ver_Amplitude),
				StartPos.z);
				transform.position = shake_pos;
			timer--;
		} else if (shake) {
				shake = false;
				transform.position = StartPos;
			}
		if (Input.GetKeyDown ("space")) {
			Shake ();
		}
	}

	public void Shake (){
		shake = true;
		timer = Shake_Time;
	}
}
