using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMix : MonoBehaviour {


    public static AudioMix instance = null;

    public AudioMixer mixer;

    public float SFXlvl;
    public float musiclvl;
    public float puzzlvl;

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
    private void Start()
    {
        SetStartLevels();
    }
    void SetStartLevels() {
        SetSFXLvl(SFXlvl);
        SetMusicLvl(musiclvl);
        SetPuzzLvl(puzzlvl);
    }

    public void SetSFXLvl(float lvl) {
        SFXlvl = lvl;
        if (lvl == -60) {
            lvl = -80;
        }
            mixer.SetFloat("SFX", lvl);
        

    }
    public void SetMusicLvl(float lvl)
    {
        musiclvl = lvl;
        if (lvl == -60)
        {
            lvl = -80;
        }
        mixer.SetFloat("Music", lvl);
        
    }
    public void SetPuzzLvl(float lvl)
    {
        puzzlvl = lvl;
        if (lvl == -60)
        {
            lvl = -80;
        }
        mixer.SetFloat("PuzzleSFX", lvl);
        
    }

}
