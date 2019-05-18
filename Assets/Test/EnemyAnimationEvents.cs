using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvents : MonoBehaviour 
{
	
	private EnemyBehaviour3 enemy;
    private CollisionDamage colDamage;
    // Use this for initialization
    void Start () 
	{
		enemy = GetComponentInParent<EnemyBehaviour3>();
        colDamage = GetComponentInChildren<CollisionDamage>();
    }
    public void Footstep()
    {
        enemy.PlayFootstepEnemy();
    }
    public void DeadFall()
    {
        enemy.PlayDeadFall();
    }
    public void DoDamage()
    {
        colDamage.CanDoDamage = true;
    }
    public void DoNotDamage()
    {
        colDamage.CanDoDamage = false;
    }
}
