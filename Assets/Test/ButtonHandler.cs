using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    ///<summary>Placeholder delegate function for our buttonList</summary>
    public delegate void ButtonAction();
    ///<summary>Array of buttons, created from a struct, below.</summary>
    public MyButton[] buttonList;
    ///<summary>Index reference to our currently selected button.</summary>
    public int selectedButton = 0;

    void Start()
    {
        // Instantiate buttonList to hold the amount of buttons we are using.
        buttonList = new MyButton[3];
        // Set up the first button, finding the game object based off its name. We also 
        // must set the expected onClick method, and should trigger the selected colour.
        buttonList[0].image = GameObject.Find("PlayButton").GetComponent<Image>();
        buttonList[0].image.color = Color.yellow;
        buttonList[0].action = PlayButtonAction;
        // Do the same for the second button. We are also ensuring the image colour is
        // set to our normalColor, to ensure uniformity.
        buttonList[1].image = GameObject.Find("OptionsButton").GetComponent<Image>();
        buttonList[1].image.color = Color.white;
        buttonList[1].action = OptionsButtonAction;

		buttonList[2].image = GameObject.Find("OptionsButton1").GetComponent<Image>();
        buttonList[2].image.color = Color.white;
        buttonList[2].action = OptionsButtonAction;
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
            buttonList[selectedButton].action();
        }
    }

    void MoveToNextButton()
    {
        // Reset the currently selected button to the default colour.
        buttonList[selectedButton].image.color = Color.white;
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
        buttonList[selectedButton].image.color = Color.yellow;
    }

    void MoveToPreviousButton()
    {
        // Should be self explanatory; similar in function to MoveToNextButton,
        // but instead, we are moving back a button.
        buttonList[selectedButton].image.color = Color.white;
        selectedButton--;
        if(selectedButton < 0)
        {
            selectedButton = (buttonList.Length - 1);
        }
        buttonList[selectedButton].image.color = Color.yellow;
    }

    ///<summary>This is the method that will call when selecting "Play".</summary>
    void PlayButtonAction()
    {
        Debug.Log("Play");
    }

    ///<summary>This is the method that will call when selecting "Options".</summary>
    void OptionsButtonAction()
    {
        Debug.Log("Options");
    }

    ///<summary>A struct to represent individual buttons. This makes it easier to wrap
    /// the required variables into a single container. Don't forget 
    /// [System.Serializable], if you wish to see your final array in the inspector.
    [System.Serializable]
    public struct MyButton
    {
        /// <summary>The image contained in the button.</summary>
        public Image image;
        /// <summary>The delegate method to invoke on action.</summary>
        public ButtonAction action;
    }
}