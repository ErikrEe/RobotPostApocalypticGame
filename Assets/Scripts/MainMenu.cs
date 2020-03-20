using System.Collections;
using UnityEngine;
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
    //Harriet }

    public void QuitGame() //This function closes the application when triggered
    {
        Application.Quit();
    }

    private void Update()
    {
        //Harriet {
        Cursor.lockState = CursorLockMode.Locked;
        if (SceneManager.GetSceneByName(levelName).isLoaded)
        {
            loadingLevel = false;
        }

        #region invisible cursor during playing, visible cursor within the menues - not used anymore //Harriet
        /*          
          //när leveln är loadad och esc menu inte är loadad.
          if (!SceneManager.GetSceneByName("EscMenu").isLoaded && (SceneManager.GetSceneByName("Level 1").isLoaded || SceneManager.GetSceneByName("Bara för aesthetic").isLoaded || SceneManager.GetSceneByName("Level 2").isLoaded))//game är loaded och escmenu inte är loaded
          {
              //playing är true 
              playing = true;
          }
          else if (SceneManager.GetSceneByName("EscMenu").isLoaded)
          {
              playing = false;
          }
          if (playing)
          {
              Cursor.visible = false;
          }
          else
          {
                Cursor.visible = true;
          } 
          */
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
