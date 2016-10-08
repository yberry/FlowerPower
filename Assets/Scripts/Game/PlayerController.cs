using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float frictionCoeff;
    public float jumpSize;
    public float gravity;

    [System.NonSerialized]
    Vector2 velocity;
    Vector2 acceleration;
    Vector2 friction = Vector2.zero;

    float jumpTimer = 0.0f;


    bool canMove = true;
    bool onGround = false;
    bool canJump = false;

    Rigidbody2D rig;
    BoxCollider2D box;

    void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
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
        bool jumpInputDown = Input.GetButtonDown("Jump");
        bool jumpInput = Input.GetButton("Jump");

        if (jumpInputDown){
            jumpTimer = 10.0f;
            Debug.Log(jumpTimer);

        }


        acceleration.x = horMove;
        //float verMove = Input.GetAxis("ver_move");
        if (canMove)
        {
            //Horizontal movement
            velocity.x += acceleration.x;
            if (velocity.x < -10.0f)
                velocity.x = -10.0f;
            else if (velocity.x > 10.0f)
                velocity.x = 10.0f;

            //Vertical movement
            if (!onGround){
                acceleration.y = -gravity;
            }
            else{
                acceleration.y = 0.0f;
                velocity.y = 0.0f;
            }


            //Add friction only when the player doesn't touch the joystick
            if (horMove == 0){
                friction.x = -1 * velocity.normalized.x * frictionCoeff;
                velocity.x += friction.x;
            }

            //Jump behaviour
            if (canJump){
                if (jumpInput && jumpTimer > 0.0f){
                    jumpTimer -= Time.deltaTime;
                    acceleration.y += jumpSize;
                } else {
                    canJump = false;
                    onGround = false;

                }
            }
            velocity.y += acceleration.y;

            rig.velocity = velocity;
        }
    }

    void OnCollisionEnter2D(Collision2D coll){
        float collBoxYSize = coll.gameObject.GetComponent<BoxCollider2D>().size.y;
        if (coll.gameObject.tag == "platform" && !onGround){
            transform.position = new Vector2(transform.position.x, coll.gameObject.transform.position.y + collBoxYSize + 0.1f);
            onGround = true;
            canJump = true;
            Debug.Log("uech copain");
        }
    }
}