using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    #region Variables


    public CharacterController controller; //Ref to characterControllerScript

    public Animator animator; //animator variable

    public static float moveSpeed = 40f;  //moveSpeed variable to determine the speed of movement
    float xMove = 0f;  //Horizontal movement (Not speed)
    public static float yMove = 0f;

    public static bool jump = false;  //True or false condition that determines if the player can jump
    bool crouch = false;  //True or false condition that determines if the player can crouch

    public static bool facingRight = true;  //true or false statement that checks which direction the player is facing

    public bool jumped; //Harriet, used to check if the player has jumped
    public float jumpdelay; //Harriet, used to give the jump delay a value


    #endregion


    // Start is called before the first frame update
    void Start()
    {
        //Harriet {
        jumped = false;
        //if the jumpdelay is equal too or less than zero then...
        if (jumpdelay <= 0)
        {
            //the jumpdelay will be equal to 0.25f
            jumpdelay = 0.25f; 
        }
        //Harriet }

    }

    // Update is called once per frame
    void Update()
    {




        // -1 * moveSpeed |or| +1 * moveSpeed

        //Erik {
        xMove = Input.GetAxisRaw("Horizontal") * moveSpeed; 



        // applies the animator parameter "Speed" to the ABSOLUTE value of the players horizontal speed
        animator.SetFloat("Speed", Mathf.Abs(xMove));

        //Erik }




        #region badCodeDidn'tWork
        /*
        if (facingRight == true)
        {
            animator.SetFloat("DirectionSpeed", xMove);
        }

        if(facingRight == false)
        {
            animator.SetFloat("DirectionSpeed", xMove);
        }

    */
        #endregion


        //Erik
        #region Pushing/pulling animation conditions

        if (xMove < 0 && facingRight && EquipItems.objectRight) // if the player is moving to the left and facing...
        {                                                      // ... to the right and has an equipped object...
            animator.SetBool("IsPushing", false);              // ... to its right then pushing = false.
        }



        if(xMove > 0 && facingRight && EquipItems.objectLeft)  // if the player is moving to the right and facing...
        {                                                      // ...to the right and has an equipped object to..
            animator.SetBool("IsPushing", false);              // its left then push = false
        }


        

        //If player is moving left and facing left and has an object to its left...
        if (xMove < 0 && facingRight && EquipItems.objectLeft)
        {
            // ... flip the player.
            animator.SetBool("IsPushing", true);
        }
        //if player is moving right and is facing right and has an object to its right...
        else if (xMove > 0 && facingRight && EquipItems.objectRight) 
        {
            // ... flip the player.
            animator.SetBool("IsPushing", true);

        }

        #endregion



        #region Jump/crouch + not holding objects
        //Jumpcode

        if (Input.GetButton("Jump") && !EquipItems.objectDraged && !CharacterController.roofAbove && !jumped) //Harriet added !jumped
        {
            
            
            jump = true;  //If player presses "space" or "up" or "W" then it sets "jump" = true
            jumped = true; //Harriet
            animator.SetBool("IsJumping", true);
            StartCoroutine(SpamBlockco());//Harriet, starts the delay co-routine

        }


        if (Input.GetButtonDown("Crouch") && !EquipItems.objectDraged)
        {
            crouch = true;  //If player presses "s" then it sets "crouch" = true
        }

        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;  //if player releases the Crouch button, then it sets "crouch" = false
        }



    }
    #endregion




    /* BadCodeDidn'tWork

    private void Flip()
    {
        facingRight = false;
    }

    */



    public void OnLanding () //Method for when player lands
    {
        animator.SetBool("IsJumping", false);

        //spela landing sound
    }



    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);  //Play the crouch animation when player is crouching
    }




    //Multiplyng by Time.fixedDeltaTime will make sure that we're moving the same amount,
    //regardless of how many times (per second) this function is called

    private void FixedUpdate()
    {
        if(MainMenu.playing)
        {
            controller.Move(xMove * Time.fixedDeltaTime, crouch, jump); //Arg 1 = horizontal movement || Arg 2 = Crouching? || Arg 3 = Jumping?
        }
        if(!MainMenu.playing)
        {
             ; //Arg 1 = horizontal movement || Arg 2 = Crouching? || Arg 3 = Jumping?
        }
        jump = false;  //sets jump to false when player has jumped



    }

    //Harriet, this co-routine adds a delay to jumps whenever the player has jumped (jumped = true) {
    public IEnumerator SpamBlockco()
    {
        //if the player has jumped
        if (jumped == true)
        {
            //then start waiting for the amount of time in jumpdelay
            yield return new WaitForSeconds(jumpdelay);
        }
        yield return null;
        jumped = false;
    }
    //Harriet }
}


