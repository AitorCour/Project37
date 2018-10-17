using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler2 : MonoBehaviour
{
    public int selectedButton = 0;
    public Image[] buttonList;

    void Start()
    {
        buttonList = GetComponentsInChildren<Image>();
        buttonList[0].color = Color.yellow;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveToNextButton();
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveToPreviousButton();
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            this.BroadcastMessage(buttonList[selectedButton].name + "Action");
        }
    }

    /// <summary>Identical to first example.</summary>
    void MoveToNextButton() 
	{
		 // Reset the currently selected button to the default colour.
        buttonList[selectedButton].color = Color.white;
        // Increment our selected button index by 1.
        selectedButton++;
        // Check that our new index does not move outside of our array.
        if(selectedButton >= buttonList.Length)
        {
            // If you want to reset to the first button, reset the index.
            selectedButton = 0;
            // If you do not, simply move it back by 1, instead.
        }
        // Set the currently selected button to the "selected" colour.
        buttonList[selectedButton].color = Color.yellow;
	}

    /// <summary>Identical to first example.</summary>
    void MoveToPreviousButton() 
	{
		// Should be self explanatory; similar in function to MoveToNextButton,
        // but instead, we are moving back a button.
        buttonList[selectedButton].color = Color.white;
        selectedButton--;
        if(selectedButton < 0)
        {
            selectedButton = (buttonList.Length - 1);
        }
        buttonList[selectedButton].color = Color.yellow;
	}

    /// <summary>Will call when user selects "PlayButton".</summary>
    void PlayButtonAction() 
	{
		Debug.Log("Play");
	}

    /// <summary>Will call when user selects "OptionsButton"</summary>
    void OptionsButtonAction() 
	{
		Debug.Log("Options");
	}
}
