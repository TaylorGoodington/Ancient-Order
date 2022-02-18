using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour 
{
    public static PlayerPrefsManager Instance { get; set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
    }

    //TODO Actually write this...
    public float GetMasterMusicVolume()
    {
        return 0.75f;
    }

}
