using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    public GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
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
