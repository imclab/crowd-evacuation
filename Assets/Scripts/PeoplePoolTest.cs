using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PeoplePoolTest : MonoBehaviour {

	public int resourceCount = 0;
	public int totalCount = 0;
	private List<People> peoples = new List<People>();
	private int i = 0,j = 0;
	// Use this for initialization
	IEnumerator Start () {
		Debug.Log("1:" + Time.time);
		yield return StartCoroutine("GetPeople");
		Debug.Log("2:" + Time.time);
		yield return StartCoroutine("PutPeople");
		Debug.Log("3:" + Time.time);
		i=0; j=0;
		yield return StartCoroutine("GetPeople");
	}
	
	// Update is called once per frame
	void Update () {
		resourceCount = PeoplePool.GetInstance().Count;
		totalCount = PeoplePool.GetInstance().TotalCount;
	}

	IEnumerator GetPeople(){
		while(i < 100){
			i++;
			peoples.Add( PeoplePool.GetInstance().GetPeople());
			yield return null;
		}
	}

	IEnumerator PutPeople(){
		while(j < 50){
			PeoplePool.GetInstance().PutPeople(peoples[j]);
			j++;
			yield return null;
		}
	}

	IEnumerator WaitAndPrint(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		//print("WaitAndPrint " + Time.time);
	}
}
