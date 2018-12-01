using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class LookAtEnemy : MonoBehaviour
{
    //public Transform[] enemy;
	//public int currentEnemy;
	public Transform target;
   //public GameObject enemy;
   // This is what the player is looking at. In this example it is the dinosaur's head.
 
   public GameObject fovStartPoint;
   // We will use the forward direction of whatever GameObject you give it.
 
   public float lookSpeed = 200;
   // How fast the rotation happens.
 
   public float maxAngle = 45;
   // The maximum fov to trigger looking at the enemy.
 
   public float maxAngleReset = 90;
   // The maximum fov to trigger returning to the base state.
     
   public bool canLean = false;
   // This turns on looking up/down depending on the enemy's height.
 
   public bool arms = false;
 
   private Quaternion lookAt;
   private Quaternion targetRotation;

   //Detectar
   //public Transform player;
   //public float maxAngle;
   public float maxRadius;

   private bool isInFov = false;
   public bool detected = false;

   void Start()
   {
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
   }

   void UpdateTarget()
   {
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;

		foreach (GameObject enemy in enemies)
		{
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			if(distanceToEnemy < shortestDistance)
			{
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}

		if(nearestEnemy != null)
		{
			target = nearestEnemy.transform;
		}
		else
		{
			target = null;
		}
   }

   void Update()
   {
		isInFov = inFOV(transform, target, maxAngle, maxRadius);
        if (EnemyInFieldOfView(fovStartPoint) && isInFov)
        {
            //Vector3 direction = enemy.transform.position - transform.position;
			//Vector3 direction = enemy[currentEnemy].position - transform.position;
			Vector3 direction = target.position - transform.position;
 
            if (!canLean)
            {
                direction = new Vector3(direction.x, 0, direction.z);
            }
 
            // Rotate the current transform to look at the enemy
            targetRotation = Quaternion.LookRotation(direction);
            lookAt = Quaternion.RotateTowards(
            transform.rotation, targetRotation, Time.deltaTime * lookSpeed);
            transform.rotation = lookAt;
        }
        else if (EnemyInFieldOfViewNoResetPoint(fovStartPoint))
        {
            return;
        }
        else
        {
            if (arms)
            {
                // make arms point at the ground
                Quaternion targetRotation = Quaternion.Euler(0, 0, 0);//Con el primer valor a 90, los brazos apuntarán al suelo
                transform.localRotation = Quaternion.RotateTowards(
                transform.localRotation, targetRotation, Time.deltaTime * lookSpeed);
            }
 
            else
            {
                // return to initial local angle
                Quaternion targetRotation = Quaternion.Euler(0, 0, 0);
                transform.localRotation = Quaternion.RotateTowards(
                transform.localRotation, targetRotation, Time.deltaTime * lookSpeed);
            }
        }
   }
 
   bool EnemyInFieldOfView(GameObject looker)
   {
 
        //Vector3 targetDir = enemy.transform.position - looker.transform.position;
		//Vector3 targetDir = enemy[currentEnemy].position - looker.transform.position;
		Vector3 targetDir = target.position - looker.transform.position;
        // gets the direction to the enemy.
 
        float angle = Vector3.Angle(targetDir, looker.transform.forward);
        // gets the angle based on the direction.
 
        if (angle < maxAngle)
        {
            return true;
        }
        else
        {
            return false;
        }
   }
 
   bool EnemyInFieldOfViewNoResetPoint(GameObject looker)
   {
        //Vector3 targetDir = enemy.transform.position - looker.transform.position;
		//Vector3 targetDir = enemy[currentEnemy].position - looker.transform.position;
		Vector3 targetDir = target.position - looker.transform.position;
        float angle = Vector3.Angle(targetDir, looker.transform.forward);
 
        if (angle < maxAngleReset)
        {
            return true;
        }
        else
        {
            return false;
        }
   }
   //si el transform del player está mas cerca del transform del otro enemigo, cambio
   private void OnDrawGizmos() //Dibujar el campo de visión
   {
		
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, maxRadius);

		Vector3 fovLine1 = Quaternion.AngleAxis(maxAngle, transform.up) * transform.forward * maxRadius;
		Vector3 fovLine2 = Quaternion.AngleAxis(-maxAngle, transform.up) * transform.forward * maxRadius;

		Gizmos.color = Color.blue;
		Gizmos.DrawRay(transform.position, fovLine1);
		Gizmos.DrawRay(transform.position, fovLine2);

		if (!isInFov)
		{
			Gizmos.color = Color.red;
			//detected = false;
		}
		else
		{
			Gizmos.color = Color.green;
			//detected = true;
		}
		//Gizmos.DrawRay(transform.position, (target.position - transform.position).normalized * maxRadius);

		Gizmos.color = Color.black;
		Gizmos.DrawRay(transform.position, transform.forward * maxRadius);
   }

   public static bool inFOV(Transform checkingObject, Transform target2, float maxAngle, float maxRadius)
	{
		Collider[] overlaps = new Collider[100];
		int count = Physics.OverlapSphereNonAlloc(checkingObject.position, maxRadius, overlaps);

		for(int i = 0; i < count + 1; i++)
		{
			if(overlaps[i] != null)
			{
				if(overlaps[i].transform == target2)
				{
					Vector3 directionBetween = (target2.position - checkingObject.position).normalized;
					directionBetween.y *= 0;

					float angle = Vector3.Angle(checkingObject.forward, directionBetween);

					if(angle <= maxAngle)
					{
						Ray ray = new Ray(checkingObject.position, target2.position - checkingObject.position);
						RaycastHit hit;

						if(Physics.Raycast(ray, out hit, maxRadius))
						{
							if (hit.transform == target2)
							{ 
								return true;
							}
						}
					}
				}
			}
		}
		return false;
	}
}