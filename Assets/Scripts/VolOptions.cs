using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class VolOptions : MonoBehaviour
{

    //Harriet's Script

    //to find the audio mixers and sliders
    public AudioMixer mixer;
    public Slider slider;
    public AudioMixer mixerTwo;
    public Slider sliderTwo;


    // Start is called before the first frame update
    void Start() //here the audio and music variables are loaded with a deafult float of 0.75f 
    {
        slider.value = PlayerPrefs.GetFloat("AudioVolume", 0.75f);
        sliderTwo.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        SetMusic(PlayerPrefs.GetFloat("MusicVolume", 0.75f));
        SetVolume(PlayerPrefs.GetFloat("AudioVolume", 0.75f));
    }

    public void SetVolume(float sliderValue) //this function sets and saves the audio volume
    {
        mixer.SetFloat("AudioVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("AudioVolume", sliderValue);
    }
    public void SetMusic(float sliderValue)//this function sets and saves the music volume
    {
        mixerTwo.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
    }

}
