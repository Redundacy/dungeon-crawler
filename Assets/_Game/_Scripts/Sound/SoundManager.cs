using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{   
    // The audio itself
    public AudioSource audioSource;

    // The slider of the music
    public Slider volumeSlider;
    
    // Default musicVolume
    private float musicVolume = 0.5f;

    // Start is called before the first frame update
    void Awake()
    {   
        if(PlayerPrefs.HasKey("volume")){
            musicVolume = PlayerPrefs.GetFloat("volume");
        }

        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("GameMusic");
        if(musicObj.Length > 1){
            Destroy(this.gameObject);
        }
        audioSource.volume = musicVolume;
        volumeSlider.value = musicVolume;
        DontDestroyOnLoad(this.gameObject);
    }

    void Update(){
        if(SceneManager.GetActiveScene().name == "Game"){
            Destroy(this.gameObject);
        }

        audioSource.volume = musicVolume;
        // Save the value into the playerPrefs
        PlayerPrefs.SetFloat("volume", musicVolume);
    }

    public void UpdateVolume(float volume){
        musicVolume = volume;
    }
}
