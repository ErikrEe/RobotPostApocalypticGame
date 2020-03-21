using UnityEngine;

public class EquipItems : MonoBehaviour
{
    //Harriet's script, used to pick up and drop a flower object with "e", as well as drag and drop boxes with "e"
    [SerializeField]
    public Rigidbody2D equObject;
    public static Rigidbody2D playerVelocity, objectRigidbody;
    GameObject dragObject;
    //dessa avänds till att kalkylera spelarens storlek på X axeln
    Collider2D playerCollider, objectCollider;
    Vector2 vector, vectorTwo;

    public static bool pickedUp, objectDraged, objectLeft, objectRight, open, open2 = false;
    private float currentheight, previousheight, travel, offset;
    private bool left, right = false;

    public Animator animator;

    //Start is called before the first frame update
    void Start()
    {
        //Fetch the Rigidbody from the GameObject
        playerVelocity = GetComponent<Rigidbody2D>();

        //Fetch the Collider from the GameObject
        playerCollider = GetComponent<Collider2D>();

    }

    //Update is called once per frame
    void Update()
    {

        float verticalVelocity = playerVelocity.velocity.y;

        vector.x = gameObject.transform.position.x;
        vectorTwo.y = gameObject.transform.position.y;

        if (!objectDraged && !pickedUp)
        {
            //Går igenom FindClosestEnemy funktionen.
            FindClosestEnemy();
            PlayerMovement.moveSpeed = 40;
        }

        #region Dragging objects
        //if player presses e..
        if (Input.GetKeyDown(KeyCode.E))
        {
            //open becomes !open, allowing the use of e, for both dragging and relasing objects
            open = !open;

            //if open = true ,the player is close enough to the closest dragObject, isn't holding the flower (pickedUp = false), isn't above or below the object... 
            if (open && (dragObject.transform.position - this.transform.position).sqrMagnitude < (objectCollider.bounds.extents.x * 2) * (objectCollider.bounds.extents.y * 2) + 1 && pickedUp == false && dragObject.transform.position.y - 1 <= gameObject.transform.position.y + objectCollider.bounds.extents.y && gameObject.transform.position.y + objectCollider.bounds.extents.y <= dragObject.transform.position.y + 1 && (CharacterController.facingRight && gameObject.transform.position.x < dragObject.transform.position.x || !CharacterController.facingRight && gameObject.transform.position.x > dragObject.transform.position.x))
            {
                //sets the ObjectDraged bool true, which starts the objectDraged function
                objectDraged = true;
                animator.SetBool("IsPulling", true); //Erik, Sets the animation bool to true, which triggers the pulling animation
            }

            //if open = false and dragObject = true, or the "dragObject" is higher up than the player...
            if (!open && dragObject || gameObject.transform.position.y + objectCollider.bounds.extents.y < dragObject.transform.position.y)
            {
                objectDraged = false;
                animator.SetBool("IsPulling", false); //Erik, Stops playing the "Pulling" animation
            }

            //and the player is on the right side of the object (OBJECT SHOULD BE LEFT SIDE)
            if (gameObject.transform.position.x > dragObject.transform.position.x)
            {
                //makes the objectLeft bool true, and the objectRight bool false
                objectLeft = true;
                objectRight = false;
            }

            //and the player is on the left side of the object (OBJECT SHOULD BE RIGHT SIDE)
            if (gameObject.transform.position.x < dragObject.transform.position.x)
            {
                //makes the objectRight bool true, and the objectLeft bool fals
                objectRight = true;
                objectLeft = false;
            }
        }

        //If an object is being dragged..
        if (objectDraged)
        {
            PlayerMovement.moveSpeed = 20;
            //sets the offset equal to half the players gameObjects x axis size plus halft the (objectCollider) dragObjects x axis size plus 0.3f
            offset = playerCollider.bounds.extents.x + objectCollider.bounds.extents.x + 0.3f;

            //if the object is on the right side..
            if (objectRight)
            {
                //makes the vector.x equal to itself plus the choosen offset
                vector.x = vector.x + offset;
            }
            //if the object is on the left side..
            if (objectLeft)
            {
                //makes the vector.x equal to itself minus the choosen offset
                vector.x = vector.x - offset;;
            }

            //sets the dragObjects position equal to the variable vector.x and the dragObjects y axis position
            dragObject.transform.position = new Vector2(vector.x, dragObject.transform.position.y);
        }
        #endregion


        #region Picking up the flower
        if (Input.GetKeyDown(KeyCode.E))
        {
            open2 = !open2;
            if (open2 && (equObject.transform.position - this.transform.position).sqrMagnitude < 2f * 2f && objectDraged == false)
            {
                pickedUp = true;
                animator.SetBool("HoldingFlower", true);  //Sets the "HoldingFlower" bool to true
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
            if (!open2)
            {
                if (pickedUp)
                {

                    equObject.transform.parent = null;                                              //this removes the object from being a child to the player 
                    pickedUp = false;


                    animator.SetBool("HoldingFlower", false);

                    equObject.constraints = RigidbodyConstraints2D.None;                            // Un freezes the object so it can tilt over again
                    gameObject.GetComponent<Rigidbody2D>().gravityScale = 3;                        //Sets the gravity for the player to 3 (3 is the default gravity scale for the player)


                }
            }
        }

        if (pickedUp)
        {
            vectorTwo.y = vectorTwo.y + 1.55f;                                               //makes the vector.y equal to itself plus 1.55f
            vector.x = vector.x + 0.65f;                                                     //makes the vector.x equal to itself plus 0.65f                                                                                           

            if (CharacterController.move < 0 && !CharacterController.facingRight)           // If the input is moving the player right and the player is facing left...
            {
                left = true;
                right = false;

            }

            else if (CharacterController.move > 0 && CharacterController.facingRight)       // Otherwise if the input is moving the player left and the player is facing right...
            {

                left = false;
                right = true;

            }
            equObject.transform.position = new Vector2(vector.x, vectorTwo.y);          //Makes the position of the object equal to the players position plus some modifications so that the object doesnt teleport into the player
            if (left)
            {
                vectorTwo.y = vectorTwo.y - 0.8f; //0.2f;                                           //makes the vector.y equal to itself plus 0.2f
                vector.x = vector.x - 1.2f;                                                 //makes the vector.x equal to itself plus 0.7f
            }
            if (right)
            {
                vectorTwo.y = vectorTwo.y - 0.8f;                                           //makes the vector.y equal to itself plus 0.2f
                vector.x = vector.x + 0.1f;                                                 //makes the vector.x equal to itself plus 0.7f
            }
            equObject.transform.position = new Vector2(vector.x, vectorTwo.y);              //Makes the position of the object equal to the players position plus some modifications so that the object doesnt teleport into the player
        }
        #endregion

    }

    //This part of the code determines if the player is moving upwards or downwards on the y axis by comparing past and current position.
    void LateUpdate() 
    {

        currentheight = transform.position.y;
        travel = currentheight - previousheight;
        //if the player is moving negatively on the y axis...
        if (pickedUp && travel < 0f)                                                        
        {
            //Then the gravity for that object is set to 0.2f
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0.2f;                     
        }
        //if the player is moving positively on the y axis...
        else if (pickedUp && travel > 0)                                                    
        {
            //Then the gravity for that object is set to 3 (which is the default value we are using)
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 3f;                       
        }
        previousheight = currentheight;
    }

    #region finds and assigns the dragObject variable, objectCollider and objectRigidbody
    //finds which object with the "enemy" script equipped is closest to the player
    void FindClosestEnemy() 
    {
        float distanceToClosestEnemy = Mathf.Infinity;
        Enemy closestEnemy = null;
        Enemy[] allEnemies = GameObject.FindObjectsOfType<Enemy>();

        //for every object with the enemy script equipped..
        foreach (Enemy currentEnemy in allEnemies)
        {
            float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
            //updates what object with the enemy script is closest to the player
            if (distanceToEnemy < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = distanceToEnemy;
                closestEnemy = currentEnemy;
            }
        }
        //assigns the closest object with the enemy script to the dragObject variable
        dragObject = closestEnemy.gameObject;

        //Fetch the Collider from the closest object with the enemy script
        objectCollider = closestEnemy.gameObject.GetComponent<Collider2D>();

        //Fetch the Rigidbody from the closest object with the enemy script
        objectRigidbody = closestEnemy.gameObject.GetComponent<Rigidbody2D>();

    }
    #endregion


}
