using UnityEngine;
using System.Collections;
using System.Collections.Generic; // this "using... " line lets us use List<>

// we could technically put this in Flyer.cs
// but this script has NOTHING TO DO WITH FLYING
// and nothing in Flyer.cs needs to know about anything about lines or drawing
// so to make our code simpler and easier to maintain / debug, we put this in a separate script
public class LineLogger : MonoBehaviour { 
	
	List<Vector3> pastPositions = new List<Vector3>();
	
	LineRenderer lineRenderer; // we could also make this public, and assign the reference from the inspector
	public float recordFrequency = 0.2f; // how often to log a line point?
	public int maxPointsStored = 50; // how many points to store before deleting older points?

	// Use this for initialization
	void Start () {
		lineRenderer = GetComponent<LineRenderer>(); // grab reference to LineRenderer, since it's not assigned in inspector
		
		// call RecordPosition() every 2 seconds after an initial delay of 0 seconds
		InvokeRepeating("RecordPosition", 0f, recordFrequency); 
	}
	
	void RecordPosition () {
		if (pastPositions.Count >= maxPointsStored) 	// if the size of pastPositions is more than our max...
			pastPositions.RemoveAt(0);				// ... then remove the first (and oldest) point from the list.
		
		pastPositions.Add( transform.position ); // add our current position to our list of past positions
		
		lineRenderer.SetVertexCount( pastPositions.Count ); 	// we must SetVertexCount because LineRenderer (which is built-in in Unity) uses an immutable array, not a resizable List
		for(int i=0; i<pastPositions.Count; i++) { 				// FOR: from an integer starting at 0, as long as it's less than all pastPositions, keep looping and incrementing by 1
			lineRenderer.SetPosition( i, pastPositions[i] );	// SetPosition() is a special method that LineRenderer uses for setting its array (look in the docs)
		}

	}
}
