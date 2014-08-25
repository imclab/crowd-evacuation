using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FramePool {
	/// <summary>
	/// Initializes a new instance of the <see cref="FramePool"/> class.
	/// </summary>
	private FramePool(){
		queue = new Queue<Frame>();
	}
	
	~FramePool(){
		
	}
	
	private static FramePool instance;
	private Queue<Frame> queue = null;
	private int totalCount = 0;
	
	/// <summary>
	/// Gets the instance.
	/// </summary>
	/// <returns>The instance.</returns>
	public static FramePool GetInstance(){
		if(instance == null){
			instance = new FramePool();
		}
		return instance;
	}
	
	/// <summary>
	/// Gets the frame.
	/// </summary>
	/// <returns>The frame.</returns>
	public Frame GetFrame(){
		if(this.Count == 0){
			totalCount++;
			return new Frame();
		}else{
			return queue.Dequeue();
		}
	}
	
	/// <summary>
	/// Puts the frame.
	/// </summary>
	/// <param name="frame">Frame.</param>
	public void PutFrame(Frame frame){
		if(frame != null){
			frame.Initialize();
			queue.Enqueue(frame);
		}
	}
	
	/// <summary>
	/// Gets the object count in pool.
	/// </summary>
	/// <returns>The count.</returns>
	public int Count{
		get{
			return queue.Count;
		}
	}
	
	/// <summary>
	/// Gets the total count, include objects in the pool and which are working.
	/// </summary>
	/// <value>The total count.</value>
	public int TotalCount{
		get{
			return totalCount;
		}
	}
}
