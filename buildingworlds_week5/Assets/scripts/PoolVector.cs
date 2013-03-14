using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolVector : MonoBehaviour {
	
	public List<Vector3> trajectory = new List<Vector3>();
	
	bool aimingMode = false;
	Rigidbody ball;
	
	LineRenderer line;
	
	public Rigidbody ourBall; // assign in Inspector

	// Use this for initialization
	void Start () {
		line = GetComponent<LineRenderer>(); // cache reference to LineRenderer
	}
	
	// Update is called once per frame
	void Update () {
		// Simpler Input
//		if (Input.GetKeyDown( KeyCode.LeftArrow ) )
//			ourBall.transform.Rotate(new Vector3(0f, -15f, 0f) );
//		
//		if (Input.GetKeyDown( KeyCode.RightArrow ) )
//			ourBall.transform.Rotate(new Vector3(0f, 15f, 0f) );
//		
//		if (Input.GetKeyDown( KeyCode.Space ) )
//			ourBall.AddForce( ourBall.transform.forward * 100000f );
		Camera camera2 = GetComponent<Camera>();
		
		// Click and drag
		Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition ); // generate a ray based on our mousePosition
		RaycastHit rayHit = new RaycastHit(); // initialize the struct we'll need for rayHit later
		
		if ( Physics.Raycast( ray, out rayHit, 1000f ) ) { // did the raycast from our mousePosition hit something?
			if ( Input.GetMouseButton(0) ) { // is left mouse button pressed down?
				if (rayHit.collider.tag == "Ball") { // the thing we hit -- is it tagged with "Ball"?
					aimingMode = true;
					ball = rayHit.collider.rigidbody; 
				} else if (aimingMode) { // it wasn't the ball, but the player has clicked and dragged from the ball, so take an aiming vector
					Vector3 levelRayHitPoint = new Vector3(rayHit.point.x, ball.transform.position.y, rayHit.point.z);
					CalculateTrajectory( ball.transform.position, (ball.transform.position - levelRayHitPoint).normalized );
					ShowTrajectory();
				}
			} else { // so then the left mouse button is UP?
				if (aimingMode) {
					Vector3 levelRayHitPoint = new Vector3(rayHit.point.x, ball.transform.position.y, rayHit.point.z);
					ball.AddForce( (ball.transform.position - levelRayHitPoint).normalized * 10000f); 
				}
				// go to Edit > Project Settings > Time and set Fixed Time Step to something small (0.05)
				// and set Time Scale to something slow (0.2) or 20% of regular time speed.
				
				aimingMode = false; // mouse button was released + ball launched, so turn off aimingMode
			}
		} else { // raycast didn't hit anything
			aimingMode = false;
		}
		
		
	}
	
	void CalculateTrajectory (Vector3 initialBallPosition, Vector3 initialForceDirection) {
		const int maxPoints = 8;
		
		trajectory.Clear();
		trajectory.Add(initialBallPosition);
		
		// these must be OUTSIDE of while() loop so that we can preserve values between loops
		Vector3 currentBallPosition = initialBallPosition; 
		Vector3 currentForceDirection = initialForceDirection;
		
		while (trajectory.Count < maxPoints) {
			Ray ray = new Ray( currentBallPosition, currentForceDirection );
			RaycastHit rayHit = new RaycastHit();
			
			if ( Physics.Raycast( ray, out rayHit, 1000f ) ) {
				currentBallPosition = rayHit.point;
				float dotProduct = Vector3.Dot(currentForceDirection.normalized, rayHit.normal);
				currentForceDirection = -2 * (dotProduct * rayHit.normal) + currentForceDirection; // reflection = 2 * (dot)normal + velocity
				trajectory.Add(currentBallPosition);
			} else { // huh, that's weird, this shouldn't happen, but if there's a bug then we need to know about it
				Debug.LogWarning( "huh? stopped bouncing only after " + trajectory.Count.ToString() );
				break;
			}
		}
	}
	
	void ShowTrajectory () {
		line.SetVertexCount( trajectory.Count );
		
		for (int i=0; i<trajectory.Count; i++) {
			line.SetPosition( i, trajectory[i] );
		}
	}
}
