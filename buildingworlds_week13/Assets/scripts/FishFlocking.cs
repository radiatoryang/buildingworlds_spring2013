using UnityEngine;
using System.Collections;
using System.Collections.Generic; // remember, you need this "using..." line to use Lists

public class FishFlocking : MonoBehaviour {

    // Flocking weights
    public float separation = 2f;
    public float alignment = 1f;
    public float cohesion = 3f;

    // Speeds and ranges
    public float speed = 5f;
    public float range = 100f;
    public float neighborRange = 50f;

    // used to pass calculations from Update() to FixedUpdate()
    Vector3 separationForce;
    Vector3 alignmentForce;
    Vector3 cohesionForce;



    List<FishFlocking> flock = new List<FishFlocking>(); // see note in Start()

    void Start () {
        // This is a really lazy, expensive, bad way of grabbing all fish in the scene!
        // for example, if I instantiated new fish during the simulation, only subsequent fish would know about them.
        // ... for best results, you'd have a FishManager object that keeps a List of all fish, control fish, etc.
        // and this object would also instantiate the fish and such.
        flock.AddRange( GameObject.FindObjectsOfType( typeof( FishFlocking ) ) as FishFlocking[] );
	}
	
	// Update is called once per frame
	void Update () {
        // GET NEIGHBORS: if a fish is within "neighborRange" then it's my neighbor
        List<FishFlocking> neighbors = new List<FishFlocking>();
        foreach (FishFlocking fish in flock) {
            if (Vector3.Distance(fish.transform.position, transform.position) < neighborRange)
                neighbors.Add(fish);
        }


        // SEPARATION: accelerate away from my neighbors
        Vector3 averageDirectionToNeighbors = Vector3.zero;
        foreach ( FishFlocking fish in neighbors ) {
            averageDirectionToNeighbors += ( transform.position - fish.transform.position ).normalized;
        }
        separationForce = averageDirectionToNeighbors / neighbors.Count;


        // ALIGNMENT: accelerate toward where my neighbors' AVERAGE facing
        Vector3 averageFacing = Vector3.zero;
        foreach ( FishFlocking neighbor in neighbors ) {
            averageFacing += neighbor.transform.forward;
        }
        alignmentForce = averageFacing / neighbors.Count;



        // COHESION: accelerate toward AVERAGE center of my neighbors
        Vector3 averageCenter = Vector3.zero;
        foreach ( FishFlocking fish in neighbors ) {
            averageCenter += fish.transform.position;
        }
        // these two lines below ARE THE SAME
        // averageCenter /= neighbors.Count;
        averageCenter = averageCenter / neighbors.Count;
        cohesionForce = ( averageCenter - transform.position ).normalized;



        // FACING:
        // Quaternion.LookRotation takes any Vector3 forward vector convert to crazy Quaternion format we need
        transform.rotation = Quaternion.LookRotation( rigidbody.velocity );


        // NOTHING TO SEE HERE, FISHIE: go back to where you were
        if ( transform.position.magnitude > range ) {
            // HACK: we really shouldn't AddForce outside of FixedUpdate()
            rigidbody.AddForce( -transform.position.normalized * speed * 5f, ForceMode.Force );
        }
        
	}

    // fixedupdate for physics
    void FixedUpdate() {
        // SEPARATION: accelerate AVERAGE away from my neighbors
        rigidbody.AddForce( separationForce * separation * speed, ForceMode.Acceleration );

        // ALIGNMENT: accelerate toward my neighbors' AVERAGE facing
        rigidbody.AddForce( alignmentForce * alignment * speed, ForceMode.Acceleration );
    
        // COHESION: accelerate toward AVERAGE center of my neighbors
        rigidbody.AddForce( cohesionForce * cohesion * speed, ForceMode.Acceleration );
    }

}
