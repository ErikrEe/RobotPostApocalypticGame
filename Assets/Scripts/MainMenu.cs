using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public Animator transition;

    public float transitionTime = 1f;


    public void QuitGame() //This function closes the application when triggered
    {
        Application.Quit();
    }





    public void PlayGame() //This function starts the game by loading the first "Level" scene
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start"); //Triggers the animation

        yield return new WaitForSeconds(transitionTime); //Pauses Coroutine for set amount of seconds

        SceneManager.LoadScene(levelIndex);
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
