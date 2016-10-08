using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float frictionCoeff;
    public float jumpSize;
    public float jumpSizeModifier;
    public float jumpTimer;
    public float gravity;
    public float wallSpeedVelocity;
    public float wallJumpVelocity;
    public bool releaseButtonToJump;
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
    bool facingRight = true;
    bool onWall = false;
    bool upsideTouch;
    bool downsideTouch;
    bool rightsideTouch;
    bool leftsideTouch;

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

            //Vertical movement
            if (!rig.IsTouchingLayers(groundLayer))
            {
                    acceleration.y = -gravity;
            }
            else
            {
                jumping = false;
                onGround = true;
                if (downsideTouch)
                {
                    velocity.y = -4.0f;
                    acceleration.y = -gravity;
                    onGround = false;
                }
                else if (leftsideTouch || rightsideTouch)
                {
                    velocity.y = -wallSpeedVelocity;
                    acceleration.y = 0.0f;
                    onGround = false;
                    onWall = true;
                }
                else
                {
                    acceleration.y = 0.0f;
                    velocity.y = 0.0f;
                    onWall = false;
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
            if (jumpInput && jumpTimerLive > 0.0f && jumping) //While on jump
            {
                acceleration.y = jumpSizeLive;
                jumpSizeLive = jumpSizeLive * jumpSizeModifier;
            }
            if (jumping) { //Decrement the timer
                jumpTimerLive -= Time.deltaTime;
            }

            if(jumpInput && onWall)
            {
                acceleration.y = jumpSize/2;
                if (leftsideTouch) {
                    acceleration.x = -wallJumpVelocity; 
                }
                if (rightsideTouch)
                {
                    acceleration.x = wallJumpVelocity;
                }
                onWall = false;
            }

            velocity.y += acceleration.y;

            //Add friction only when the player doesn't touch the joystick
            if (horMove == 0 && !onWall)
            {
                friction.x = -1 * velocity.normalized.x * frictionCoeff;
                acceleration.x += friction.x;
                if (velocity.x < 0.5f && facingRight)
                {
                    velocity.x = 0.0f;
                }
                if (velocity.x > 0.5f && !facingRight)
                {
                    velocity.x = 0.0f;
                }
            }

            velocity.x += acceleration.x;

            if (velocity.x < -10.0f)
                velocity.x = -10.0f;
            else if (velocity.x > 10.0f)
                velocity.x = 10.0f;

            rig.velocity = velocity;
        }

        if(velocity.x < 0.0f && facingRight)
        {
            flip();
        }
        else if (velocity.x > 0.0f && !facingRight)
        {
            flip();
        }
    }

    void OnTriggerStay2D(Collider2D coll)
    {
       switch(coll.gameObject.tag)
        {
            case "upside": upsideTouch = true;
                break;
            case "downside": downsideTouch = true;
                break;
            case "leftside": leftsideTouch = true;
                break;
            case "rightside": rightsideTouch = true;
                break;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        switch (coll.gameObject.tag)
        {
            case "upside": upsideTouch = false;
                break;
            case "downside": downsideTouch = false;
                break;
            case "leftside": leftsideTouch = false;
                            onWall = false;
                break;
            case "rightside": rightsideTouch = false;
                            onWall = false;
                break;
        }
    }

    //Flip the character's sprite
    void flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}