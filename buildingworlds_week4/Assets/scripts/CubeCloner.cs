using UnityEngine;
using System.Collections;
using System.Collections.Generic; // this "using... " line lets us use List<>

public class CubeCloner : MonoBehaviour { // we put instantiation / spawn logic in a DIFFERENT script
	
	List<Flyer> flyerList = new List<Flyer>(); // a list is like a dynamically-resizable array
	public Flyer cubePrefab; // prefab object, assigned in Inspector
	public int cubeCount = 10;

	// Use this for initialization
	void Start () {
		for (int i=0; i<cubeCount; i++) { // FOR LOOP: start from 0; as long as it's less than cubeCount; keep looping and incrementing counter
			Flyer tempFlyer = Instantiate ( cubePrefab, Vector3.zero, Quaternion.identity ) as Flyer;
			flyerList.Add(tempFlyer); // add this flyer clone to our list of flyers
			
			tempFlyer.speed = i * 0.1f; // e.g. on the 5th flyer we spawn, it'll be 10% faster than the 4th flyer we spawned
			tempFlyer.SetNewTarget(); // we can call PUBLIC methods on other classes too
		}
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space) ) { 	// if we push the spacebar...
			foreach (Flyer f in flyerList) { 		// ... then grab every Flyer in our flyerList...
				f.SetNewTarget(Vector3.zero);		// ... and make them all go to (0,0,0)
			}
		}
	}

}
