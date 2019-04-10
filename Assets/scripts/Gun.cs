using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{

    public float maxDistance;
    public LayerMask mask;
	//private AudioSource sound;

	//public Vector3 direction = Vector3.forward;
    //private Transform transform.forward;
    private int maxAmmo = 7;
    public int currentAmmo;
    public int Munition;
    private int iniMunition = 0;
	private int variableM;
    public float fireRate;
    public float hitForce;
    public int hitDamage;

    public bool isShooting;
    public bool isReloading;

    public float ReloadTime;

	public ParticleSystem particleShoot;
	public ParticleSystem particleSteam;

    public Animator animator;

    private PlayerBehaviour plBehaviour;

	// Use this for initialization
	void Start ()
    {
        isShooting = false;
        isReloading = false;
        //currentAmmo = maxAmmo;
        //Munition = iniMunition;
        plBehaviour = GetComponentInParent<PlayerBehaviour>();

        if (Data.ammoSet)
        {
            currentAmmo = Data.ammo;
            //hud.SetLife(playerLife);
        }
        else currentAmmo = maxAmmo;
        if (Data.munitionSet)
        {
            Munition = Data.munition;
            //hud.SetLife(playerLife);
        }
        else Munition = iniMunition;
		//sound = GetComponentInChildren<AudioSource>();
	}
    public void Shot()
    {
        if(isShooting || isReloading) return;
        if(currentAmmo <= 0)
        {
            plBehaviour.NoShootSound();
        }
        animator.SetTrigger("Shooting");
        plBehaviour.ShootSound();
        isShooting = true;
        currentAmmo--;
        Data.SetAmmo(currentAmmo);

		particleShoot.Play();
		particleSteam.Play();

        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Coje el punto de la posicion del mouse y lanza un rayo
        RaycastHit hit = new RaycastHit();

        if(Physics.Raycast(transform.position,  transform.forward, out hit, maxDistance, mask))
        {
            Debug.Log(hit.transform.name);
            Debug.DrawRay(transform.position, transform.forward * maxDistance, Color.red, 10.0f);
			EnemyBehaviour3 enemyHealth = hit.collider.GetComponent<EnemyBehaviour3>();
			
            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce( (transform.forward) * hitForce, ForceMode.Impulse);
            }
			if(enemyHealth != null)
			{
				enemyHealth.ReciveDamage(hitDamage, hit.point);
			}
        }
        StartCoroutine(WaitFireRate());
    }

    private IEnumerator WaitFireRate() //Usar corutinas para contar tiempo
    {
        yield return new WaitForSeconds(fireRate);
        isShooting = false;

       // yield return null;//cierra la corutina
    }

    public void Reload()
    {
        if(isReloading) return;
        if(Munition <= 0 ) return;
		if(currentAmmo == maxAmmo) return;
        isReloading = true;
        // animacion.SetTrigger("recharge");
        //reload.SetTrigger ("reload");
        StartCoroutine(WaitForReload());
		animator.SetTrigger("Reloading");
        //Data.SetAmmo(currentAmmo);
    }

    private IEnumerator WaitForReload()
    {
        yield return new WaitForSeconds(ReloadTime);
		if (currentAmmo > 0)
		{
			variableM = maxAmmo - currentAmmo;
			if(variableM > Munition)
			{
				currentAmmo += Munition;
				Munition = 0;
			}
			if(variableM <= Munition)
			{
				Munition -= variableM;
				currentAmmo += variableM;
			}
		}
		else if (Munition >= maxAmmo)
		{
			currentAmmo = maxAmmo;
			Munition -= maxAmmo;
		}
        else if (Munition < maxAmmo)
		{
			currentAmmo = Munition;
			Munition = 0;
		}
        isReloading = false;
        Data.SetAmmo(currentAmmo);
        Data.SetMunition(Munition);
    }

    public void GetAmmo(int m)
    {
        Munition += m;
        Data.SetMunition(Munition);
    }

	/*public void PrecisionShot()
	{
		if(isShooting || isReloading) return;
        if(currentAmmo <= 0) return;
		animator.SetTrigger("Shooting");
        isShooting = true;
        currentAmmo--;

		particleShoot.Play();
		particleSteam.Play();
        RaycastHit hit = new RaycastHit();
        if(Physics.Raycast(transform.position,  transform.forward, out hit, maxDistance, mask))
        {
            Debug.Log(hit.transform.name);
            Debug.DrawRay(transform.position, transform.forward * maxDistance, Color.red, 10.0f);
			EnemyBehaviour3 enemyHealth = hit.collider.GetComponent<EnemyBehaviour3>();

            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce( (transform.forward) * hitForce, ForceMode.Impulse);
            }
			if(enemyHealth != null)
			{
				//Cambiar para llamar a Dead
				//El fire Rate es mas tiempo
				//Se activará automaticamente este disparo a los dos segundos de estar en modo apuntado
				enemyHealth.DeadlyDamage(hit.point);
			}
        }
        StartCoroutine(WaitFireRate());
	}*/
    public void GodMode()
    {
        Munition = 999;
    }
}
