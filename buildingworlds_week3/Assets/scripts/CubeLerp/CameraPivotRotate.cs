using UnityEngine;
using System.Collections;

public class CameraPivotRotate : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		// rotate 15 degrees / second, around the Y axis 
		transform.Rotate(new Vector3(0f, 15f * Time.deltaTime, 0f) );
	}
}
