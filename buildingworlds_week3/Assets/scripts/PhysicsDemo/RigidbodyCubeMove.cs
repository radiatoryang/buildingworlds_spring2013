using UnityEngine;
using System.Collections;

public class RigidbodyCubeMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// FixedUpdate is called once every now and then
	void FixedUpdate () {
		
		// Add a directional force to this rigidbody when player presses "W" on keyboard
		if ( Input.GetKey(KeyCode.W) )
			GetComponent<Rigidbody>().AddForce(transform.forward * 400f, ForceMode.Acceleration);
		
		// if player's mouse (x-axis) is non-zero, then apply torque and spin the cube around
		if (Mathf.Abs( Input.GetAxis("Mouse X") ) > 0f )
			rigidbody.AddTorque(new Vector3(0f, 1500f, 0f) * Input.GetAxis("Mouse X"));
		
		// if player's mouse (y-axis) is non-zero, then apply torque in a different axis
		if (Mathf.Abs( Input.GetAxis("Mouse Y") ) > 0f )
			rigidbody.AddTorque(new Vector3(0f, 0f, 250f) * Input.GetAxis("Mouse Y"));

	}
}
