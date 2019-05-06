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
	public GameObject canvasMap;
	public GameObject canvasInv;

	EventSystem eventSystem;
	public GameObject pauseButton;
    public GameObject pauseLayout;
	public GameObject invButton;
	public GameObject mapButton;
	public GameObject notes;

    /*public GameObject equipGun;
    public GameObject infoGun;

	public GameObject exBan;
	public GameObject infoBan;

	public GameObject exKey;
	public GameObject infoKey;

    public GameObject exKey2;
    public GameObject infoKey2;

    public GameObject exAmmo;
	public GameObject infoAmmo;

    public GameObject exBust;
    public GameObject infoBust;

    public GameObject exBox;
    public GameObject infoBox;

    public GameObject exBall;
    public GameObject infoBall;

    public GameObject exLader;
    public GameObject infoLader;

    public GameObject note1;
    public GameObject note2;*/

	public GameObject canvasOp;
	/*public GameObject screenOp;
	public GameObject soundOp;
	public GameObject controlsOp;

	public GameObject panelOp;

	public GameObject panelSubOp;*/

    public GameObject fadeObj;

    GameObject[] inventoryStuff;
    private SettingsMenu settings;

    private void Awake () 
	{
		//inputManager = GetComponent<InputManager>();
		eventSystem = EventSystem.current;
        //fadeObj.SetActive(true);
        Data.InitGameData();
    }
	void Start()
    {
        inputManager = GetComponent<InputManager>();
        settings = GameObject.FindGameObjectWithTag("HUD").GetComponent<SettingsMenu>();
        fadeObj.SetActive(true);
    }
	public void Pause()
	{
		inputManager.SetPause(true);
        settings.masterVol.SetFloat("Master", -20);
        Time.timeScale = 0;
		canvasPause.SetActive(true);
        pauseLayout.SetActive(true);
		eventSystem.SetSelectedGameObject(pauseButton);
		//Debug.Log("Paused");
	}

	public void Resume()
	{
		inputManager.SetPause(false);
        settings.masterVol.SetFloat("Master", settings.vol_1);
        Time.timeScale = 1;
        inventoryStuff = GameObject.FindGameObjectsWithTag("Inv");
        foreach (GameObject go in inventoryStuff)
        {
            go.SetActive(false);
        }
        canvasPause.SetActive(false);
		canvasOp.SetActive(false);
		/*screenOp.SetActive(false);
		soundOp.SetActive(false);
		controlsOp.SetActive(false);
		panelOp.SetActive(false);
		panelSubOp.SetActive(false);*/
	}

	public void OpenInventory()
	{
		inputManager.SetInventory(true);
		canvasInv.SetActive(true);
        settings.masterVol.SetFloat("Master", -20);
        Time.timeScale = 0;
		canvasInventory.SetActive(true);
		eventSystem.SetSelectedGameObject(invButton);
	}

	public void CloseInventory()
	{
        settings.masterVol.SetFloat("Master", settings.vol_1);
        Time.timeScale = 1;
        inventoryStuff = GameObject.FindGameObjectsWithTag("Inv");
        foreach (GameObject go in inventoryStuff)
        {
            go.SetActive(false);
        }
        inputManager.SetInventory(false);
		/*canvasInv.SetActive(false);
		map.SetActive(false);
		notes.SetActive(false);
        equipGun.SetActive(false);
        infoGun.SetActive(false);
		exBan.SetActive(false);
		infoBan.SetActive(false);
		exKey.SetActive(false);
		infoKey.SetActive(false);
        exKey2.SetActive(false);
        infoKey2.SetActive(false);
        exAmmo.SetActive(false);
		infoAmmo.SetActive(false);
        exBust.SetActive(false);
        infoBust.SetActive(false);
        exBox.SetActive(false);
        infoBox.SetActive(false);
        exBall.SetActive(false);
        infoBall.SetActive(false);
        exLader.SetActive(false);
        infoLader.SetActive(false);
        note1.SetActive(false);
        note2.SetActive(false);*/
    }
	
	public void OpenMap()
	{
		inputManager.SetMap(true);
        settings.masterVol.SetFloat("Master", -20);
        Time.timeScale = 0;
		canvasMap.SetActive(true);
		canvasInv.SetActive(false);
		canvasInventory.SetActive(true);
		eventSystem.SetSelectedGameObject(mapButton);
	}

	public void CloseMap()
	{
		inputManager.SetMap(false);
        settings.masterVol.SetFloat("Master", settings.vol_1);
        Time.timeScale = 1;
		canvasMap.SetActive(false);
		notes.SetActive(false);
	}
}
