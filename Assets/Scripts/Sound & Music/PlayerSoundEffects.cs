using UnityEngine;

public class PlayerSoundEffects : MonoBehaviour
{
    public AudioClip[] tracks;
    
    private static AudioClip[] staticTracks;
    private static AudioSource player;

    void Start()
    {
        player = GetComponent<AudioSource>();
        staticTracks = tracks;

        //TODO Set start sound effects from player prefs.
    }

    public static void PlaySoundEffect(int clipNumber)
    {
        AudioClip clip = staticTracks[clipNumber];
        player.clip = clip;
        player.Play();
    }
}
