using UnityEngine;
using System.Collections;

public class ThingScale : MonoBehaviour {
	
	public float scaleSpeed = 1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// increase the Y scale (height) by 1 *over* every second
		// transform.localScale += new Vector3 (0f, 1f, 0f) * Time.deltaTime;
		
		// increase the Y scale (height) by 1 *over* every second (same thing as above, just in different way)
		transform.localScale = new Vector3 (transform.localScale.x, 
											transform.localScale.y + (scaleSpeed * Time.deltaTime), 
											transform.localScale.z);
	}
}
