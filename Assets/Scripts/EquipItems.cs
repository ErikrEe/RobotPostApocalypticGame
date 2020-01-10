using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipItems : MonoBehaviour
{
    [SerializeField]
    public Rigidbody2D equObject;
    public static bool pickedUp = false;
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
        vectorTwo.y = equObject.transform.position.y;
        if (Input.GetKeyDown(KeyCode.E) && (gameObject.transform.position - this.transform.position).sqrMagnitude < 1.5f * 1.5f)
        {
            vector.x = vector.x + 0.7f; //makes the vector.x equal to itself plus 0.7f
            vectorTwo.y = vectorTwo.y + 1f; //makes the vector.x equal to itself plus 0.7f
            equObject.transform.position = new Vector2(vector.x, vectorTwo.y); //Makes the position of the game object equal to the players position plus 0.7f on the x axis
            equObject.transform.parent = gameObject.transform;
            pickedUp = true;
            equObject.transform.rotation = Quaternion.Euler(0, 0, -90);
            equObject.constraints = RigidbodyConstraints2D.FreezeRotation;




            //GameObject.FindGameObjectsWithTag("equObject")
        }
        if(pickedUp)
        {

            vector.x = vector.x + 0.7f; //makes the vector.x equal to itself plus 0.7f
            equObject.transform.position = new Vector2(vector.x, vectorTwo.y); //Makes the position of the game object equal to the players position plus 0.7f on the x axis
           // vectorTwo.y = vectorTwo.y + 1f; //makes the vector.x equal to itself plus 0.7f
        }

        if (Input.GetKey(KeyCode.Q) && pickedUp)
        {
            equObject.transform.parent = null; //this removes the object from being a child to the player 
            pickedUp = false;
            equObject.constraints = RigidbodyConstraints2D.None; // Un freezes the object so it can tilt over again.

        }
    }
}
