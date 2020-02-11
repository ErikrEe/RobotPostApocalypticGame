using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class MainMenu : MonoBehaviour
{

    //Behöver stänga av movement speed etc när escMenu är aktiv, Equip Items scriptet interferar förmodligen då det ändrar movement speed etc, kanske stnga av scripten?? vem vet tbh..

    public Animator backToMainMenu;

    public Animator transition;

    public Animator titleTransition;

    public Animator startButton;

    public Animator optionsButton;

    public Animator quitButton;

    public float transitionTime = 1f;

    private float timer = 0;
    private float sceneDelay = 1.5f;
    private bool hasOptions = false;

    private bool hasBackToMenu = false;
    private float optionsTimer = 0;
    private float backToMenuDelay = 1.5f;

    [SerializeField]
    private string levelName;
    [SerializeField]
    private bool unloadScene;
    [SerializeField]
    private string unloadLevelName;
    [SerializeField]
    private bool loadingLevel;

    public bool playing;


    public void QuitGame() //This function closes the application when triggered
    {
        Application.Quit();
    }

    private void Update()
    {
        if (SceneManager.GetSceneByName(levelName).isLoaded) loadingLevel = false;

        if(!SceneManager.GetSceneByName("EscMenu").isLoaded && SceneManager.GetSceneByName("Level 1").isLoaded || SceneManager.GetSceneByName("Bara för aesthetic").isLoaded)//game är loaded och escmenu inte är loaded
        {
            playing = true;
        }

        if(playing) //playing är true när leveln är loadad och esc menu inte är loadad.
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
            //stäng av movement Och controller scripten
            //EquipItems.playerVelocity.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.LogError("EscMenu blir nu loadad");
            EscMenu();
            //player.constraints = RigidbodyConstraints2D.FreezeAll; göra så att player inte kan röra på sig.

        }


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




        if(hasBackToMenu)
        {
            optionsTimer += Time.deltaTime;
        }

        if(optionsTimer > backToMenuDelay)
        {
            SceneManager.LoadScene("MainMenu");
            optionsTimer = 0;
            hasBackToMenu = false;
        }




    }

    public void GoToOptions() //this function loads the "Options" scene
    {
        hasOptions = true;
        optionsButton.SetTrigger("ClickOptions");

        transition.SetTrigger("Start");

        



    }


    public void BackToMainMenu() //this function loads the "MainMenu" scene again
    {
        hasBackToMenu = true;
        transition.SetTrigger("Start");
        backToMainMenu.SetTrigger("backToMenu");

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

    public void EscMenu()
    {
        //Using the below code for a back button thing
        if (!SceneManager.GetSceneByName(levelName).isLoaded)// && !loadingLevel) Den verkade fucka upp allt..
        /*If scene is not already loaded, and the script is not already loading a level*/
        {
            Debug.Log(gameObject.name + " is trying to load" + levelName);
            SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive); //Loads scene
            loadingLevel = true;
            if (unloadScene && SceneManager.GetSceneByName(unloadLevelName).isLoaded)
            /*If the level that it is configured to unload is already loaded and unloadScene is ticked*/
            {
                SceneManager.UnloadSceneAsync(unloadLevelName); //Unloads level
            }
        }
    }

    public void Back()
    {
        SceneManager.UnloadSceneAsync("EscMenu");

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



    //fixa en funktion som unloadar ESC MENU- check
    //fixa så att karaktären och allt annat är pausat under esc menu
    //fixa så att det un-pausar när esc menu är unloadat




}
