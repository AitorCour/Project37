using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class GameManager : MonoBehaviour 
{
	private InputManager inputManager;
	public GameObject canvasPause;
	public GameObject canvasInventory;
	EventSystem eventSystem;
	public GameObject pauseButton;
	public GameObject invButton;

	public GameObject inventory;
	public GameObject map;
	public GameObject notes;
	
	private void Awake () 
	{
		inputManager = GetComponent<InputManager>();
		eventSystem = EventSystem.current;
	}
	
	public void Pause()
	{
		inputManager.SetPause(true);
		Time.timeScale = 0;
		canvasPause.SetActive(true);
		eventSystem.SetSelectedGameObject(pauseButton);
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
		eventSystem.SetSelectedGameObject(invButton);
	}

	public void CloseInventory()
	{
		inputManager.SetInventory(false);
		Time.timeScale = 1;
		canvasInventory.SetActive(false);
		inventory.SetActive(true);
		map.SetActive(false);
		notes.SetActive(false);
	}
	

}
