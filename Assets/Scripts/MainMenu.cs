﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class MainMenu : MonoBehaviour
{

    public Animator transition;

    public Animator titleTransition;

    public Animator startButton;

    public Animator optionsButton;

    public Animator quitButton;

    public float transitionTime = 1f;

    float timer = 0;
    public bool timerReached = false;
    float sceneDelay = 1.5f;
    bool hasOptions = false;





    public void QuitGame() //This function closes the application when triggered
    {
        Application.Quit();
    }

    private void Update()
    {
        if (hasOptions)
        {
            timer += Time.deltaTime;
        }
        if (timer > sceneDelay)
        {
            SceneManager.LoadScene("OptionsMenu");
            timer = 0;
            hasOptions = false;
        }
    }

    public void GoToOptions() //this function loads the "Options" scene
    {
        hasOptions = true;
        optionsButton.SetTrigger("ClickOptions");

        



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
            EquipItems.pickedUp = false;
            EquipItems.objectDraged = false;
            EquipItems.objectLeft = false;
            EquipItems.objectRight = false;
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

        titleTransition.SetTrigger("TitleStart");
        startButton.SetTrigger("ClickStart");
        optionsButton.SetTrigger("ClickOptions");
        quitButton.SetTrigger("ClickQuit");



        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }



    //fixa en funktion som unloadar ESC MENU
    //fixa så att karaktären och allt annat är pausat under esc menu
    //fixa så att det un-pausar när esc menu är unloadat




}
