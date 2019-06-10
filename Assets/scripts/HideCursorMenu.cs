using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideCursorMenu : MonoBehaviour 
{
	private MouseCursor mouseCursor;

	// Use this for initialization
	void Start () 
	{
		mouseCursor = new MouseCursor();
		mouseCursor.HideCursor();
	}
	
	// Update is called once per frame
	/*void Update () 
	{
		if(Input.GetMouseButtonDown(0)) mouseCursor.HideCursor();
		
        else if(Input.GetKeyDown(KeyCode.Escape)) mouseCursor.ShowCursor();
	}*/
}
