using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Flower : MonoBehaviour
{
    [SerializeField] //finns det något annat sätt att hitta "player" på?
    Rigidbody2D player;
    GameObject thisObject;
    [SerializeField] //finns det något annat sätt att hitta "player" på?
    Rigidbody2D flower;


    public bool pickedUp = false;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && (player.transform.position - this.transform.position).sqrMagnitude < 1.3f * 1.3f) //This if statement is triggered whe the button E is pressed and when the player is close enough to the object in a 1.3*1.3 radius from the player.
        {
            //flower.transform.SetParent(player.transform);
            gameObject.transform.parent = player.transform; //denna fungerar också
            pickedUp = true;
            //transform.Rotate(0, 0, -90);
            gameObject.transform.rotation = Quaternion.Euler(0, 0, -90); //ändrar rotationen till 0,0,-90 så att objectet står upp när det är Picked Up
            flower.constraints = RigidbodyConstraints2D.FreezeRotation; // så att objectet inte välter när ma pker in i den
            //gameObject.tag = "PickedUp";
        }
        

        //#### ÄNDRA Q TILL E - dock loopar den om för fort och jag har ingen aning om hur man ska fixa det....
        if (Input.GetKey(KeyCode.Q) && pickedUp)   // gameObject.tag == "PickedUp" osäker på om det där ska vara med
        {
            gameObject.transform.parent = null; //this removes the object from being a child to the player 
            pickedUp = false;
            flower.constraints = RigidbodyConstraints2D.None; // så att objectet man vältas igen

        } 

       /* if (Input.GetButtonDown("Jump") && pickedUp)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, PlayerController.jumpHeight), ForceMode2D.Impulse);
        } */
    }
}
