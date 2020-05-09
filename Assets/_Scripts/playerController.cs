using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    //movement
    public float currentSpeed;
    public float moveSpeed = 4500;
    public float maxSpeed = 20;
    public float gravity = 10;
    public bool grounded;
    public LayerMask Ground;

    public float counterMove = .175f;
    float threshold = .01f;

    Vector3 normalVector = Vector3.up;

    public float maxSlopeAngle = 90f;

    //input
    float x, y;
    bool jumping;

    //jump
    bool rdyToJump = true;
    float jumpCooldown = .25f;
    public float jumpForce = 550f;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        Inputs();
    }

    void FixedUpdate()
    {
        Movement();
    }

    void Inputs()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        jumping = Input.GetButton("Jump");
    }

    void Movement()
    {
        rb.AddForce(Vector3.down * Time.deltaTime * gravity);

        Vector2 mag = transform.right;
        float xMag = mag.x, yMag = mag.y;

        counterMovement(x, y, mag);

        if (rdyToJump && jumping) Jump();

        float maxSpeed = this.maxSpeed;

        if (x > 0 && xMag > maxSpeed) x = 0;
        if (x < 0 && xMag < -maxSpeed) x = 0;

        float multiplyer = 1f, multiplyerV = 1f;

        if (!grounded)
        {
            multiplyer = .5f;
            multiplyerV = .5f;
        }
        rb.AddForce(transform.right * x * moveSpeed * Time.deltaTime * multiplyer * multiplyerV);

    }

    private void Jump()
    {
        if (grounded && rdyToJump)
        {
            rdyToJump = false;

            rb.AddForce(Vector2.up * jumpForce * 1.5f);
            rb.AddForce(normalVector * jumpForce * .5f);

            Vector3 vel = rb.velocity;
            if (rb.velocity.y < .5f)
                rb.velocity = new Vector3(vel.x, 0, vel.z);
            else if (rb.velocity.y > 0)
                rb.velocity = new Vector3(vel.x, vel.y / 2, vel.z);

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void ResetJump()
    {
        rdyToJump = true;
    }

    private void counterMovement(float x, float y, Vector2 mag)
    {
        if (!grounded || jumping) return;

        if (Mathf.Abs(mag.x) > threshold && Mathf.Abs(x) < .05f || (mag.x < -threshold && x > 0) || (mag.x > threshold && x < 0))
        {
            rb.AddForce(moveSpeed * transform.right * Time.deltaTime * -mag.x * counterMove);
        }
        
        if (Mathf.Sqrt(Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.y, 2)) > maxSpeed)
        {
            float fallSpeed = rb.velocity.y;
            Vector3 n = rb.velocity.normalized * maxSpeed;
            rb.velocity = new Vector3(n.x, fallSpeed, 0);
        }
        
    }

    bool IsFloor(Vector3 v)
    {
        float angle = Vector3.Angle(Vector3.up, v);
        return angle < maxSlopeAngle;
    }

    bool cancellingGrounded;

    private void OnCollisionStay2D(Collision2D other)
    {
        int layer = other.gameObject.layer;

        if (Ground != (Ground | (1 << layer))) return;

        for (int i = 0; i < other.contactCount; i++)
        {
            Vector3 normal = other.contacts[i].normal;

            if (IsFloor(normal))
            {
                grounded = true;
                cancellingGrounded = false;
                normalVector = normal;
                CancelInvoke(nameof(StopGrounded));
            }
        }

        float delay = 3f;
        if (!cancellingGrounded)
        {
            cancellingGrounded = true;
            Invoke(nameof(StopGrounded), Time.deltaTime * delay);
        }
    }

    public void StopGrounded()
    {
        //grav = true;
        grounded = false;
    }
}
