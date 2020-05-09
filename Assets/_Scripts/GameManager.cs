using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    //score
    public TMP_Text scoreText;
    public int score;
    public ParticleSystem part;
    public GameObject gameOver;

    public void Score()
    {
        scoreText.text = "score: " + score;
        part.Play();
    }

    public void PlayerDeath()
    {
        gameOver.SetActive(true);
    }
}
