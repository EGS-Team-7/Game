using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControllerV2 : MonoBehaviour
{
    public float moveSpeed = 10f;
    public Vector2 moveDirection;
    public float moveAngle;
    float x, y;

    void Update()
    {
        Inputs();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Inputs()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

    }

    void Movement()
    {
        moveDirection = new Vector2(x, y);
        moveAngle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
        transform.Translate(transform.right * x * moveSpeed * Time.deltaTime);
        transform.Translate(transform.up * y * moveSpeed * Time.deltaTime);
    }
}
