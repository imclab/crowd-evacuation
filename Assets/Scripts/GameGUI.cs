using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
public class GameGUI : MonoBehaviour {

	private string strButton = "暂停";
	private bool isPause = false;
	private float transparent = 0.5f;
	private GameObject goWall = null;

	// Use this for initialization
	void Start () {
		goWall = GameObject.Find("phxx/1Wall");
		SetTransparent();
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
		Debug.Log(transparent);
		SetTransparent();
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
