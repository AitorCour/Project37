using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
	public GameObject myCamera;
	private CameraManager myCameraManager;
	// Use this for initialization
	void Start ()
    {
		myCameraManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<CameraManager>();
	}
	
	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player")
		{
			myCameraManager.DeactivateAllCameras();
			myCamera.SetActive(true);
            myCameraManager.GetActiveCamera();
			//Debug.Log("Trigger");
		}
	}
}
