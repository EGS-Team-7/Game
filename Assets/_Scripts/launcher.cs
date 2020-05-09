using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class launcher : MonoBehaviour
{
    public ParticleSystem[] shot;
    public int activeShot;
    public float speed = 10f;
    public float fireRate = 1;
    public float dif;
    public float angle;

    public GameObject[] ShotUI;

    public RectTransform Selector;

    float timer = .1f;
    playerControllerV2 player;

    float sound = 0f;

    GameManager gameManager;

    void Start()
    {
        player = GetComponentInParent<playerControllerV2>();
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }


    void Update()
    {
        Look();
        ShotChange();
        AngleAdjust();
        Shoot();
    }
    void Look()
    {
        Vector3 mouse = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        angle = Mathf.Atan2(mouse.y, mouse.x) * Mathf.Rad2Deg;
        gameObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void ShotChange()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            activeShot = 0;
            Selector.localPosition = new Vector3(-50, -204.5f, 0);
            Selector.gameObject.GetComponent<AudioSource>().Play();
            for (int i = 1; i < shot.Length; i++)
            {
                shot[i].gameObject.SetActive(false);
            }
        }

        if(gameManager.score >= 500)
        {
            ShotUI[1].SetActive(true);
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                activeShot = 1;
                Selector.localPosition = new Vector3(0, -204.5f, 0);
                Selector.gameObject.GetComponent<AudioSource>().Play();
                shot[0].gameObject.SetActive(false);
                shot[1].gameObject.SetActive(true);
                for (int i = 2; i < shot.Length; i++)
                {
                    shot[i].gameObject.SetActive(false);
                }
            }
        }

        if (gameManager.score >= 1000)
        {
            ShotUI[2].SetActive(true);
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                activeShot = 2;
                Selector.localPosition = new Vector3(50, -204.5f, 0);
                Selector.gameObject.GetComponent<AudioSource>().Play();
                shot[0].gameObject.SetActive(false);
                shot[1].gameObject.SetActive(false);
                shot[2].gameObject.SetActive(true);
                for (int i = 3; i < shot.Length; i++)
                {
                    shot[i].gameObject.SetActive(false);
                }
            }
        }
    }

    void AngleAdjust()
    {
        dif = Mathf.DeltaAngle(angle, player.moveAngle);

        if (dif >= -90 && dif <= 90 && player.moveDirection.magnitude > 0)
        {
            var main = shot[activeShot].main;
            float mult = 5 + (10 / (Mathf.Abs(dif) / 10));
            float mult2 = Mathf.Clamp(mult, 5, 10);
            main.startSpeedMultiplier = mult2;
        }

        if (dif < -90 || dif > 90 || player.moveDirection.magnitude == 0)
        {
            var main = shot[activeShot].main;
            main.startSpeedMultiplier = 5;
        }
    }

    void Shoot()
    {
        sound -= Time.deltaTime;

        timer -= Time.deltaTime;
        if (timer <= 0)
            if (Input.GetAxisRaw("Fire1") == 1)
            {
                var em = shot[activeShot].emission;
                em.enabled = true;


                float shoot;
                shoot = shot[activeShot].particleCount;

                

                if (sound < 0)
                {
                    gameObject.GetComponent<AudioSource>().Play();
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
        if (Input.GetAxisRaw("Fire1") != 1)
        {
            var em = shot[activeShot].emission;
            em.enabled = false;
        }
    }
}
