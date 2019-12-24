using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour 
{
	public GameObject[] cameras;
	public GameObject startCamera;
    public GameObject actualCamera;
    private CameraBasedMovement movement;
	// Use this for initialization
	void Start () 
	{
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<CameraBasedMovement>();
		cameras = GameObject.FindGameObjectsWithTag("Camera");
		for(int i = 0; i < cameras.Length; i++)
		{
			cameras[i].SetActive(false);
		}
		startCamera.SetActive(true);
        actualCamera = startCamera;
        GetActiveCamera();
	}
	
	public void DeactivateAllCameras()
	{
		for(int i = 0; i < cameras.Length; i++)
		{
			cameras[i].SetActive(false);
		}
	}
    public void GetActiveCamera()
    {
        for(int i = 0; i < cameras.Length; i++)
        {
            if(cameras[i].activeInHierarchy)
            {
                actualCamera = cameras[i];
            }
        }
        movement.ChangeCamera();
    }
}
