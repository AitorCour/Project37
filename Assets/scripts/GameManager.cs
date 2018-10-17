using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
	private InputManager inputManager;
	public GameObject canvasPause;
	public GameObject canvasInventory;

	private void Awake () 
	{
		inputManager = GetComponent<InputManager>();
	}
	
	public void Pause()
	{
		inputManager.SetPause(true);
		Time.timeScale = 0;
		canvasPause.SetActive(true);
	}

	public void Resume()
	{
		inputManager.SetPause(false);
		Time.timeScale = 1;
		canvasPause.SetActive(false);
	}

	public void OpenInventory()
	{
		inputManager.SetInventory(true);
		Time.timeScale = 0;
		canvasInventory.SetActive(true);
	}

	public void CloseInventory()
	{
		inputManager.SetInventory(false);
		Time.timeScale = 1;
		canvasInventory.SetActive(false);
	}
	
}
