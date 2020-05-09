using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    AudioSource music;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        music = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }
}
