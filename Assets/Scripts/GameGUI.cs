using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
public class GameGUI : MonoBehaviour {

	private string strButton = "暂停";
	private bool isPause = false;
	private float transparent = 0.5f;
	private GameObject goWall = null;
	private GameObject goFlyController = null;
	private GameObject goClassroomVp = null;
	private GameObject goCornerVp = null;
	private GameObject goSquareVp = null;
	private FlyCamera cmpFlyCamera = null;

	// Use this for initialization
	void Start () {
		goWall = GameObject.Find("phxx3f3/1Walls");
		SetTransparent();
		goFlyController = GameObject.Find("FlyController");
		goClassroomVp = GameObject.Find("ViewPoints/Classroom");
		goCornerVp = GameObject.Find("ViewPoints/Corner");
		goSquareVp = GameObject.Find("ViewPoints/Square");
		cmpFlyCamera = goFlyController.GetComponent<FlyCamera>();
		//start point : class room
		goFlyController.transform.position = goCornerVp.transform.position;
		cmpFlyCamera.rotationX = goCornerVp.transform.localScale.x;
		cmpFlyCamera.rotationY = goCornerVp.transform.localScale.y;
	}
	
	void OnGUI(){
		GUILayout.BeginVertical("box");
		if(GUILayout.Button(strButton)){
			if(isPause){
				Time.timeScale = 1;
				strButton = "暂停";
			}else{
				Time.timeScale = 0;
				strButton = "继续";
			}
			isPause = !isPause;
		}
		GUILayout.Label("墙壁透明度");
		transparent = GUILayout.HorizontalSlider(transparent, 0.0f, 1.0f);
		//Debug.Log(transparent);
		SetTransparent();
		GUILayout.BeginHorizontal("box");
		if(GUILayout.Button("教室")){
			goFlyController.transform.position = goClassroomVp.transform.position;
			cmpFlyCamera.rotationX = goClassroomVp.transform.localScale.x;
			cmpFlyCamera.rotationY = goClassroomVp.transform.localScale.y;
		}
		if(GUILayout.Button("拐角")){
			goFlyController.transform.position = goCornerVp.transform.position;
			cmpFlyCamera.rotationX = goCornerVp.transform.localScale.x;
			cmpFlyCamera.rotationY = goCornerVp.transform.localScale.y;
		}
		if(GUILayout.Button("广场")){
			goFlyController.transform.position = goSquareVp.transform.position;
			cmpFlyCamera.rotationX = goSquareVp.transform.localScale.x;
			cmpFlyCamera.rotationY = goSquareVp.transform.localScale.y;
		}
		GUILayout.EndHorizontal();
		GUILayout.EndVertical();
	}

	void SetTransparent(){
		foreach(Transform tr in goWall.transform){
			Color tmp = tr.gameObject.renderer.material.GetColor("_Color");
			tmp.a = transparent;
			tr.gameObject.renderer.material.SetColor("_Color", tmp);
		}
	}
}
