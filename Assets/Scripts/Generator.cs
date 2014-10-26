using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Generator.
/// version : 0.5
/// </summary>
public class Generator : MonoBehaviour {
	/// <summary>
	/// The charactors. user specified prefabs.
	/// </summary>
	public List<GameObject> charactors;
	
	/// <summary>
	/// The last frame data
	/// </summary>
	Frame lastFrame = null;
	
	GameObject parent = null;
	
	/// <summary>
	/// The dead I ds. Record the id of people who is dead
	/// </summary>
	List<int> deadIDs = new List<int>();
	
	List<int> daodiIDs = new List<int>();
	
	/// <summary>
	/// The time of one frame.
	/// </summary>
	public float frameTime = 0.1f;
	
	/// <summary>
	/// The stood people objects.
	/// </summary>
	private Dictionary<int, People> stoodPeople = new Dictionary<int, People>();

	private bool hasFinished = false;
	
	// Use this for initialization
	void Start () {
		parent = GameObject.Find("/Instances");
		InvokeRepeating("GenerateOneFrame", 1f, frameTime);
	}

	void LateUpdate () {
		//GenerateOneFrame();
	}
	
	void GenerateOneFrame(){
		Queue<Frame> queue = FrameBuffer.GetInstance().GetQueue();
		Frame thisFrame = null;
		if(queue.Count == 0){
			if(hasFinished){
				CancelInvoke("GenerateOneFrame");
				foreach(Transform people in parent.transform){
					if(people.gameObject.animation){
						people.gameObject.animation.wrapMode = WrapMode.Loop;
						people.gameObject.animation.CrossFade("idle");
					}
				}
				Debug.Log("Simulation Finished!");
			}else{
				Debug.Log("no more data...");
			}
			return;
		}else{
			thisFrame = queue.Dequeue();
		}
		//Debug.Log(thisFrame.id + "generating...");
		
		List<int> newIDs = new List<int>();
		List<int> delIDs = new List<int>();
		List<int> sameIDs = new List<int>();
		
		//this is the first frame
		if(lastFrame == null){
			newIDs = new List<int>(thisFrame.peoples.Keys);
		}else{
			Dictionary<string, List<int>> dict = 
				Utilities.CompareTwoFrames(lastFrame, thisFrame);
			newIDs = dict["new"];
			delIDs = dict["del"];
			sameIDs = dict["same"];
		}
		
		System.Random random = new System.Random();
		if(newIDs.Count != 0){
			foreach(int id in newIDs){
				int role = thisFrame.peoples[id].m_role;
				int chrctIndex = -1;//random.Next(charactors.Count);
				switch(role)
				{
				case 0://teacher
					chrctIndex = 10+random.Next(5);
					break;
				case 1://student
					chrctIndex = random.Next(10);
					break;
				default://random role
					chrctIndex = random.Next(charactors.Count);
					break;
				}
				GameObject instance = (GameObject)Instantiate(
					charactors[chrctIndex],
					thisFrame.peoples[id].m_position, 
					Quaternion.Euler(0.0f, random.Next(90), 0.0f));

				instance.name = "people" + id.ToString();
				instance.transform.parent = parent.transform;
				instance.transform.localScale = Vector3.Scale(instance.transform.localScale, thisFrame.peoples[id].m_figureScale);
				if(instance.gameObject.animation){
					instance.gameObject.animation.wrapMode = WrapMode.Loop;
					foreach(AnimationState state in instance.gameObject.animation){
						state.speed = thisFrame.peoples[id].m_animSpeed;
					}
					instance.gameObject.animation.Play("idle");
				}
			}
		}
		if(delIDs.Count != 0){
			foreach(int id in delIDs){
				string name = "/Instances/people" + id.ToString();
				Destroy(GameObject.Find(name));
			}
		}
		if(sameIDs.Count != 0){
			foreach(int id in sameIDs){
				string name = "/Instances/people" + id.ToString();
				People people = thisFrame.peoples[id];
								
				GameObject goPeople = GameObject.Find(name);
				Vector3 pos = people.m_position;
				PeopleStatus status = people.m_status;
				
				if(status == PeopleStatus.WALK){//walk
					//play animation
					if(goPeople.gameObject.animation){
						goPeople.gameObject.animation.wrapMode = WrapMode.Loop;
						goPeople.gameObject.animation.CrossFade("walk");
					}
					//move
					iTween.MoveTo(goPeople, 
						iTween.Hash("position",pos,
						"time",frameTime,
						"orienttopath",true,
						"axis","y",
						"looptype",iTween.LoopType.none,
						"easetype",iTween.EaseType.linear));
				}else if(status == PeopleStatus.RUN){//run
					//play animation
					if(goPeople.gameObject.animation){
						goPeople.gameObject.animation.wrapMode = WrapMode.Loop;
						goPeople.gameObject.animation.CrossFade("run");
					}
					//move
					iTween.MoveTo(goPeople, 
					              iTween.Hash("position",pos,
					            "time",frameTime,
					            "orienttopath",true,
					            "axis","y",
					            "looptype",iTween.LoopType.none,
					            "easetype",iTween.EaseType.linear));
				}else if(status == PeopleStatus.IDLE){//idle, on the escalator
					//play animation
					if(goPeople.gameObject.animation){
						goPeople.gameObject.animation.wrapMode = WrapMode.Loop;
						goPeople.gameObject.animation.CrossFade("idle");
					}
					//move
					iTween.MoveTo(goPeople, 
						iTween.Hash("position",pos,
					            "time",frameTime,
					            "orienttopath",true,
					            "axis","y",
						"looptype",iTween.LoopType.none,
						"easetype",iTween.EaseType.linear));
				}else if(status == PeopleStatus.BOW){//wanyao
					if(goPeople.gameObject.animation){
						goPeople.gameObject.animation.wrapMode = WrapMode.Once;
						goPeople.gameObject.animation.CrossFade("wanyao");
					}
				}else if(status == PeopleStatus.FALL){//daodi
					if(daodiIDs.Contains(id)){
						continue;
					}else{
						if(goPeople.gameObject.animation){
							goPeople.gameObject.animation.wrapMode = WrapMode.Once;
							goPeople.gameObject.animation.CrossFade("daodi");
							daodiIDs.Add(id);
						}
					}
				}else if(status == PeopleStatus.DEAD){//dead
					//the man has already dead
					if(deadIDs.Contains(id)){
						continue;	
					}else{
						if(goPeople.gameObject.animation){
							goPeople.gameObject.animation.wrapMode = WrapMode.Once;
							goPeople.gameObject.animation.CrossFade("dead");
							deadIDs.Add(id);
						}
					}
				}else if(status == PeopleStatus.STAND){//stand
					if(goPeople.gameObject.animation){
						goPeople.gameObject.animation.wrapMode = WrapMode.Once;
						goPeople.gameObject.animation.CrossFade("idle");
					}
				}
				
			}
		}
		if(lastFrame != null){
			foreach(KeyValuePair<int, People> kvp in lastFrame.peoples)	{
				PeoplePool.GetInstance().PutPeople(kvp.Value);
			}
		}
		FramePool.GetInstance().PutFrame(lastFrame);
		lastFrame = thisFrame;
		//////////
//		if(thisFrame.id == 0){
//			Time.timeScale = 0;
//		}
	}

	void NotifyFinish(){
		hasFinished = true;
	}
}
