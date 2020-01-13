using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void QuitGame() //This function closes the application when triggered
    {
        Application.Quit();
    }
    public void PlayGame() //This function starts the game by loading the first "Level" scene
    {
        SceneManager.LoadScene("NewCodeScene");
    }
    public void GoToOptions() //this function loads the "Options" scene
    {
        SceneManager.LoadScene("Options");
    }

    public void BackToMainMenu() //this function loads the "MainMenu" scene again
    {
        SceneManager.LoadScene("MainMenu");
    }

    //fixa en funktion som unloadar ESC MENU
    //fixa så att karaktären och allt annat är pausat under esc menu
    //fixa så att det un-pausar när esc menu är unloadat
}
