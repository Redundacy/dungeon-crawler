using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{   
    public AudioSource audioSource;

    public Slider volumeSlider;
    private float musicVolume = 0.5f;
    // Start is called before the first frame update
    void Awake()
    {   
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameMusic");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        // After that , set up the audio
        if(PlayerPrefs.HasKey("volume")){
            musicVolume = PlayerPrefs.GetFloat("volume");
        }
        audioSource.volume = musicVolume;
        volumeSlider.value = musicVolume;
    }

    void Update()
    {
        //if (SceneManager.GetActiveScene().name == "Game")
        if (SceneManager.GetActiveScene().name == "3DTesting")
        {
            Destroy(this.gameObject);
        }

        audioSource.volume = musicVolume;
        // Save the value into the playerPrefs
        PlayerPrefs.SetFloat("volume", musicVolume);
    }

    public void UpdateVolume(float volume){
        //musicVolume = volume;
        musicVolume = volumeSlider.value;
    }

}
