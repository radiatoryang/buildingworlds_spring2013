using UnityEngine;
using System.Collections;

public class Controller4Jump : MonoBehaviour {

    public float moveSpeed = 5f; // what's a good moveSpeed?
    public float turnRate = 15f; // what's a good turnRate?
    public float jumpPower = 5f;
    bool grounded = false;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        #region GRAVITY / GROUNDED RAYCAST ========
        // if the ground-detecting raycast hits NOTHING, keep falling
        if ( !Physics.Raycast( transform.position, -transform.up, 1f ) ) {
            transform.position += Physics.gravity * Time.deltaTime;
            grounded = false;
        } else {
            grounded = true;
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

        #region JUMP INPUT ======
        if ( grounded && Input.GetKeyDown( KeyCode.Space ) ) {
            StartCoroutine( Jump() );
        }
        #endregion
    }

    IEnumerator Jump() {
        float t = 0f;
        while ( t < 1f ) {
            // apply jump translation; counteract gravity
            transform.position += (-Physics.gravity + Vector3.up * jumpPower) * Time.deltaTime;
            t += Time.deltaTime / 1f;
            if ( Physics.Raycast( transform.position, transform.up, 1.1f ) ) {
                break; // stop jumping if we hit the ceiling
            }
            yield return 0;
        }
    }
}