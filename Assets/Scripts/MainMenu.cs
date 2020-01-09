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
        SceneManager.LoadScene("SampleScene");
    }
    public void GoToOptions() //this function loads the "Options" scene
    {
        SceneManager.LoadScene("Options");
    }
}
