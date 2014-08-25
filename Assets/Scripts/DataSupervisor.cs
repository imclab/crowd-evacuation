using UnityEngine;
using System.Collections;

public class DataSupervisor : MonoBehaviour {
	
	public int peopleCount = 0;
	public int peopleTotalCount = 0;
	public int frameCount = 0;
	public int frameTotalCount = 0;
	public int frameBufferCount = 0;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		peopleCount = PeoplePool.GetInstance().Count;
		peopleTotalCount = PeoplePool.GetInstance().TotalCount;
		frameCount = FramePool.GetInstance().Count;
		frameTotalCount = FramePool.GetInstance().TotalCount;
		frameBufferCount = FrameBuffer.GetInstance().GetQueue().Count;
	}
}
