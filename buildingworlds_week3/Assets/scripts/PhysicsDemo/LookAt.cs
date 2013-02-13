using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour {
	
	public Transform lookAtThis; // assigned in Inspector
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(lookAtThis);
	}
}
