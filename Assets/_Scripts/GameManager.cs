







using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;          // Allow detecting scene changes
using TMPro;                                // Allow updating score component








/// <summary>
/// Handles scoring, difficulty, and death
/// </summary>
public class GameManager : MonoBehaviour
{



    // SINGLETON
    private static GameManager _gameManager;    // How we track whether or not a game manager already exists
                                                // See the `Music.cs` class if you need help on this topic.

    // SCORING
    public TMP_Text scoreText;                  // How the player knows the score
    public int score = 0;                       // What the score is
    public ParticleSystem part;                 // When a player scores

    // DEATH
    public GameObject gameOver;                 // What opens when a player dies

    // DIFFICULTY
    private launcher _playerLauncher;           // Where the thresholds for weapon upgrades are (currently) held during play
    [Tooltip("Supported difficulty modes")]
    [SerializeField] private List<Difficulty> _difficulties;    // The available difficulty modes
    private Difficulty _currentDifficulty;                      // The selected difficulty mode
    private int _currentDifficultyIndex;                        // Where in the difficulty list we are




    public void Awake()
    {

        // Check for singleton instance
        if (_gameManager)
        {
            // If there is already a reference to a game manager,
            // Then we need to destroy ourselves to prevent errors

            Destroy(gameObject);
        }
        else
        {
            // There is no reference to a game manager, so we must be it
            // We set ourselves to be that reference, set ourselves to not be destroyed,

            _gameManager = this;
            DontDestroyOnLoad(gameObject);
        }


        // Assigns the current difficulty.
        // Creates one if none exist.
        try
        {
            _currentDifficulty = _difficulties[0];
        }
        catch
        {
            Debug.Log("No difficulty modes available");
            // This spawns, places, names, and sets a difficulty in the event none is locatable
            try
            {
                // Instantiate a new GameObject, Name it, Move it under the `GameManager` object (prevents cluttering up the hierarchy)
                GameObject newDifficultyObject = new GameObject();
                newDifficultyObject.name = "Error Difficulty";
                newDifficultyObject.transform.SetParent(GameObject.Find("GameManager").transform);

                // Adds a difficulty component
                newDifficultyObject.AddComponent<Difficulty>();
                Difficulty newDifficulty = newDifficultyObject.GetComponent<Difficulty>();

                // Assigns the new (default) difficulty
                _difficulties.Add(newDifficulty);
                _currentDifficulty = newDifficulty;
            }
            catch
            {
                Debug.Log("Could not find dynamic folder, or instantiation failed");
            }
        }


        // Register our `OnSceneChange` method with the `SceneManager` object
        SceneManager.sceneLoaded += OnSceneChange;


        // Check each time we load
        // OnSceneChange();
    }




    /// <summary>
    /// Trying to catch assignments on scene change
    /// 
    /// See also: https://www.reddit.com/r/Unity3D/comments/4yjcj5/onlevelwasloaded_replacement/
    /// See also: https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager-sceneLoaded.html
    /// 
    /// This is *NOT* an official method from Unity, this is CUSTOM, see the section above in `Awake`
    /// </summary>
    public void OnSceneChange(Scene scene, LoadSceneMode mode)
    {
        // Try this on activation, everytime
        GetPlayerLauncher();

        // Now contact the player launcher and update the values
        try
        {
            _playerLauncher.UpdateThresholds(_currentDifficulty.Thresholds());
        }
        catch
        {
            Debug.Log("Player launcher unreachable");
        }

        // Also load in any additional references we need, for in-game scenes
        LoadInGame();
    }


    /// <summary>
    /// Gathers references to InGame assets, such as score canvas, special effects, etc.
    /// </summary>
    private void LoadInGame()
    {
        // Score Text
        try
        {
            //scoreText = GameObject.Find("InGameUICanvas").transform.Find("ScoreText").GetComponent<TextMeshPro>();
            scoreText = GameObject.Find("InGameUICanvas").transform.Find("ScoreText").GetComponent<TMP_Text>();
        }
        catch
        {
            Debug.Log("could not find score text");
        }

        // Score particles
        try
        {
            part = GameObject.Find("InGameCamera").transform.Find("ScoreParticles").GetComponent<ParticleSystem>();
        }
        catch
        {
            Debug.Log("Could not find score particles");
        }

        // Death UI
        try
        {
            gameOver = GameObject.Find("InGameUICanvas").transform.Find("PlayerDeath").gameObject;
        }
        catch
        {
            Debug.Log("Could not find player death UI");
        }
    }



    /// <summary>
    /// Try finding the player launcher
    /// </summary>
    private void GetPlayerLauncher()
    {
        // Finds the Player, and then the playerLauncher
        try
        {
            _playerLauncher = GameObject.FindGameObjectWithTag("Player").transform.Find("Shooting").GetComponent<launcher>();
        }
        catch
        {
            Debug.Log("Could not find player launcher");
        }
    }




    /// <summary>
    /// When the player scores
    /// </summary>
    public void Score()
    {
        scoreText.text = "score: " + score;
        part.Play();
    }




    /// <summary>
    /// When the player dies
    /// </summary>
    public void PlayerDeath()
    {
        gameOver.SetActive(true);
    }




    /// <summary>
    /// Toggles the difficulty from the available list
    /// Each difficulty option is a:
    ///  - String name
    ///  - Int[] thresholds
    /// This then updates those thresholds in the player launcher
    /// </summary>
    /// <returns> the new difficulty mode </returns>
    public string ToggleDifficulty()
    {
        // Try and set the new difficulty to the next index
        // If it does not exist, reset the search
        try
        {
            _currentDifficultyIndex += 1;
            _currentDifficulty = _difficulties[_currentDifficultyIndex];
        }
        catch
        {
            _currentDifficultyIndex = 0;
            _currentDifficulty = _difficulties[_currentDifficultyIndex];
        }
        
        // Now contact the player launcher and update the values
        try
        {
            _playerLauncher.UpdateThresholds(_currentDifficulty.Thresholds());
        }
        catch
        {
            Debug.Log("Player launcher unreachable");
        }

        // Finally, send back the name of the difficulty
        return _currentDifficulty.Name();
    }
}
