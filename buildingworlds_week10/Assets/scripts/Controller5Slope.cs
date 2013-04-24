using UnityEngine;
using System.Collections;

public class Controller5Slope : MonoBehaviour {

    public float moveSpeed = 5f; // what's a good moveSpeed?
    public float turnRate = 15f; // what's a good turnRate?
    public float jumpPower = 5f;
    bool grounded = false;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        #region SLOPE DETECTION + GRAVITY / GROUNDED RAYCAST ========
        // if the ground-detecting raycast hits NOTHING, keep falling
        // ... NEW: added slope detection check

        if ( !Physics.Raycast( transform.position, -transform.up, 1.25f ) ) {
            Debug.Log( "ungrounded!" );
            Vector3 targetPosition = transform.position + Physics.gravity * Time.deltaTime;
            transform.position = Vector3.Lerp( transform.position, targetPosition, Time.deltaTime * 30f);
            grounded = false;
        } else {
            grounded = true;

            // we're grounded? okay, grab slope data then
            RaycastHit rayHit = new RaycastHit(); // initialize struct
            Ray ray = new Ray(transform.position + transform.up, -transform.up);
            if ( Physics.SphereCast( ray, 0.5f, out rayHit, 2f ) && rayHit.point.y + 1f > transform.position.y) {
                Debug.Log( rayHit.point.y + 1f );
                transform.position = Vector3.Lerp(transform.position, new Vector3( transform.position.x, rayHit.point.y + 1f, transform.position.z), Time.deltaTime * 5f);
            }
        }
        #endregion

        #region MOVING + WALL COLLISION ========
        // if player presses W, move forward, unless there's a wall in front of us
        bool capsuleCastHitWall = Physics.CapsuleCast(  transform.position - transform.up * 0.1f, // bottom of capsule
                                                    transform.position + transform.up * 0.5f, // top of capsule
                                                    0.5f, // radius of capsule
                                                    transform.forward, // direction of capsuleCast
                                                    0.1f ); // distance of capsuleCast
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