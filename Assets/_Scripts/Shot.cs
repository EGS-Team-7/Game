using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public GameManager gameManager;

    ParticleSystem part;

    private void Start()
    {
        part = gameObject.GetComponent<ParticleSystem>();
    }

    private void Update()
    {

    }

    private void OnParticleCollision(GameObject other)
    {
        int val = other.GetComponent<EnemyController>().scoreValue;
        gameManager.score += val;
        gameManager.Score();
        if (other.layer == 10)
            other.GetComponent<EnemyController>().Death();
    }
}
