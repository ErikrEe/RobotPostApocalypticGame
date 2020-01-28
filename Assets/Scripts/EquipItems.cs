using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EquipItems : MonoBehaviour
{
    [SerializeField]
    public Rigidbody2D equObject;
    public static bool pickedUp = false;
    public static bool objectDraged = false;
    public static bool objectLeft = false;
    public static bool objectRight = false;
    private bool left = false, right = false;
    Vector2 vector, vectorTwo;
    Rigidbody2D playerVelocity;
    private float currentheight;
    private float previousheight;
    private float travel;
    GameObject dragObject;

    public Animator animator;

    //dessa avänds till att kalkylera spelarens storlek på X axeln
    Collider2D playerCollider, objectCollider;

    float offset;



    void Start()                                                                             //Start is called before the first frame update
    {
        playerVelocity = GetComponent<Rigidbody2D>();

        //Fetch the Collider from the GameObject
        playerCollider = GetComponent<Collider2D>();

    }


    void Update()                                                                            //Update is called once per frame
    {

        if (!objectDraged)
        {
            FindClosestEnemy(); //Går igenom FindClosestEnemy funktionen.
        }

         float verticalVelocity = playerVelocity.velocity.y;

        vector.x = gameObject.transform.position.x;
        vectorTwo.y = gameObject.transform.position.y;
//MODIFIERA KODEN UNDER#################################################################################
            if (Input.GetKeyDown(KeyCode.E) && (dragObject.transform.position - this.transform.position).sqrMagnitude < (objectCollider.bounds.extents.x * 2) * (objectCollider.bounds.extents.y * 2) +1 && pickedUp == false && dragObject.transform.position.y -1 <= gameObject.transform.position.y + objectCollider.bounds.extents.y && gameObject.transform.position.y + objectCollider.bounds.extents.y <= dragObject.transform.position.y +1)  
        {


            objectDraged = true;


            animator.SetBool("IsPulling", true);  //Sets the animation bool to true, which triggers the pulling animation


        }


        if (objectDraged) //If an object is being dragged..
        {
            if(gameObject.transform.position.x > dragObject.transform.position.x) //and the player is on the right side of the object (OBJECT SHOULD BE LEFT SIDE)
            {          
                offset = playerCollider.bounds.extents.x + objectCollider.bounds.extents.x + 0.3f; //det som ska läggas till , till drag objects +  - objectSizeX.x/5f
                vector.x = vector.x - offset;                                                     //makes the vector.x equal to itself plus 0.7f
                objectLeft = true;
                objectRight = false;
            }
            if (gameObject.transform.position.x < dragObject.transform.position.x) //and the player is on the left side of the object
            {
                offset = playerCollider.bounds.extents.x + objectCollider.bounds.extents.x + 0.3f; //yay
                vector.x = vector.x + offset;                                                     //makes the vector.x equal to itself plus 0.7f
                objectRight = true;
                objectLeft = false;

            }

            if(objectLeft)
            {

                dragObject.transform.position = new Vector2(vector.x, dragObject.transform.position.y);

            }
            if(objectRight)
            {

                dragObject.transform.position = new Vector2(vector.x, dragObject.transform.position.y);

            }
        }


        if (Input.GetKey(KeyCode.Q) && dragObject || gameObject.transform.position.y + objectCollider.bounds.extents.y < dragObject.transform.position.y)  //if Q is pressed or the "dragObject" is higher up than the player...
        {
            objectDraged = false;

            animator.SetBool("IsPulling", false); //Stops playing the "Pulling" animation

        }





            if (Input.GetKeyDown(KeyCode.E) && (equObject.transform.position - this.transform.position).sqrMagnitude < 2f * 2f  && objectDraged == false) 
            {

            pickedUp = true;
            vector.x = vector.x + 0.7f;                                                      //makes the vector.x equal to itself plus 0.7f



            equObject.constraints = RigidbodyConstraints2D.FreezeAll;
            equObject.transform.parent = gameObject.transform;
            equObject.transform.position = new Vector2(vector.x, vectorTwo.y);              //Makes the position of the object equal to the players position plus some modifications so that the object doesnt teleport into the player
            equObject.transform.localRotation = Quaternion.Euler(0, 0, 0);


            if (equObject.transform.localScale.x < 0) //Tobbes kod
                {
                Vector3 theScale = equObject.transform.localScale;                          //Multiply the player's x local scale by -1.
                theScale.x *= -1;
                equObject.transform.localScale = theScale;
                }
            }

        if(pickedUp)
        {
            vectorTwo.y = vectorTwo.y + 1.55f;                                               //makes the vector.y equal to itself plus 0.2f
            vector.x = vector.x + 0.65f;                                                     //makes the vector.x equal to itself plus 0.7f
                                                                                            //equObject.transform.position = new Vector2(vector.x, vectorTwo.y); 
                                                                                            //Makes the position of the game object equal to the players position plus 0.7f on the x axis and 0.2 on the y axis


            if (CharacterController.move < 0 && !CharacterController.facingRight)           // If the input is moving the player right and the player is facing left...
            {
                

               // Flip();                                                                     // ... flip the flower.
                left = true;
                right = false;

            }

            else if (CharacterController.move > 0 && CharacterController.facingRight)       // Otherwise if the input is moving the player left and the player is facing right...
            {
               // Flip();                                                                     // ... flip the flower.
                left = false;
                right = true;

            }
            if (left)
            {
                vectorTwo.y = vectorTwo.y - 0.8f; //0.2f;                                           //makes the vector.y equal to itself plus 0.2f
                vector.x = vector.x -1.2f;                                                 //makes the vector.x equal to itself plus 0.7f
                equObject.transform.position = new Vector2(vector.x, vectorTwo.y);          //Makes the position of the object equal to the players position plus some modifications so that the object doesnt teleport into the player
            }
            else if (right)
            {
                vectorTwo.y = vectorTwo.y - 0.8f;                                           //makes the vector.y equal to itself plus 0.2f
                vector.x = vector.x + 0.1f;                                                 //makes the vector.x equal to itself plus 0.7f
                equObject.transform.position = new Vector2(vector.x, vectorTwo.y);          //Makes the position of the object equal to the players position plus some modifications so that the object doesnt teleport into the player
            }

        }




        if (Input.GetKey(KeyCode.Q) && pickedUp)
        {
            equObject.transform.parent = null;                                              //this removes the object from being a child to the player 
            pickedUp = false;
            equObject.constraints = RigidbodyConstraints2D.None;                            // Un freezes the object so it can tilt over again
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 3;                        //Sets the gravity for the player to 3 (3 is the default gravity scale for the player)
        }




        void Flip()
        {
            
            CharacterController.facingRight = !CharacterController.facingRight;             //Switch the way the player is labelled as facing.
            Vector3 theScale = transform.localScale;                                        //Multiply the player's x local scale by -1.
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    void LateUpdate() //This part of the code determines if the player is moving upwards or downwards on the y axis by comparing past and current position.
    {

        currentheight = transform.position.y;
        travel = currentheight - previousheight;
        if (pickedUp && travel < 0f)                                                        //if the player is moving negatively on the y axis...
        {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0.2f;                     //Then the gravity for that object is set to 0.2f
        }
        else if (pickedUp && travel > 0)                                                    //if the player is moving positively on the y axis...
        {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 3f;                       //Then the gravity for that object is set to 3 (which is the default value)
        }
        previousheight = currentheight;
    }





    void FindClosestEnemy() //hittar viken box/enemy är närmast spelaren
    {
        float distanceToClosestEnemy = Mathf.Infinity;
        Enemy closestEnemy = null;
        Enemy[] allEnemies = GameObject.FindObjectsOfType<Enemy>();

        foreach (Enemy currentEnemy in allEnemies)
        {
            float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToEnemy < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = distanceToEnemy;
                closestEnemy = currentEnemy;
            }
        }
       // Debug.DrawLine(this.transform.position, closestEnemy.transform.position);
        dragObject = closestEnemy.gameObject;

        //Fetch the Collider from the GameObject
        objectCollider = closestEnemy.gameObject.GetComponent<Collider2D>();

    }


}
