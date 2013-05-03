using UnityEngine;
using System.Collections;

public class Fish : MonoBehaviour {

    Vector3 destination;
    public float speed = 5f;
    public float stoppingDistance = 1f;

	// Use this for initialization
	void Start () {
        SetNewDestination();
	}
	
	// Update is called once per frame
	void Update () {
        // Quaternion.LookRotation takes any Vector3 forward vector convert to crazy Quaternion format we need
        transform.rotation = Quaternion.LookRotation(rigidbody.velocity);
	}

    // fixedupdate for physics
    void FixedUpdate() {
        // apply a physics force in the direction of our destination
        rigidbody.AddForce( ( destination - transform.position ).normalized * Time.fixedDeltaTime * speed, ForceMode.VelocityChange );
    
        // if fish is near our current destination, then set a new destination
        if ( Vector3.Distance( transform.position, destination ) < stoppingDistance ) {
            SetNewDestination();
        }
    }



    void SetNewDestination() {
        // set "destination" to a random point inside an imaginary sphere of radius ____
        SetNewDestination(10f);
    }

    void SetNewDestination( float range ) {
        SetNewDestination( Random.insideUnitSphere * range );
    }

    void SetNewDestination(Vector3 newDestination) {
        destination = newDestination;
	
    }
}
