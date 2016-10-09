using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float movementCoeff;
    public float frictionCoeff;
    public float jumpSize;
    public float jumpSizeModifier;
    public float jumpTimer;
    public float gravity;
    public float wallJumpSize;
    public float wallSpeedVelocity;
    public float wallJumpVelocity;
    public bool releaseButtonToJump;

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

    //Collision states
    bool upsideTouch;
    bool downsideTouch;
    bool rightsideTouch;
    bool leftsideTouch;

    float stepTime = 0.15f;
    float stepTimeLive;


    Rigidbody2D rig;
    SoundEffectController sounds;

    //Flip the character's sprite
    private void flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        sounds = SoundEffectController.Instance;
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
        bool jumpInputDown = Input.GetButtonDown("Jump");

        if (canMove)
        {
            acceleration.x = horMove;

            //Vertical movement
            if (upsideTouch)
            {
                acceleration.y = 0.0f;
                velocity.y = 0.0f;
                onWall = false;
                onGround = true;
                jumping = false;
                if(leftsideTouch || rightsideTouch) //Down corner case
                {
                    velocity.x = 0.0f;
                }
            }
            else if (downsideTouch)
            {
                velocity.y = -2.0f;
                acceleration.y = -gravity;
                onGround = false;
                jumping = false;
                onWall = false;
                if(leftsideTouch || rightsideTouch) //Upper Corner case
                {
                    velocity.x = 0.0f;
                }
            }
            else if(leftsideTouch || rightsideTouch)
            {
                velocity.y = -wallSpeedVelocity;
                acceleration.y = -gravity;
                onGround = false;
                onWall = true;
                jumping = false;
            }
            else
            {
                acceleration.y = -gravity;
                onGround = false;
                onWall = false;
            }

            if (onGround)
            {
                if (jumpInput) //When the button is pressed
                {
                    acceleration.y = jumpSize;
                    jumpSizeLive = jumpSize;
                    jumping = true;
                    onGround = false;
                    jumpTimerLive = jumpTimer;
                    Debug.Log("JUMP");
                }
            }

            //Jump behaviour
            if (jumping)
            {
                if (jumpInput && jumpTimerLive > 0.0f)
                {
                    acceleration.y = jumpSizeLive;
                    jumpSizeLive = jumpSizeLive * jumpSizeModifier;
                }
                jumpTimerLive -= Time.deltaTime;
            }

            if (onWall)
            {
                if (jumpInput)
                {
                    acceleration.y = wallJumpSize;
                    if (leftsideTouch)
                    {
                        acceleration.x = -wallJumpVelocity;
                    }
                    if (rightsideTouch)
                    {
                        acceleration.x = wallJumpVelocity;
                    }
                }
            }

            //Add friction only when the player doesn't touch the joystick
            if (horMove == 0)
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

            if(velocity.magnitude > 1.0f && onGround && stepTimeLive <= 0.0f)
            {
                stepTimeLive = stepTime;
                sounds.MakeStepSound();
            }
            if(stepTimeLive > 0.0f)
            {
                stepTimeLive -= Time.deltaTime;
            }


            velocity.y += acceleration.y;
            velocity.x += acceleration.x;

            //Velocity cap
            if (velocity.x < -10.0f)
                velocity.x = -10.0f;
            else if (velocity.x > 10.0f)
                velocity.x = 10.0f;

            rig.velocity = velocity * movementCoeff;
        }
        
        //Sprite facing on the good direction
        if(horMove < 0.0f && facingRight)
        {
            flip();
        }
        else if (horMove > 0.0f && !facingRight)
        {
            flip();
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        sounds.MakeLandingSound();
        if (coll.gameObject.tag == "upside") upsideTouch = true;
        if(coll.gameObject.tag == "downside") downsideTouch = true;
        if(coll.gameObject.tag == "leftside") leftsideTouch = true;
        if(coll.gameObject.tag == "rightside") rightsideTouch = true;
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "upside")
        {
            upsideTouch = false;
            sounds.MakeJumpSound();
        }
        if (coll.gameObject.tag == "downside") downsideTouch = false;
        if (coll.gameObject.tag == "leftside")
        {
            leftsideTouch = false;
            sounds.MakeWallJumpSound();
        }
        if (coll.gameObject.tag == "rightside")
        {
            rightsideTouch = false;
            sounds.MakeWallJumpSound();
        }
    }
}