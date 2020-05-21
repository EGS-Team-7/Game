







using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;






/// <summary>
/// Currently, a combination of UI, Rewards, Input, and other Systems
/// Needs to be broken out into proper components.
/// </summary>
public class launcher : MonoBehaviour
{



    // SHOT MOVEMENT
    public ParticleSystem[] shot;       // What we emit, stored in an array
    public int activeShot;              // The current shot-type (0=default, 1=spread, 2=guided)
    public float speed = 10f;           // How quickly the shot travels
    
    // AIMING
    public float dif;                   // ???
    public float angle;                 // The direction the player will shoot in

    // UI
    public GameObject[] ShotUI;         // How the player selects which shot-type to use
    public RectTransform Selector;      // How the player knows which shot they have selected

    // COOLDOWNS
    float timer = .1f;                  // How long the player must wait between shots
    float sound = 0f;                   // How long until the player can make shot-sounds

    // EXTERNAL
    playerControllerV2 player;          // How the player communicates input
    GameManager gameManager;            // Where the score is stored and checked

    // THRESHOLDS
    [SerializeField] private int _defaultShotThreshold = 0;     // How long until the player unlocks the default shot
    [SerializeField] private int _spreadShotThreshold = 500;    // How long until the player unlocks the spread shot
    [SerializeField] private int _guidedShotThreshold = 1000;   // How long until the player unlocks the guided shot




    void Start()
    {
        player = GetComponentInParent<playerControllerV2>();
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }




    void Update()
    {
        Look();         // Find where the player is looking
        ShotChange();   // Identify if the player wants to change shot-types or fire
        AngleAdjust();
        Shoot();
    }




    /// <summary>
    /// Adjusts angle of reticle (the arrow) based on the mouse position relative to the camera position
    /// </summary>
    void Look()
    {
        // Find the mouse position relative to the camera frame
        Vector3 mouse = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        
        // Find the angle from the player's posiiton and the mouse
        angle = Mathf.Atan2(mouse.y, mouse.x) * Mathf.Rad2Deg;

        // Rotate the cursor in the direction of the new angle
        gameObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }




    /// <summary>
    /// Allows player to change their shot-type, controls firing effect, and disables projectiles (WHY?).
    /// 1 (Default) single particle, static
    /// 2 (Spread) unlocked at 501 points
    /// 3 (Guided) unlocked at 1000 points
    /// </summary>
    void ShotChange()
    {
        CheckDefault();
        CheckSpread();
        CheckGuided();
    }




    private void CheckDefault()
    {
        // After the player has earned _default_ points, they may select the default shot-type
        if (/* gameManager.score */ FindObjectOfType<GameManager>().GetComponent<GameManager>().score >= _defaultShotThreshold)
        {
            Debug.Log("UNLOCKED: Defaultshot");

            // Always allow checking if the player selects the default shot-type
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                // Set the shot-type to default
                activeShot = 0;

                // Move the selector (a UI rectangle) over to the default shots's position
                Selector.localPosition = new Vector3(-50, -204.5f, 0);

                // Activate the shot sound
                Selector.gameObject.GetComponent<AudioSource>().Play();

                // Deactivate all shots
                // (WHY?)
                for (int i = 1; i < shot.Length; i++)
                {
                    shot[i].gameObject.SetActive(false);
                }
            }
        }
    }





    private void CheckSpread()
    {
        // After the player has earned _spread_ points, they may select the spread shot-type
        if (/* gameManager.score */ FindObjectOfType<GameManager>().GetComponent<GameManager>().score >= _spreadShotThreshold)
        {
            Debug.Log("UNLOCKED: Spreadshot");

            // Show the spread shot-type in the UI
            ShotUI[1].SetActive(true);

            // Allow checking if the player selects the spread shot-type
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                // Set the shot-type to spread
                activeShot = 1;

                // Move the selector (a UI rectangle) over to the spread shots's position
                Selector.localPosition = new Vector3(0, -204.5f, 0);

                // Activate the shot sound
                Selector.gameObject.GetComponent<AudioSource>().Play();

                // Specifically deactivate the first two shots
                // (WHY?)
                shot[0].gameObject.SetActive(false);
                shot[1].gameObject.SetActive(true);

                // Deactivate all shots
                // (WHY?)
                for (int i = 2; i < shot.Length; i++)
                {
                    shot[i].gameObject.SetActive(false);
                }
            }
        }
        else
        {
            Debug.Log("FFFFFF");
        }
    }



    private void CheckGuided()
    {
        Debug.Log("SCORE: " + gameManager.score + " THRESH: " + _guidedShotThreshold);

        // After the player has earned _guided_ points, they may select the guided shot-type
        if (/* gameManager.score */ FindObjectOfType<GameManager>().GetComponent<GameManager>().score >= _guidedShotThreshold)
        {
            Debug.Log("UNLOCKED: Guidedshot");

            // Show the guided shot-type in the UI
            ShotUI[2].SetActive(true);

            // Allow checking if the player selects the guided shot-type
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                // Set the shot-type to guided
                activeShot = 2;

                // Move the selector (a UI rectangle) over to the guided shots's position
                Selector.localPosition = new Vector3(50, -204.5f, 0);

                // Activate the shot sound
                Selector.gameObject.GetComponent<AudioSource>().Play();

                // Specifically deactivate the first three shots
                // (WHY?)
                shot[0].gameObject.SetActive(false);
                shot[1].gameObject.SetActive(false);
                shot[2].gameObject.SetActive(true);

                // Deactivate all shots
                // (WHY?)
                for (int i = 3; i < shot.Length; i++)
                {
                    shot[i].gameObject.SetActive(false);
                }
            }
        }
    }





    /// <summary>
    /// Controls shot speed through air, based on look and move direction
    /// </summary>
    void AngleAdjust()
    {
        // Find the difference between where we are looking and where we are going
        dif = Mathf.DeltaAngle(angle, player.moveAngle);

        // If we are aiming in the same direction we are moving, and we are moving
        if (dif >= -90 && dif <= 90 && player.moveDirection.magnitude > 0)
        {
            // Launch the shot at an adjusted speed based on how extreme the angle is
            var main = shot[activeShot].main;
            float mult = 5 + (10 / (Mathf.Abs(dif) / 10));
            float mult2 = Mathf.Clamp(mult, 5, 10);
            main.startSpeedMultiplier = mult2;
        }

        // If we are looking too far away from where we are moving, or are not moving at all
        if (dif < -90 || dif > 90 || player.moveDirection.magnitude == 0)
        {
            // Launch the shot with regular speed
            var main = shot[activeShot].main;
            main.startSpeedMultiplier = 5;
        }
    }




    /// <summary>
    /// Controls the instantiation, input, and cooldown of shooting
    /// </summary>
    void Shoot()
    {
        // Decrement the time to fire and time to fire-sound
        sound -= Time.deltaTime;
        timer -= Time.deltaTime;
        
        // When the player is allowed to fire again
        if (timer <= 0)
        {
            // If they choose to fire
            if (Input.GetAxisRaw("Fire1") == 1)
            {
                // instantiate a new projectile and its effects
                var em = shot[activeShot].emission;
                em.enabled = true;

                // Keep track of how many particles there are
                // (Why? There are zero references to this variable)
                float shoot;
                shoot = shot[activeShot].particleCount;

                // When the player is allowed to make sound again
                if (sound < 0)
                {
                    // Make some more sound
                    gameObject.GetComponent<AudioSource>().Play();

                    // Set the cooldown for each type of shot
                    if(activeShot == 0)
                    {
                        sound = 1f;
                    }
                    if (activeShot == 1)
                    {
                        sound = .5f;
                    }
                    if (activeShot == 2)
                    {
                        sound = .5f;
                    }
                }
            }
        }

        // What does this do?
        // Why is this here?
        if (Input.GetAxisRaw("Fire1") != 1)
        {
            var em = shot[activeShot].emission;
            em.enabled = false;
        }
    }




    /// <summary>
    /// Allows other classes to change when the player can unlock new shot-types
    /// </summary>
    public void UpdateThresholds(List<int> newThresholds)
    {
        _defaultShotThreshold = newThresholds[0];
        _spreadShotThreshold = newThresholds[1];
        _guidedShotThreshold = newThresholds[2];
    }




}
