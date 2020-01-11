using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EquipItems : MonoBehaviour
{
    [SerializeField]
    public Rigidbody2D equObject;
    public static bool pickedUp = false;
    private bool left = false;
    private bool right = false;
    Vector2 vector;
    Vector2 vectorTwo;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        vector.x = gameObject.transform.position.x;
        vectorTwo.y = gameObject.transform.position.y;
        if (Input.GetKeyDown(KeyCode.E) && (gameObject.transform.position - this.transform.position).sqrMagnitude < 1.5f * 1.5f)
        {
            pickedUp = true;
            vector.x = vector.x + 0.7f;                                                 //makes the vector.x equal to itself plus 0.7f
            //vectorTwo.y = vectorTwo.y + 1f;                                             //makes the vector.x equal to itself plus 0.7f
           // equObject.transform.position = new Vector2(vector.x, vectorTwo.y);          //Makes the position of the game object equal to the players position plus 0.7f on the x axis
            equObject.transform.parent = gameObject.transform; 
            equObject.transform.rotation = Quaternion.Euler(0, 0, -90);
            equObject.constraints = RigidbodyConstraints2D.FreezeRotation;
            //equObject.GetComponent<Rigidbody2D>().gravityScale = 0;

            //GameObject.FindGameObjectsWithTag("equObject")
        }
        if(pickedUp)
        {
            vectorTwo.y = vectorTwo.y + 0.2f; //makes the vector.y equal to itself plus 0.2f
            vector.x = vector.x + 0.7f;                                                 //makes the vector.x equal to itself plus 0.7f
                                                                                        //equObject.transform.position = new Vector2(vector.x, vectorTwo.y);          //Makes the position of the game object equal to the players position plus 0.7f on the x axis and 0.2 on the y axis

            // If the input is moving the player right and the player is facing left...
            if (CharacterController.move < 0 && !CharacterController.facingRight)
            {
                
                // ... flip the player.
                Flip();
                //equObject.transform.position = new Vector2(vector.x, vectorTwo.y);   
                left = true;
                right = false;

            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (CharacterController.move > 0 && CharacterController.facingRight)
            {
                // ... flip the player.
                Flip();
                left = false;
                right = true;
                //equObject.transform.position = new Vector2(-vector.x, vectorTwo.y);
            }
        }
        if(pickedUp && left)
        {
            vectorTwo.y = vectorTwo.y + 0.2f; //makes the vector.y equal to itself plus 0.2f
            vector.x = vector.x - 1.5f;                                                 //makes the vector.x equal to itself plus 0.7f
            equObject.transform.position = new Vector2(vector.x, vectorTwo.y);

        }
        if(pickedUp && right)
        {
            vectorTwo.y = vectorTwo.y + 0.2f; //makes the vector.y equal to itself plus 0.2f
            vector.x = vector.x + 0.7f;                                                 //makes the vector.x equal to itself plus 0.7f
            equObject.transform.position = new Vector2(vector.x, vectorTwo.y);
        }

        if (Input.GetKey(KeyCode.Q) && pickedUp)
        {
            equObject.transform.parent = null;                                          //this removes the object from being a child to the player 
            pickedUp = false;
            equObject.constraints = RigidbodyConstraints2D.None;                        // Un freezes the object so it can tilt over again.
            //equObject.GetComponent<Rigidbody2D>().gravityScale = 1; //Sets the gravity for that object to 0
        }


        void Flip()
        {
            // Switch the way the player is labelled as facing.
            CharacterController.facingRight = !CharacterController.facingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}
