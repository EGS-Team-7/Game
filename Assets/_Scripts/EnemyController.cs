using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int scoreValue;
    public float DeathTimer;

    bool die = false;

    public ParticleSystem[] Shots;
    public ParticleSystem dead;

    private void Start()
    {
    }

    void Update()
    {
        if(die == true)
        {
            DeathTimer -= Time.deltaTime;
            if (DeathTimer <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Death()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().isTrigger = true;

        for (int i = 0; i < Shots.Length; i++)
        {
            var em = Shots[i].emission;
            em.enabled = false;
        }
        dead.Play();
        gameObject.GetComponent<AudioSource>().Play();
        die = true;
    }
}
