using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Utilities : MonoBehaviour {
	/// <summary>
	/// Compares the two frame.
	/// </summary>
	/// <returns>
	/// The two frame.
	/// for example:
	/// {"new":[1,2,3],
	/// "del":[4,5,6],
	/// "same":[7,8,9]}
	/// </returns>
	public static Dictionary<string, List<int>> CompareTwoFrames(Frame first,
		Frame second){
		Dictionary<string, List<int>> dict = new Dictionary<string, List<int>>();
		List<int> newIDs = new List<int>();
		List<int> delIDs = new List<int>();
		List<int> sameIDs = new List<int>();
		if(first.id == second.id){
			return null;
		}
		List<int> firstKeys = new List<int>(first.peoples.Keys);
		List<int> secondKeys = new List<int>(second.peoples.Keys);
		while(firstKeys.Count > 0){
			int k = firstKeys[0];
			if(secondKeys.Contains(k)){
				sameIDs.Add(k);
				firstKeys.Remove(k);
				secondKeys.Remove(k);
			}else{
				delIDs.Add(k);
				firstKeys.Remove(k);
			}
		}
		newIDs = secondKeys;
		
		dict.Add("new", newIDs);
		dict.Add("del", delIDs);
		dict.Add("same", sameIDs);
		return dict;
	}
	
	
//	// Use this for initialization
//	void Start () {
//	
//	}
//	
//	// Update is called once per frame
//	void Update () {
//	
//	}
//	
	public static List<GameObject> GetChildren(GameObject go)
    {
        List<GameObject> children = new List<GameObject>();
        foreach (Transform tran in go.transform)
        {
            children.Add(tran.gameObject);
        }
        return children;
    }
	
	//convert 3d bounds to 2d rect, discarding y coordinate
	public static Rect GetRectInXZ(Bounds bounds){
		Vector3 min = bounds.min;
		Vector3 max = bounds.max;
		//Debug.Log("min: "+min);
		//Debug.Log("max: "+max);
		Rect rect = new Rect(min.x, min.z, max.x - min.x, max.z - min.z);
		//Debug.Log(rect);
		return rect;
	}
		
	public static IEnumerator WaitAMoment(float delay)
	{
    	yield return new WaitForSeconds(delay);
	}
}
