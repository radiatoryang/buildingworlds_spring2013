using UnityEngine;
using System.Collections;

public class CameraBounce : MonoBehaviour {
	
	public float speed = 2f;
	public float distance = 0.1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// move the thing up 2 units along the Y axis *over* every second
		// transform.position += new Vector3 (0f, 2f, 0f) * Time.deltaTime;
		
		// make the thing bounce according to a sine wave
		transform.position += transform.forward * Mathf.Sin(Time.time * speed) * Time.deltaTime * distance;
	}
	
	
	
}
