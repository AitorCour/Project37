using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvents : MonoBehaviour 
{
	
	private EnemyBehaviour3 enemy;
	// Use this for initialization
	void Start () 
	{
		enemy = GetComponentInParent<EnemyBehaviour3>();
	}
	
	public void EndAttack()
	{
		enemy.Attack();
	}
}
