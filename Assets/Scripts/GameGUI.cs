using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
public class GameGUI : MonoBehaviour {

	private string strButton = "暂停";
	private bool isPause = false;
	private float transparent = 0.5f;
	private GameObject goWall = null;
	private GameObject goFlyController = null;
	private GameObject goClassroomCam = null;
	private GameObject goCornerCam = null;
	private GameObject goSquareCam = null;

	// Use this for initialization
	void Start () {
		goWall = GameObject.Find("phxx/1Wall");
		SetTransparent();
		goFlyController = GameObject.Find("FlyController");
		goClassroomCam = GameObject.Find("Views/Classroom");
		goCornerCam = GameObject.Find("Views/Corner");
		goSquareCam = GameObject.Find("Views/Square");
		goFlyController.SetActive(true);
		goClassroomCam.SetActive(false);
		goCornerCam.SetActive(false);
		goSquareCam.SetActive(false);
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
		if(GUILayout.Button("自由")){
			goFlyController.SetActive(true);
			goClassroomCam.SetActive(false);
			goCornerCam.SetActive(false);
			goSquareCam.SetActive(false);
		}
		if(GUILayout.Button("教室")){
			goFlyController.SetActive(false);
			goClassroomCam.SetActive(true);
			goCornerCam.SetActive(false);
			goSquareCam.SetActive(false);
		}
		if(GUILayout.Button("拐角")){
			goFlyController.SetActive(false);
			goClassroomCam.SetActive(false);
			goCornerCam.SetActive(true);
			goSquareCam.SetActive(false);
		}
		if(GUILayout.Button("广场")){
			goFlyController.SetActive(false);
			goClassroomCam.SetActive(false);
			goCornerCam.SetActive(false);
			goSquareCam.SetActive(true);
		}
		GUILayout.EndHorizontal();
		GUILayout.EndVertical();
	}

	void SetTransparent(){
//		foreach(Transform tr in goWall.transform){
//			Color tmp = tr.gameObject.renderer.material.GetColor("_Color");
//			tmp.a = transparent;
//			tr.gameObject.renderer.material.SetColor("_Color", tmp);
//		}
	}
}
