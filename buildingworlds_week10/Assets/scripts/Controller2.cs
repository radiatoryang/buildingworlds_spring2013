using UnityEngine;
using System.Collections;

public class Controller2 : MonoBehaviour {

    public float moveSpeed = 5f; // what's a good moveSpeed?
    public float turnRate = 15f; // what's a good turnRate?

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        #region GRAVITY / GROUNDED RAYCAST ========
        // if the ground-detecting raycast hits NOTHING, keep falling
        if ( !Physics.Raycast( transform.position, -transform.up, 1f ) ) {
            transform.position += Physics.gravity * Time.deltaTime;
        } 
        #endregion

        #region MOVING ========
        // if player presses W, move forward
        if ( Input.GetKey( KeyCode.W ) ) {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
        #endregion

        #region TURNING ========
        // if player presses A, turn left; if D, turn right.
        if ( Input.GetKey( KeyCode.A ) ) {
            transform.Rotate( Vector3.up, -turnRate * Time.deltaTime );
        } else if ( Input.GetKey( KeyCode.D ) ) {
            transform.Rotate( Vector3.up, turnRate * Time.deltaTime );
        }
        #endregion
    }
}
