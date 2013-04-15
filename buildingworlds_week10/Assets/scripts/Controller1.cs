using UnityEngine;
using System.Collections;

public class Controller1 : MonoBehaviour {

    public float moveSpeed = 5f; // what's a good moveSpeed?
    public float turnRate = 15f; // what's a good turnRate?

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        #region MOVING
        // if player presses W, move forward
        if ( Input.GetKey( KeyCode.W ) ) {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
        #endregion

        #region TURNING
        // if player presses A, turn left; if D, turn right.
        if ( Input.GetKey( KeyCode.A ) ) {
            transform.Rotate( Vector3.up, -turnRate * Time.deltaTime );
        } else if ( Input.GetKey( KeyCode.D ) ) {
            transform.Rotate( Vector3.up, turnRate * Time.deltaTime );
        }
        #endregion
    }
}
