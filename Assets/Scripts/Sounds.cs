using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class Sounds : MonoBehaviour
{
    
    //lägger in componenten själv
    [SerializeField]
    AudioSource robotMovement, ambient1, ambient2, ambient3;

    int intNumber;
    //bool playRobotMovement = false;//INTE FRÅN ROAD TO HELL HELLER ^^




    // Start is called before the first frame update
    void Start() //here the audio and music variables are loaded and saved as playerprefs
    {
        //##VART BORDE DET HÄR SITTA SÅ ATT DE LADDAS OCH SPARAS EFTER MAN ÄNDRAT MED OPTIONS?


        /*
        public void Start() //in start playerpref-variables for the volume options are loaded
        {
            mixer.SetFloat("Volume", Mathf.Log10(PlayerPrefs.GetFloat("AudioVolume", 0.75f)) * 20);
            mixerTwo.SetFloat("Volume", Mathf.Log10(PlayerPrefs.GetFloat("MusicVolume", 0.75f)) * 20);
        }
        */

        slider.value = PlayerPrefs.GetFloat("AudioVolume", 0.75f);
        sliderTwo.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        SetMusic(PlayerPrefs.GetFloat("MusicVolume", 0.75f));
        SetVolume(PlayerPrefs.GetFloat("AudioVolume", 0.75f));

        //lägger in componentet
        //  robotMovement = GetComponent<AudioSource>();

        //### MÅSTE GÖRA SÅ ATT SCRIPTET ILGGER PÅ AUDIOSOURCEN + DEN SKA VARA NOT PLAY ON AWAKE MEN LOOP
        //robotMovement.Pause();//INTE FRÅN ROAD TO HELL

    }

    public AudioMixer mixer;
    [SerializeField]
    private Slider slider;

    public AudioMixer mixerTwo;
    [SerializeField]
    private Slider sliderTwo;

    public void SetVolume(float sliderValue) //this function sets and saves the audio volume
    {
        mixer.SetFloat("Volume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("AudioVolume", sliderValue);
    }
    public void SetMusic(float sliderValue)//this function sets and saves the music volume
    {
        mixerTwo.SetFloat("Volume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
    }



    //OVAN ÄR KOD FRÅN ROAD TO HELL^^


    // Update is called once per frame
    void Update()
    {

        //TOG BORT MUSIKEN FRÅN PLAY ON AWAKE FÖR ORKAR ITNTEL YSNA PÅ DEN RN
        
        //if the player/robot is on the ground and moves...
        if(CharacterController.grounded && (CharacterController.move < 0 || CharacterController.move > 0))
        {
            //if the robotMovement is not playing...
            if(!robotMovement.isPlaying)
            {
                //play the robot sound
                robotMovement.Play(0);

            }
        }
        else
        {
            //stop playing the robotMovement sound
            robotMovement.Stop();
        }






        //## LÄGGA TILL RANDOM WAIT TIME MELLAN ALLT SÅ ATT LJUDEN SPELAS AT RANDOM POINTS!
        //if the intNumber is equal to 1, if the intNumber is equal to 2, if the intNumber is equal to 3...
        if (intNumber == 1)
        {
            ambient1.Play(0);
        }
        else if(intNumber == 2)
        {
            ambient2.Play(0);
        }
        else if(intNumber == 3)
        {
            ambient3.Play(0);
        }

    }



    //This function chooses a random number between 1-3 whenever it is called, and sets the intNumber equal to the randomly picked number
    int GetRandom (int min, int max)
    {
        min = 1;
        max = 3;
        int rand = Random.Range(min, max);

        rand = intNumber;
        return rand;
    }
}
