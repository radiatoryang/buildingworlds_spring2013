using UnityEngine;
using System.Collections;

public class Flyer : MonoBehaviour {
	
	public float speed = 5f; // if speed is negative, then flyers will just zoom off into space forever; can you figure out why?
	Vector3 target;
	public float targetRange = 10f;
	
	// Update is called once per frame
	void Update () {
		// move towards the target by [speed] units over 1 second
		transform.position += (target - transform.position).normalized * Time.deltaTime * speed;
		
		// have we reached our destination?...
		if ( (target - transform.position).magnitude < 0.2f) {
			SetNewTarget(); // ... then set a new target
		}
	}
	
	// Sets a new target for the Flyer; this is PUBLIC which means other scripts (like CubeCloner) can call it
	public void SetNewTarget () {
		Vector3 newTarget = new Vector3 (  Random.Range(-targetRange, targetRange), 
								Random.Range(-targetRange, targetRange), 
								Random.Range(-targetRange, targetRange)   );
		SetNewTarget(newTarget);
	}
	
	// notice we have TWO definitions for SetNewTarget(), but they take different parameters; this is called "overloading"
	// Unity knows which one we mean simply by how we use it; if there's no parameter, it knows we mean the 1st SetNewTarget(), but if there is...
	
	// also, notice how the 1st SetNewTarget() calls the 2nd SetNewTarget() instead of setting "target" itself; this is considered good practice
	public void SetNewTarget (Vector3 newTarget) {
		target = newTarget;
	}
}
