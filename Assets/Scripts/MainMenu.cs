using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    //Erik {
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
    //Erik }


    //Harriet {
    [SerializeField]
    private string levelName;
    [SerializeField]
    private bool unloadScene;
    [SerializeField]
    private string unloadLevelName;
    [SerializeField]
    private bool loadingLevel;

    public static bool playing;

    public AudioMixer mixerTwo;
    public AudioMixer mixer;
    //Harriet }

    //Harriet {
    public void Start() //in start playerpref-variables for the volume options are loaded
    {
        mixer.SetFloat("AudioVol", Mathf.Log10(PlayerPrefs.GetFloat("AudioVolume", 0.75f)) * 20);
        mixerTwo.SetFloat("MusicVol", Mathf.Log10(PlayerPrefs.GetFloat("MusicVolume", 0.75f)) * 20);

        EquipItems.pickedUp = false;  
        EquipItems.objectDraged = false;            //When Starting the game, the variables that check certain conditions...
        EquipItems.objectLeft = false;              //...(flower being picked up, dragging / pushing objects... 
        EquipItems.objectRight = false;             //...and facing directions) will be reset - Erik
        CharacterController.facingRight = true;

    }
    //Harriet }

    public void QuitGame() //This function closes the application when triggered
    {
        Application.Quit();
    }

    private void Update()
    {
        //Harriet {
        //Should lock and hide the cursor every frame, (though Unity shows the mouse anyways since we're using the "Escape" key to load a scene, and unit uses escape to show the mouse cursor)
        Cursor.lockState = CursorLockMode.Locked;
        if (SceneManager.GetSceneByName(levelName).isLoaded)
        {
            loadingLevel = false;
        }

        #region playing game or in menu

        //if EscMenu is not loaded and one of the game scenes is loaded...
        if (!SceneManager.GetSceneByName("EscMenu").isLoaded && (SceneManager.GetSceneByName("Level 1").isLoaded || SceneManager.GetSceneByName("Bara för aesthetic").isLoaded || SceneManager.GetSceneByName("Level 2").isLoaded) || SceneManager.GetSceneByName("Level 3").isLoaded)
        {
            playing = true;
        }
        //if EscMenu is loaded...
        else if (SceneManager.GetSceneByName("EscMenu").isLoaded)
        {
            playing = false;
        }
        #endregion

        #region invisible cursor during playing, visible cursor within the menues -not used anymore //Harriet
        /* if (playing)
         {
             Cursor.visible = false;
         }
         else
         {
           Cursor.visible = true;
       } */

        #endregion

        //if the player presses escape...
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //then the function "EscMenu" will be started
            EscMenu();
        }
        //Harriet }

        //Erik {
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

        if (hasBackToMenu)
        {
            optionsTimer += Time.deltaTime;
        }

        if (optionsTimer > backToMenuDelay)
        {
            SceneManager.LoadScene("MainMenu");
            optionsTimer = 0;
            hasBackToMenu = false;
        }
        // Erik }

    }

    //this function loads the "Options" scene
    public void GoToOptions()
    {
        hasOptions = true;
        optionsButton.SetTrigger("ClickOptions");

        transition.SetTrigger("Start");

    }

    //this function loads the "MainMenu" scene again
    public void BackToMainMenu()
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



    //This function starts the game by loading the first "Level" scene
    public void PlayGame()
    {
        LoadNextLevel();
    }

    //Metod som startar en coroutine, och sedan accessar scenen + transitionar till "scenIndex" + 1
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));

    }

    //Harriet {
    public void EscMenu()
    {
        //if the scene assigned to the variable LevelName is not already loaded then...
        if (!SceneManager.GetSceneByName(levelName).isLoaded)
        {
            //Loads scene as an added scener (so the previous one is still loaded, and active in the background)
            SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
            loadingLevel = true;
            //if unloadscene is true and the level assigned to unloadLevelName is loaded then...
            if (unloadScene && SceneManager.GetSceneByName(unloadLevelName).isLoaded)
            {
                //the level assigned to unloadLevelName will be unloaded
                SceneManager.UnloadSceneAsync(unloadLevelName);
            }
        }
    }
    //Unloads the EscMenu when called
    public void Back()
    {
        SceneManager.UnloadSceneAsync("EscMenu");
    }
    //Harriet }

    //Erik {
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
    //Erik }

}
