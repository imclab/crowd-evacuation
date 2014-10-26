using UnityEngine;
using System.Collections;
using System.IO;
using System.Data;

public class ReadAndParseDataFromDB : MonoBehaviour {

	//private string dbName = "agents";
	public string tableName = "test";
	//public bool isReadAll = false;
	
	public float interval = 0.1f;
	public int linesPerRead = 10000;
	private int linesPerFrame = 0;
	private int startLine = 0;
	private int frameID = 0;

	private MySqlDB mysqldb = null;
	private string debugInfo = "";

//	void OnGUI(){
//		GUILayout.BeginHorizontal("debug");
//		GUILayout.Label(mysqldb.dbConnection.ConnectionString);
//		GUILayout.Label(debugInfo);
//		GUILayout.EndHorizontal();
//	}
	
	// Use this for initialization
	void Start () {
		Application.runInBackground = true;
		Screen.SetResolution(804, 673, false);
		mysqldb = new MySqlDB();

		DataTable dt = mysqldb.SelectLimitLines(tableName, startLine, 1);
		linesPerFrame = int.Parse(dt.Rows[0][1].ToString());
		startLine++;
		InvokeRepeating("ReadOneFrameFromMySql", 0f, interval);
	}
	
	// Update is called once per frame
	void Update () {
		//ReadOneFrameFromMySql();
	}

	void ReadOneFrameFromMySql(){
		DataTable result = mysqldb.SelectLimitLines(tableName, startLine, linesPerFrame);
		if(result.Rows.Count == 0){
			Debug.Log("waiting for data...");
			return;
		}else if(result.Rows.Count == 1){
			Debug.Log("data finish...");
			SendMessage("NotifyFinish");
			CancelInvoke("ReadOneFrameFromMySql");
			return;
		}else if(result.Rows.Count < linesPerFrame){
			//data incomplete
			return;
		}
		
		Frame frame = FramePool.GetInstance().GetFrame();//new Frame();
		frame.id = frameID;
		for (int i = 0; i < result.Rows.Count; i++) {
			//"id:{0}, people_id:{1}, people_status:{2}, posx:{3}, posy:{4}"
			//"posz:{5}, velx:{6}, vely:{7}",
			DataRow data = result.Rows[i];
			int peopleID = int.Parse(data[1].ToString());
			int peopleRole = int.Parse(data[2].ToString());
			int peopleStatus = int.Parse(data[3].ToString());
			float posx = float.Parse(data[4].ToString());
			float posy = float.Parse(data[6].ToString());//posz
			float posz = float.Parse(data[5].ToString());//posy
			float orntx = float.Parse(data[7].ToString());
			float ornty = float.Parse(data[8].ToString());
			//if(peopleID <= 10){//test propurse only
			People p = PeoplePool.GetInstance().GetPeople();
			p.m_id = peopleID;
			p.m_role = peopleRole;
			p.m_status = (PeopleStatus)peopleStatus;
			p.m_position = new Vector3(posx,posy,posz);
			p.m_orientation = new Vector3(orntx, 0, ornty);
			p.m_animSpeed = p.m_animBaseSpeed + (Random.value - 0.5f) * 0.4f;
			p.m_figureScale = new Vector3(1.0f + (Random.value - 0.5f) * 0.4f, 
			                              1.0f + (Random.value - 0.5f) * 0.4f, 
			                              1.0f + (Random.value - 0.5f) * 0.4f);
			frame.peoples.Add(peopleID, p);
			//}
		}
		FrameBuffer.GetInstance().GetQueue().Enqueue(frame);
//		if(frameID < 15){
//			debugInfo += frame.ToString();
//		}
		frameID++;
		startLine += result.Rows.Count;
		//Debug.Log(frameID + " finish...");
	}

	void OnDestroy(){
		mysqldb.Close();
	}

	
}
