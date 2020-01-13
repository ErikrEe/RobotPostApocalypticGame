using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EquipItems : MonoBehaviour
{
    [SerializeField]
    public Rigidbody2D equObject;
    public static bool pickedUp = false;
    private bool left = false, right = false;
    Vector2 vector, vectorTwo;
    Rigidbody2D playerVelocity;

    void Start()                                                                             //Start is called before the first frame update
    {
        playerVelocity = GetComponent<Rigidbody2D>();
    }

    void Update()                                                                            //Update is called once per frame
    {
        float verticalVelocity = playerVelocity.velocity.y;


        vector.x = gameObject.transform.position.x;
        vectorTwo.y = gameObject.transform.position.y;
        if (Input.GetKeyDown(KeyCode.E) && (equObject.transform.position - this.transform.position).sqrMagnitude < 2f * 2f)
        {
            pickedUp = true;
            vector.x = vector.x + 0.7f;                                                      //makes the vector.x equal to itself plus 0.7f

            
            
            
            //Debug.Log("Bruh funka bror " + equObject.transform.rotation.eulerAngles);
            equObject.constraints = RigidbodyConstraints2D.FreezeAll;
            equObject.transform.parent = gameObject.transform;
            equObject.transform.position = new Vector2(vector.x, vectorTwo.y);              //Makes the position of the object equal to the players position plus some modifications so that the object doesnt teleport into the player
            equObject.transform.localRotation = Quaternion.Euler(0, 0, -90);
            if (equObject.transform.localScale.x < 0) //Tobbes kod
            {
                Vector3 theScale = equObject.transform.localScale;                                        //Multiply the player's x local scale by -1.
                theScale.x *= -1;
                equObject.transform.localScale = theScale;
            }
        }

        if(pickedUp)
        {
            vectorTwo.y = vectorTwo.y + 0.2f;                                               //makes the vector.y equal to itself plus 0.2f
            vector.x = vector.x + 0.7f;                                                     //makes the vector.x equal to itself plus 0.7f
                                                                                            //equObject.transform.position = new Vector2(vector.x, vectorTwo.y); 
                                                                                            //Makes the position of the game object equal to the players position plus 0.7f on the x axis and 0.2 on the y axis


            if (CharacterController.move < 0 && !CharacterController.facingRight)           // If the input is moving the player right and the player is facing left...
            {
                

                Flip();                                                                     // ... flip the flower.
                left = true;
                right = false;

            }

            else if (CharacterController.move > 0 && CharacterController.facingRight)       // Otherwise if the input is moving the player left and the player is facing right...
            {
                Flip();                                                                     // ... flip the flower.
                left = false;
                right = true;

            }
            if (left)
            {
                vectorTwo.y = vectorTwo.y + 0.2f;                                           //makes the vector.y equal to itself plus 0.2f
                vector.x = vector.x - 1.5f;                                                 //makes the vector.x equal to itself plus 0.7f
                equObject.transform.position = new Vector2(vector.x, vectorTwo.y);          //Makes the position of the object equal to the players position plus some modifications so that the object doesnt teleport into the player
            }
            else if (right)
            {
                vectorTwo.y = vectorTwo.y + 0.2f;                                           //makes the vector.y equal to itself plus 0.2f
                vector.x = vector.x + 0.1f;                                                 //makes the vector.x equal to itself plus 0.7f
                equObject.transform.position = new Vector2(vector.x, vectorTwo.y);          //Makes the position of the object equal to the players position plus some modifications so that the object doesnt teleport into the player
            }


             if(pickedUp && PlayerMovement.jump)
             {
                 if(playerVelocity.position.y < 0)
                 {
                     gameObject.GetComponent<Rigidbody2D>().gravityScale = 0f;              //Sets the gravity for that object to 0
                 }
                 gameObject.GetComponent<Rigidbody2D>().gravityScale = 1f;                  //Sets the gravity for that object to 1

             } 
        }




        if (Input.GetKey(KeyCode.Q) && pickedUp)
        {
            equObject.transform.parent = null;                                              //this removes the object from being a child to the player 
            pickedUp = false;
            equObject.constraints = RigidbodyConstraints2D.None;                            // Un freezes the object so it can tilt over again
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;                        //Sets the gravity for that object to 1
        }


        void Flip()
        {
            
            CharacterController.facingRight = !CharacterController.facingRight;             //Switch the way the player is labelled as facing.
            Vector3 theScale = transform.localScale;                                        //Multiply the player's x local scale by -1.
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}
