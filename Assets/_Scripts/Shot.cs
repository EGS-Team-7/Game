using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public GameManager gameManager;     // Where the score is held

    ParticleSystem part;                // What collides with other objects

    private void Start()
    {
        part = gameObject.GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other)
    {
        // Only targets enemies (designated by layer 10)
        if (other.layer == 10)
        {
            // Get the score
            int val;
            try
            {
                val = other.GetComponent<EnemyController>().scoreValue;
            }
            catch
            {
                val = 100;
            }

            // Tell the Gamemanager
            gameManager.AddScore(val);
            gameManager.Score();

            // Kill the target
            other.GetComponent<EnemyController>().Death();
        }
    }
}
