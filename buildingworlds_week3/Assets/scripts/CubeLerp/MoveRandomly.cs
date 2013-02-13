using UnityEngine;
using System.Collections;

public class MoveRandomly : MonoBehaviour {
	
	Vector3 moveTarget; // I omit the "public" keyword, so this variable is now private, because only MoveRandomly will need to know it
	
	const float speed = 15f; // "const" keyword means it is a CONSTANT, which means it will never change values
	const float range = 10f; // ... you do that if you want it to be easily configurable / tweakable, but edit it in code and not the inspector
							 // ... there is also little / no performance cost to this, the number gets directly "baked" into the code
	
	// Use this for initialization
	void Start () {
		// every 3 seconds, choose a new moveTarget
		InvokeRepeating("NewMoveTarget", 1f, 3f);
	}
	
	// Update is called once per frame
	void Update () {
		// You can also put Lerp (linear interpolate) in Update... it's pretty lazy, though
		transform.position = Vector3.Lerp(transform.position, moveTarget, Time.deltaTime * speed);
	}
	
	void NewMoveTarget () {
		// Random.onUnitSphere generates a random direction on the surface of a "unit sphere" (sphere with radius of 1)
		// it's very good for randomizing directions!
		// We also randomize the range / depth...
		moveTarget = Random.onUnitSphere * Random.Range(0f, range);
	}
}
