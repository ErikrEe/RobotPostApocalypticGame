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


    public void GoToOptions() //this function loads the "Options" scene
    {
        SceneManager.LoadScene("OptionsMenu");
    }


    public void BackToMainMenu() //this function loads the "MainMenu" scene again
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }




    public void PlayGame() //This function starts the game by loading the first "Level" scene
    {
        LoadNextLevel();
    }


    public void LoadNextLevel() //Metod som startar en coroutine, och sedan accessar scenen + transitionar till "scenIndex" + 1
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));

    }

    //Coroutine nedan som triggar allting, spelar animation, väntar en sekund, loadar scenen

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }



    //fixa en funktion som unloadar ESC MENU
    //fixa så att karaktären och allt annat är pausat under esc menu
    //fixa så att det un-pausar när esc menu är unloadat




}
