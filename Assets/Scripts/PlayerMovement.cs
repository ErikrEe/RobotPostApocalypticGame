using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller; //Ref to characterControllerScript
    public Animator animator;

    public float moveSpeed = 40f;

    float xMove = 0f;  //Horizontal movement (Not speed)

    public static bool jump = false;
    bool crouch = false;

    public static bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        xMove = Input.GetAxisRaw("Horizontal") * moveSpeed; // -1 * moveSpeed |or| +1 * moveSpeed


        animator.SetFloat("Speed", Mathf.Abs(xMove));


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



        if(xMove < 0 && facingRight && EquipItems.objectRight)
        {
            animator.SetBool("IsPushing", false);
        }

        if(xMove > 0 && facingRight && EquipItems.objectLeft)
        {
            animator.SetBool("IsPushing", false);
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

    

        if (Input.GetButton("Jump") && !EquipItems.objectDraged)
        {
            jump = true;  //If player presses "space" or "up" or "W" then it sets "jump" = true
            animator.SetBool("IsJumping", true);
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;  //If player presses "down" or "s" then it sets "crouch" = true
        }

        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;  //if player releases the Crouch button, then it sets "crouch" = false
        }


    }


    /*

    private void Flip()
    {
        facingRight = false;
    }

    */

   public void OnLanding ()
    {
        animator.SetBool("IsJumping", false);
    }

    //Multiplyng by Time.fixedDeltaTime will make sure that we're moving the same amount,
    //regardless of how many times (per second) this function is called

    private void FixedUpdate()
    {
        controller.Move(xMove * Time.fixedDeltaTime, crouch, jump); //Arg 1 = horizontal movement || Arg 2 = Crouching? || Arg 3 = Jumping?
        jump = false;  //sets jump to false when player has jumped



    }
}
