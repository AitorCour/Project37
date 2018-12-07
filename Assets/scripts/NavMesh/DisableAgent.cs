using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DisableAgent : MonoBehaviour 
{
	private NavMeshAgent myNavAgent;
	public bool active = true;
	
	public float timeCounter;
	public float disTime = 1.0f;

	// Use this for initialization
	void Start () 
	{
		myNavAgent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!active)
		{
			myNavAgent.enabled = false;
			UpdateTime();
		}
		else if(active)
		{
			myNavAgent.enabled = true;
			timeCounter = 0;
		}
	}

	void UpdateTime()
	{
		if(timeCounter >= disTime)
		{
			active = true;
		}
		else timeCounter += Time.deltaTime;
	}
}
