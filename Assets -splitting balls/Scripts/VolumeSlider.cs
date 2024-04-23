using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider s1;
    [SerializeField] private TMP_Text t1;
    [SerializeField] public AudioSource backgroundMusic;    

    // Start is called before the first frame update
    void Start()
    {
        //have to relink with actual music
        if (backgroundMusic == null){
            Debug.Log("Slider: Creating link to original AudioSource Object");
            backgroundMusic = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioSource>();
        }
        CheckVolumePrefs();
    }


    public void CheckVolumePrefs(){
        if(!PlayerPrefs.HasKey("VolumeLv")){
            Debug.Log("Creating PlayerPrefs.VolumeLv");
            PlayerPrefs.SetFloat("VolumeLv",50);
        }
        else{
            //Debug.Log(PlayerPrefs.GetInt("VolumeLv"));
            s1.value = PlayerPrefs.GetFloat("VolumeLv");
        }

    }

    public void SetVolume(){
        //Debug.Log(s1.value);
        t1.text = s1.value.ToString();
        PlayerPrefs.SetFloat("VolumeLv", (int)s1.value);
        backgroundMusic.volume = PlayerPrefs.GetFloat("VolumeLv")/100;
    }
}