// Company: The Puzzlers
// Copyright (c) 2018 All Rights Reserved
// Author: Leonel Jara
// Date: 04/13/2018
/* Summary: 
 * For managing the audio level using a mixer.
 * This is used by the pause main menu for now.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMix : MonoBehaviour {


    public static AudioMix instance = null;

    //Ref to the ixer
    public AudioMixer mixer;

    //Starting values for each sound group
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
        //Set the audio level with starting values
        SetStartLevels();
    }
    void SetStartLevels() {
        SetSFXLvl(SFXlvl);
        SetMusicLvl(musiclvl);
        SetPuzzLvl(puzzlvl);
    }
    //Set SFX volume
    public void SetSFXLvl(float lvl) {
        SFXlvl = lvl;
        if (lvl == -60) {
            lvl = -80;
        }
            mixer.SetFloat("SFX", lvl);
        

    }
    //Set music volume
    public void SetMusicLvl(float lvl)
    {
        musiclvl = lvl;
        if (lvl == -60)
        {
            lvl = -80;
        }
        mixer.SetFloat("Music", lvl);
        
    }
    //set puzzle sfx volume
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
