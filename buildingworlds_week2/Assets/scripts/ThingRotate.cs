using UnityEngine;
using System.Collections;

public class ThingRotate : MonoBehaviour {
	
	public float rotateSpeed = 15f;
	public float moveSpeed = 10f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// rotate 15 degrees around the Y axis *over* every second
		transform.Rotate(new Vector3 (0f, rotateSpeed, 0f) * Time.deltaTime);
		
		// move thing 5 units, over every second, FORWARD relative to its local rotation axes
		transform.position += transform.forward * Time.deltaTime * moveSpeed;
		
		// print the value of transform.forward, every frame, in the console
		// Debug.Log(transform.forward);
	}
}
