







using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;








/// <summary>
/// UI Component, allows a button to change the level to a new one
/// </summary>
public class LevelLoader : MonoBehaviour
{



    public int scene;




    /// <summary>
    /// UI Button a player presses with effects and no latency
    /// </summary>
    public void Play()
    {
        // Play the "select" sound
        gameObject.GetComponent<AudioSource>().Play();

        // Load the next level in the background
        StartCoroutine(Load());
    }




    /// <summary>
    /// Loads the selected scene in the background
    /// </summary>
    /// <returns>Instructions to load the next scene</returns>
    IEnumerator Load()
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(scene);
    }
}