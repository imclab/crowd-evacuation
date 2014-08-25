using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Frame buffer. Singleton
/// </summary>
public class FrameBuffer {
	private FrameBuffer(){
		 queue = new Queue<Frame>();
	}
	
	private static FrameBuffer instance;
	
	public static FrameBuffer GetInstance(){
        if(instance==null){
        	instance=new FrameBuffer();
        }
        return instance;
	}
	
	public Queue<Frame> GetQueue(){
		return queue;
	}
	
	/// <summary>
	/// frame buffer.
	/// </summary>
	private Queue<Frame> queue = null;
}
