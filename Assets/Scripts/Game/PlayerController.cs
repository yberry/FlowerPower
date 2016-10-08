using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float frictionCoeff;
    public float jumpSize;
    public float jumpSizeModifier;
    public float jumpTimer;
    public float gravity;
    public LayerMask groundLayer;


    float jumpSizeLive;

    [System.NonSerialized]
    Vector2 velocity;
    Vector2 acceleration;
    Vector2 friction = Vector2.zero;

    float jumpTimerLive;

    bool canMove = true;
    bool onGround = false;
    bool jumping = false;

    bool floorTouch;
    bool rightSideTouch;
    bool leftSideTouch;

    Rigidbody2D rig;

    void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        float horMove = Input.GetAxis("hor_move");
        bool jumpInput = Input.GetButton("Jump");

        acceleration.x = horMove;

        if (canMove)
        {
            //Horizontal movement
            velocity.x += acceleration.x;
            if (velocity.x < -10.0f)
                velocity.x = -10.0f;
            else if (velocity.x > 10.0f)
                velocity.x = 10.0f;

            //Vertical movement
            if (!rig.IsTouchingLayers(groundLayer))
            {
                acceleration.y = -gravity;
            }
            else
            {
                jumping = false;
                onGround = true;
                acceleration.y = 0.0f;
                velocity.y = 0.0f;
            }


            //Add friction only when the player doesn't touch the joystick
            if (horMove == 0)
            {
                friction.x = -1 * velocity.normalized.x * frictionCoeff;
                velocity.x += friction.x;
                if(velocity.x < 0.5f)
                {
                    velocity.x = 0.0f;
                }
            }

            //Jump behaviour
            if (jumpInput && onGround) //When the button is pressed
            {
                acceleration.y = jumpSize;
                jumpSizeLive = jumpSize;
                jumping = true;
                onGround = false;
                jumpTimerLive = jumpTimer;
            }
            if (jumpInput && jumpTimerLive > 0.0f) //While on jump
            {
                acceleration.y = jumpSizeLive;
                jumpSizeLive = jumpSizeLive * jumpSizeModifier;
            }
            if (jumping) { //Decrement the timer
                jumpTimerLive -= Time.deltaTime;
            }

            velocity.y += acceleration.y;

            rig.velocity = velocity;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "platform")
        {
            foreach(ContactPoint2D touch in coll.contacts)
            {
                //Debug.Log(touch.otherCollider.collider2D.);
            }
        }
    }
}