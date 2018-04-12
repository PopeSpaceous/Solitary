using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMix : MonoBehaviour {


    public static AudioMix instance = null;

    public AudioMixer mixer;

    public float SFXlvl = 0;
    public float musiclvl = 0;
    public float puzzlvl = 0;

    private void Awake()
    {
        //Set the instance only once.
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            //Enforces that there will always be one instance of the AudoMix gameObject, this is for type errors prevention.
            Destroy(gameObject);
            Debug.LogWarning("Another instance of AudoMix has been created and destoryed!");
        }
    }

    public void SetSFXLvl(float lvl) {
        mixer.SetFloat("SFX", lvl);
        SFXlvl = lvl;
    }
    public void SetMusicLvl(float lvl)
    {
        mixer.SetFloat("Music", lvl);
        musiclvl = lvl;
    }
    public void SetPuzzLvl(float lvl)
    {
        mixer.SetFloat("PuzzleSFX", lvl);
        puzzlvl = lvl;
    }

}
