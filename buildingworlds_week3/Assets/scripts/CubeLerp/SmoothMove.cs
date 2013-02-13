using UnityEngine;
using System.Collections;

public class SmoothMove : MonoBehaviour {
	
	public Vector3 target = new Vector3(10f, 1f, 10f);
	public float timeToReachTarget = 5f;
	
	public Transform sphere; // assigned in inspector
	public Light cubeLight; // will find and assign the reference automatically in Start()

	// Use this for initialization
	void Start () {
		// after waiting for 2 seconds, execute the "StartMoving" method every 6 seconds
		InvokeRepeating("StartMoving", 2f, 6f);
		
		// populate "cubeLight" variable with a Component of type "Light" on the child GameObject called "Point light"
		cubeLight = transform.Find("Point light").GetComponent<Light>(); 
	}
	
	// this gets Invoked in Start() above
	void StartMoving () { 
		// Begin the IEnumerator called "StartMove", and pass-in our Transform and time variables to configure it
		StartCoroutine( StartMove(sphere, timeToReachTarget) );
	}
	
	IEnumerator StartMove (Transform destination, float duration) {
		float t = 0f; // initialize timer at 0
		Vector3 start = transform.position; // set the "start" of our linear (line) interpolation to our current position
		
		while (t < 1f) { // while the timer is less than 1.0, keep doing this loop:
			t += Time.deltaTime / duration; // increment counter by a fraction of duration
			
			cubeLight.intensity = t * 8f; // increase intensity of light from 0 to 8
			transform.position = Vector3.Lerp(start, destination.position, t); // set cube position to a point (t = 0.0-1.0, or 0%-100%)
																			   // sampled along a line from coordinates "start" and "destination"
			
			// if you want to know WHY we must wait one frame, try commenting out the line below; you'll see the cube "teleport"...
			// ... because everything is happening in a single frame unless we tell it to wait otherwise!
			yield return 0; // wait one frame
		}
		
	}
}
