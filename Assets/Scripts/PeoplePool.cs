using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PeoplePool {
	/// <summary>
	/// Initializes a new instance of the <see cref="PeoplePool"/> class.
	/// </summary>
	private PeoplePool(){
		queue = new Queue<People>();
	}

	~PeoplePool(){

	}
	
	private static PeoplePool instance;
	private Queue<People> queue = null;
	private int totalCount = 0;

	/// <summary>
	/// Gets the instance.
	/// </summary>
	/// <returns>The instance.</returns>
	public static PeoplePool GetInstance(){
		if(instance == null){
			instance = new PeoplePool();
		}
		return instance;
	}

	/// <summary>
	/// return a people object
	/// </summary>
	/// <returns>The people.</returns>
	public People GetPeople(){
		if(this.Count == 0){
			totalCount++;
			return new People();
		}else{
			return queue.Dequeue();
		}
	}

	/// <summary>
	/// Puts the people into pool
	/// </summary>
	public void PutPeople(People people){
		if(people != null){
			people.Initialize();
			queue.Enqueue(people);
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
