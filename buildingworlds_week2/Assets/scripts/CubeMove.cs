using UnityEngine;
using System.Collections;

public class CubeMove : MonoBehaviour {
	
	public float speed = -10f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// move cube up 1 unit (* speed) over 1 second
		// transform.position += new Vector3(0f, 0f, 1f) * Time.deltaTime * speed; // 0.1f (10 FPS)... (2000 FPS) 0.002f
		
		// move cube FORWARD, with respect to rotation, over 1 second
		transform.position += transform.forward * Time.deltaTime * speed; // 0.1f (10 FPS)... (2000 FPS) 0.002f
		
		// makes it bounce up and down
		// transform.position += new Vector3(0f, Mathf.Sin(Time.time * speed), 0f) * Time.deltaTime;
		
		// make it spin
		transform.Rotate(new Vector3(0f, 1f, 0f) * Time.deltaTime * speed);
		
		// let's make it grow over time
		//transform.localScale += new Vector3 (0f, 1f, 0f) * Time.deltaTime;
		
	}
}
