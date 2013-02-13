using UnityEngine;
using System.Collections;

public class TriggerDemo : MonoBehaviour {
	
	// when a collider with a rigidbody enters this trigger
	void OnTriggerEnter (Collider activator) {
		if (activator.tag == "IHaveALight") { // is the collider tagged with the tag "IHaveALight"?...
			if (activator.light.enabled == false) // is the ball turned off?
				activator.light.enabled = true; // then turn it on!
		}
	}
}
