using UnityEngine;
using System.Collections;

public class Controller3 : MonoBehaviour {

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

        #region MOVING + WALL COLLISION ========
        // if player presses W, move forward, unless there's a wall in front of us
        bool capsuleCastHitWall = Physics.CapsuleCast(  transform.position + transform.up, // top of capsule
                                                    transform.position - transform.up, // bottom of capsule
                                                    0.5f, // radius of capsule
                                                    transform.forward, // direction of capsuleCast
                                                    1f ); // distance of capsuleCast
        if ( Input.GetKey( KeyCode.W ) && !capsuleCastHitWall ) {
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