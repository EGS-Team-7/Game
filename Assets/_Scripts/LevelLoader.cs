using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public int scene;
    public void Play()
    {
        gameObject.GetComponent<AudioSource>().Play();
        StartCoroutine(Load());
    }
    IEnumerator Load()
    {
        yield return new WaitForSeconds(.2f);
        SceneManager.LoadScene(scene);
    }
}