using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    public GameManager gameManager;

    private void Start()
    {
        try
        {
            gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        }
        catch
        {
            Debug.Log("No Gamemanager found");
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if(other.layer == 11)
        {
            gameManager.PlayerDeath();
            gameObject.GetComponent<AudioSource>().Play();
            other.SetActive(false);
        }
    }
}
