using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerCollider : MonoBehaviour
{

    public Collider2D collider;

    void Start()
    {
        //collider.enabled = false;
        collider.isTrigger = true;
    }


    void FixedUpdate()
    {

        // Cast a ray to the right.
        // RaycastHit2D hit = Physics2D.Raycast((transform.position), Vector2.right, 2f); //,11

        // If it hits something...
         if (EquipItems.pickedUp) // && hit.collider != null
         {
             //movement speed = 0
             //PlayerMovement.moveSpeed = 0;
             //Toggle the Collider on and off when pressing the space bar
             //collider.enabled = true;
                         //collider = collider.enabled;
                                 collider.isTrigger = false;


         }
         else if (!EquipItems.pickedUp) //DEN FLIPPAR INTE!!!!! //hit.collider == null || 
         {
             //movement speed = movement speed
             //PlayerMovement.moveSpeed = 40f;
             //collider.enabled = false;
                         //collider = !collider.enabled;
                                 collider.isTrigger = true;


         }
       /* if (Input.GetKeyDown(KeyCode.E))
        {
            //Toggle the Collider on and off when pressing e
            collider.enabled = !collider.enabled;
        }*/

    }
}
