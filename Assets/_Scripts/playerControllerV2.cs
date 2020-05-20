using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControllerV2 : MonoBehaviour
{



    public float moveSpeed = 10f;       // How quickly we travel
    public Vector2 moveDirection;       // Which direction we are travelling
    public float moveAngle;             // ???
    float x, y;                         // Which direction we want to go




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
        // Reset the direction we ar facing
        moveDirection = new Vector2(x, y);

        // Set it to the direction we are travelling
        moveAngle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;

        // Move ourselves in the desired direction
        transform.Translate(transform.right * x * moveSpeed * Time.deltaTime);
        transform.Translate(transform.up * y * moveSpeed * Time.deltaTime);
    }
}
