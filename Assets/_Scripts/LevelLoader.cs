using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public int scene;


    public void Play()
    {
        // Play the "select" sound
        gameObject.GetComponent<AudioSource>().Play();

        // Load the next level in the background
        StartCoroutine(Load());
    }
    IEnumerator Load()
    {
        yield return new WaitForSeconds(.2f);
        SceneManager.LoadScene(scene);
    }
}