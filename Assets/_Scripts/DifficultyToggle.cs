







using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;








/// <summary>
/// UI Component, allows a button to change the difficulty
/// </summary>
public class DifficultyToggle : MonoBehaviour
{



    [Header("Changes the Difficulty Mode")]

    private GameManager _gameManager;           // Where the difficulty is set
    private TextMeshPro _difficultyModeText;    // Where we communicate the selected difficulty




    public void Awake()
    {
        // Find the GameManager
        try
        {
            _gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        }
        catch
        {
            Debug.Log("No Gamemanager found");
        }

        // Find our Text Component
        _difficultyModeText = gameObject.transform.Find("DifficultyText (TMP)").GetComponent<TextMeshPro>();
        _difficultyModeText.SetText("LOADED");

        // Request the Difficulty mode from the GameManager
        StartCoroutine(Toggle());
    }




    /// <summary>
    /// What the user selects and presses from the canvas
    /// </summary>
    public void UIToggle()
    {
        // Play the "select" sound
        gameObject.GetComponent<AudioSource>().Play();

        // Change the settings in the background
        StartCoroutine(Toggle());
    }




    /// <summary>
    /// Messages the GameManager for a new difficulty and updates the displayed difficulty in the button
    /// </summary>
    IEnumerator Toggle()
    {
        yield return new WaitForSeconds(0.2f);
        try
        { 
            _difficultyModeText.text = _gameManager.ToggleDifficulty();
        }
        catch
        {
            Debug.Log("Cannot set difficulty");
            //_difficultyModeText.text = "???";
            _difficultyModeText.SetText("???");
        }
    }
}
