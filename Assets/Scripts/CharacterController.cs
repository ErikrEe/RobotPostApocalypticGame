﻿using UnityEngine;
using UnityEngine.Events;

public class CharacterController : MonoBehaviour
{

    //Erik {

    [SerializeField] private float jumpForce = 400f;                          // Amount of force added when the player jumps
    [Range(0, 1)] [SerializeField] private float crouchSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private bool airControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask whatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform groundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform ceilingCheck;                          // A position marking where to check for ceilings
    [SerializeField] private Collider2D crouchDisableCollider;                // A collider that will be disabled when crouching

    //Erik }


    public static bool hasLanded = false; //Harriet's code, used in PLayerMovement to determine when the landing sound should be played
    public static bool roofAbove = false; //Harriet's code, used in PlayerMovement to determine if there is roof above the player so that it can not jump
    const float groundedRadius = .1f; // Radius of the overlap circle to determine if grounded
    public static bool grounded;            // Whether or not the player is grounded.
    const float ceilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody2D rb;
    public static bool facingRight = true;  // For determining which way the player is currently facing.
    private Vector3 velocity = Vector3.zero;  //The "Target" velocity for the player

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;  //An event which controls what happens when the player is landing / has landed

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnCrouchEvent;
    private bool wasCrouching = false;  //Variable which determines if the player was crouching

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();  //get the Rigidbody2D Component and stores it in the variable "rb"

        if (OnLandEvent == null)  //Creates a new Event if it doesn't exist
            OnLandEvent = new UnityEvent();

        if (OnCrouchEvent == null) //Creates a new Event if it doesn't exist
            OnCrouchEvent = new BoolEvent();
    }


    private void OnDrawGizmos()
    {

        Gizmos.DrawWireSphere(groundCheck.position, groundedRadius);  //"Draws" / creates an invisible sphere... 
    }                                                                 //..that visually shows what is being checked and if the player is assigned as.. 
                                                                      //ON the ground or NOT on the ground
    private void FixedUpdate()
    {
        bool wasGrounded = grounded;  //variable for if the player is on the ground is now re-assigned to "grounded"
        grounded = false;
        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {



                grounded = true;

                if (!wasGrounded)
                {
                    OnLandEvent.Invoke();
                    hasLanded = true;
                }

                else
                {
                    hasLanded = false;
                }
            }
        }
    }




    public static float move; //Harriet, to be able to use move in both scripts while the variable still being inside of the Function

    public void Move(float moveTwo, bool crouch, bool jump)
    {
        move = moveTwo; //Harriet, to be able to use move in both scripts while the variable still being inside of the Function
        // If crouching, check to see if the character can stand up
        if (!crouch)
        {
            // If the character has a ceiling preventing them from standing up, keep them crouching
            if (Physics2D.OverlapCircle(ceilingCheck.position, ceilingRadius, whatIsGround))
            {
                crouch = true;
                roofAbove = true;
            }
            else
            {
                roofAbove = false;
            }
        }






        //only control the player if grounded or airControl is turned on
        if (grounded || airControl)
        {

            // If crouching and not holding the flower (!pickedUp)
            if (crouch && !EquipItems.pickedUp) //Harriet, added !EquipItems.PickedUp
            {
                if (!wasCrouching)
                {
                    wasCrouching = true;
                    OnCrouchEvent.Invoke(true);
                }

                // Reduce the speed by the crouchSpeed multiplier
                move *= crouchSpeed;

                // Disable one of the colliders when crouching
                if (crouchDisableCollider != null)
                    crouchDisableCollider.enabled = false;

            }
            else
            {
                // Enable the collider when not crouching
                if (crouchDisableCollider != null)
                    crouchDisableCollider.enabled = true;

                if (wasCrouching)
                {
                    wasCrouching = false;
                    OnCrouchEvent.Invoke(false);
                }
            }

            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, rb.velocity.y);
            // And then smoothing it out and applying it to the character
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmoothing);

            // If the input is moving the player right and the player is facing left and the player is not draging a box (!objectDraged)...
            if (move > 0 && !facingRight && !EquipItems.objectDraged) //Harriet, added !EquipItems.objectDraged
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right and the player is not draging a box (!objectDraged)...
            else if (move < 0 && facingRight && !EquipItems.objectDraged)//Harriet, added !EquipItems.objectDraged
            {
                // ... flip the player.
                Flip();
            }
        }
        // If the player is on the ground, AND presses the "Jump" button - Erik
        if (grounded && jump)
        {
            // Add a vertical force to the player, and tell the program that the player is no longer on the ground - Erik
            grounded = false;
            rb.AddForce(new Vector2(0f, jumpForce));
        }



    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing - Erik
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1 - Erik
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
