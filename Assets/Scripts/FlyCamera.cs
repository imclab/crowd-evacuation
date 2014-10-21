using UnityEngine;
using System.Collections;
 
public class FlyCamera : MonoBehaviour
{
 
	/*
	EXTENDED FLYCAM
		Desi Quintans (CowfaceGames.com), 17 August 2012.
		Based on FlyThrough.js by Slin (http://wiki.unity3d.com/index.php/FlyThrough), 17 May 2011.
 
	LICENSE
		Free as in speech, and free as in beer.
 
	FEATURES
		WASD/Arrows:    Movement
		          Q:    Climb
		          E:    Drop
                      Shift:    Move faster
                    Control:    Move slower
                        End:    Toggle cursor locking to screen (you can also press Ctrl+P to toggle play mode on and off).
	*/
 
	public float cameraSensitivity = 90;
	public float climbSpeed = 4;
	public float normalMoveSpeed = 10;
	public float slowMoveFactor = 0.25f;
	public float fastMoveFactor = 3;
	public float minDistance = 20;
 
	public float rotationX = 180.0f;
	public float rotationY = 0.0f;
	
 	private bool mouseDown = false;
	
	void Start ()
	{
//		GameObject target = GameObject.Find("Target"); 
//		iTween.MoveTo(gameObject,iTween.Hash(
//			"position",new Vector3(32,5,56),
//			"time",2.0f,
//			"looktarget",target.transform,
//			"looktime",0.0f
//			));
	}
 
	void Update ()
	{
		//when mouse is interacting with GUI elements, such as buttons, sliders.
		//Attention: labels are not included.
		if(GUIUtility.hotControl != 0){
			return;
		}

		//rotate
		if(Input.GetMouseButton(0)){
			rotationX += Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;
			rotationY += Input.GetAxis("Mouse Y") * cameraSensitivity * Time.deltaTime;
			rotationY = Mathf.Clamp(rotationY, -90, 90);
		}
 
		transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
		transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);
 
		//translate
		Vector3 newPosition = transform.position;
	 	if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift))
	 	{
			newPosition += transform.forward * (normalMoveSpeed * fastMoveFactor) * Input.GetAxis("Vertical") * Time.deltaTime;
			newPosition += transform.right * (normalMoveSpeed * fastMoveFactor) * Input.GetAxis("Horizontal") * Time.deltaTime;
	 	}
	 	else if (Input.GetKey (KeyCode.LeftControl) || Input.GetKey (KeyCode.RightControl))
	 	{
			newPosition += transform.forward * (normalMoveSpeed * slowMoveFactor) * Input.GetAxis("Vertical") * Time.deltaTime;
			newPosition += transform.right * (normalMoveSpeed * slowMoveFactor) * Input.GetAxis("Horizontal") * Time.deltaTime;
	 	}
	 	else
	 	{
	 		newPosition += transform.forward * normalMoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
			newPosition += transform.right * normalMoveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
	 	}
 
 
		if (Input.GetKey (KeyCode.Q)) {newPosition += transform.up * climbSpeed * Time.deltaTime;}
		if (Input.GetKey (KeyCode.E)) {newPosition -= transform.up * climbSpeed * Time.deltaTime;}
		
		//collision detection
		Vector3 dir = newPosition - transform.position;
		Ray ray = new Ray(transform.position, dir);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, minDistance)){
			Debug.DrawLine(ray.origin, hit.point, Color.red);
			Debug.Log(hit.collider);
		}else{
			transform.position = newPosition;
		}
 
//		if (Input.GetKeyDown (KeyCode.End))
//		{
//			Screen.lockCursor = (Screen.lockCursor == false) ? true : false;
//		}
	}
}
