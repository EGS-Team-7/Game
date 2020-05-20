using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public int scene;


    public void Play()
    {
        // Check and see whether the music is already playing
        // If not, play it
        AudioSource music = gameObject.GetComponent<AudioSource>();
        if (!music.isPlaying)
        {
            Debug.Log("STARTING MUSIC!");
            music.Play();
        }

        // Load the next level in the background
        StartCoroutine(Load());
    }
    IEnumerator Load()
    {
        yield return new WaitForSeconds(.2f);
        SceneManager.LoadScene(scene);
    }
}