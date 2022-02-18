using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utility;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; set; }
	private AudioSource player;
    //public List<TrackInformation> overWorldTracks;
    //public List<TrackInformation> combatTracks;

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
    
    void Start ()
    {
		player = GetComponent<AudioSource>();
    }
	
	public void PlayMusic (AudioClip clip, bool loop)
    {
        player.clip = clip;

        if (loop)
        {
            player.loop = true;
        }
        else
        {
            player.loop = false;
        }

        player.Play();
    }

    private IEnumerator FadeMusicIn ()
    {
        float targetVolume = PlayerPrefsManager.Instance.GetMasterMusicVolume();
        float fadeTime = .5f;

        while (player.volume < targetVolume)
        {
            player.volume += (targetVolume / fadeTime) * Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator FadeMusicOut()
    {
        float initialVolume = player.volume;
        float fadeTime = .75f;

        while (player.volume > 0)
        {
            player.volume -= (initialVolume / fadeTime) * Time.deltaTime;
            yield return null;
        }
    }
	
	public void ChangeVolume (float volume)
    {
		player.volume = volume;
	}

    public void StartCombatMusic(AudioClip audioClip)
    {
        PlayMusic(audioClip, true);
        StartCoroutine(FadeMusicIn());
    }
}

//[Serializable]
//public struct TrackInformation
//{
//    public Location location;
//    public TrackScenario scenario;
//    public AudioClip clip;
//}