using UnityEngine;
using System.Collections;

public class WalkAndRun : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// We are in full control here - don't let any other animations play when we start
		animation.Stop();
	
		// By default loop all animations
		animation.wrapMode = WrapMode.Loop;
		//animation.Play("walk");
	
		// The jump animation is clamped and overrides all others
//		AnimationClip walk = animation["walk"];
//		walk.layer = 1;
//		walk.enabled = false;
//		walk.wrapMode = WrapMode.Clamp;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.R)){
			Debug.Log("RRRRRRRR");
			animation.CrossFade ("run");
		}else if(Input.GetKeyDown(KeyCode.T)){
			animation.CrossFade ("walk");
		}
	}
}
