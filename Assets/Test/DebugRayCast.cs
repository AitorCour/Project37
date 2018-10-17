using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugRayCast : MonoBehaviour 
{
	public Vector3 direction = -Vector3.up;
	
	public RaycastHit hit;
	public float Maxdistance = 10;
	public LayerMask layermask;

	// Use this for initialization
	void Start () 
	{
		Debug.DrawRay(transform.position, transform.forward * Maxdistance, Color.green);
	}
	
	// Update is called once per frame
	void Update () 
	{
		Debug.DrawRay(transform.position, transform.forward * Maxdistance, Color.green);
		if(Physics.Raycast(transform.position, transform.forward, out hit, Maxdistance, layermask))
		{
			print(hit.transform.name);
		}
	}
}
