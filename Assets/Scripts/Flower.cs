using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Flower : MonoBehaviour
{
    [SerializeField] // #####finns det något annat sätt att hitta "player" på?
    Rigidbody2D player;
    GameObject thisObject;
    [SerializeField] 
    Rigidbody2D flower;

    Vector2 vector;


    public bool pickedUp = false;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        vector.x = player.transform.position.x; //makes the variable vector equal to the players x position 
        if (Input.GetKeyDown(KeyCode.E) && (player.transform.position - this.transform.position).sqrMagnitude < 1.25f * 1.25f) //This if statement is triggered whe the button E is pressed and when the player is close enough to the object in a 1.3*1.3 radius from the player.
        {
            vector.x = vector.x + 0.7f; //makes the vector.x equal to itself plus 0.7f
            gameObject.transform.position = new Vector2(vector.x, gameObject.transform.position.y); //Makes the position of the game object equal to the players position plus 0.7f on the x axis
            gameObject.transform.parent = player.transform; //Makes the gameObject a child of the player (player from the serialize field player variable)
            pickedUp = true;  //changes this bool to true
            gameObject.transform.rotation = Quaternion.Euler(0, 0, -90); //changes the rotation of the object to 0,0,-90 so that the object is in its default position when picked up
            flower.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation; //Freezes the rotation and position of the object when it's picked up so it does not fall over or slide away from the player/parent
            //flower.constraints = RigidbodyConstraints2D.FreezeRotation; // Freezes the rotation of the object when it's picked up so it does not fall over
        }

       /* if(Input.GetKeyDown(KeyCode.A))// här försöker jag ändra så att blomman är på andra sidan av karaktären när den går åt vänster
        {

            vector.x = vector.x + 0.7f;
            gameObject.transform.position = new Vector2(vector.x, gameObject.transform.position.y); //Makes the position of the game object equal to the players position plus 0.7f on the x axis
        } */
        //fixa så att man kan glidflyga när man håller i blomman


        //#### ÄNDRA Q TILL E - dock loopar den om för fort och jag har ingen aning om hur man ska fixa det....
        if (Input.GetKey(KeyCode.Q) && pickedUp) 
        {
            gameObject.transform.parent = null; //this removes the object from being a child to the player 
            pickedUp = false;
            flower.constraints = RigidbodyConstraints2D.None; // Un freezes the object so it can tilt over again.

        } 


    }
}
