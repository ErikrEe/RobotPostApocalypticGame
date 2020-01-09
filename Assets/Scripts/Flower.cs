using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Flower : MonoBehaviour
{
    [SerializeField] //finns det något annat sätt att hitta "player" på?
    Rigidbody2D player;
    GameObject thisObject;


    public bool pickedUp = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && (player.transform.position - this.transform.position).sqrMagnitude < 1.3f * 1.3f) //This if statement is triggered whe the button E is pressed and when the player is close enough to the object in a 1.3*1.3 radius from the player.
        {
            //flower.transform.SetParent(player.transform);
            gameObject.transform.parent = player.transform; //denna fungerar också
            pickedUp = true;
            //thisObject.constraints = RigidbodyConstraints2D.FreezeRotation; // så att objectet inte välter när ma pker in i den
            //gameObject.tag = "PickedUp";
        }
        

        //#### ÄNDRA Q TILL E - dock loopar den om för fort och jag har ingen aning om hur man ska fixa det....
        if (Input.GetKey(KeyCode.Q) && pickedUp)   // gameObject.tag == "PickedUp" osäker på om det där ska vara med
        {
            gameObject.transform.parent = null; //this removes the object from being a child to the player 
            pickedUp = false;
            //gameObject.Constraints = RigidbodyConstraints2D.None; // så att objectet man vältas igen

        } 
    }
}
