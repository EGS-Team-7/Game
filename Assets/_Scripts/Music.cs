using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    AudioSource music;
    private static Music _musicPlayer;          // Because this is _static_, it does not belong to any object
                                                // Allowing us to detect if there are duplicates of this class

    private void Awake()
    {
        if (_musicPlayer)
        {
            // If there is already a reference to a music player,
            // Then we need to destroy ourselves to prevent the music from
            // playing again

            Destroy(gameObject);
        }
        else
        {
            // There is no reference to a music player, so we must be it
            // We set ourselves to be that reference, set ourselves to not be destroyed,
            // And start playing our song.

            _musicPlayer = this;
            DontDestroyOnLoad(gameObject);
            music = gameObject.GetComponent<AudioSource>();
            if (!music.isPlaying)
            {
                music.Play();
            }
        }
    }
}
