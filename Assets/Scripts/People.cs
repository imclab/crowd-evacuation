using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PeopleStatus{
	IDLE=0,
	RUN,
	WALK,
	BOW,//wanyao
	FALL,//daodi
	DEAD,
	STAND//stand
}

/// <summary>
/// People.
///  Store the variant in one moment.
/// </summary>
public class People {
	public int m_id;
	public int m_role;
	public PeopleStatus m_status;
	public Vector3 m_position;
	public Vector3 m_orientation;

	//used to control the speed of animation
	public float m_animBaseSpeed;
	public float m_animSpeed;

	//each component represent the scale of each part
	//x: thickness of the body
	//y: fatness 
	//z: height
	public Vector3 m_figureScale;

	public People(){
		Initialize();
	}
	
	public People(int id, int role, int status, Vector3 targetPosition, 
	              Vector3 targetOrnt, float animSpeed, Vector3 figureScale){
		m_id = id;
		m_role = role;
		m_status = (PeopleStatus)status;
		m_position = targetPosition;
		m_orientation = targetOrnt;
		m_animSpeed = animSpeed;
		m_figureScale = figureScale;
	}

	public void Initialize(){
		m_id = 0;
		m_role = -1;
		m_status = (PeopleStatus)0;
		m_position = Vector3.zero;
		m_orientation = Vector3.zero;
		m_animBaseSpeed = 1.0f;
		m_animSpeed = 0.0f;
		m_figureScale = new Vector3(1.0f, 1.0f, 1.0f);
	}

	public override string ToString ()
	{
		return string.Format("[People{0}]:{1} {2}", m_id, m_role, m_status);
	}
}
