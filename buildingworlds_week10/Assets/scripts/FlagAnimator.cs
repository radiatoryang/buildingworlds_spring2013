using UnityEngine;
using System.Collections;

public class FlagAnimator : MonoBehaviour {
	
	Animation anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animation>();
        anim["FlagWave"].layer = 0;
        anim["FlagDead"].layer = 2;
	}
	
	// Update is called once per frame
	void Update () {
        anim.CrossFade( "FlagWave" );

	}
}
