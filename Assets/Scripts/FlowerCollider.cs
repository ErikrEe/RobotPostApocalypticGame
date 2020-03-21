using UnityEngine;

public class FlowerCollider : MonoBehaviour
{
    //Harriet's script, used to turn a collider on and off, when picking up and dropping the game object called Flower
    //Finding the right collider to use in the script
    public Collider2D collider;

    void Start()
    {
        //The collider is active as a trigger preventing it from looking as if it's collliding with objects
        collider.isTrigger = true;
    }

    void FixedUpdate()
    {
        // If the flower is picked up...
        if (EquipItems.pickedUp)
        {
            collider.isTrigger = false; //then the trigger is not enabled -> making the collider actually collide

        }
        //If the flower is not picked up...
        else if (!EquipItems.pickedUp)
        {
            //then the collider is used as a trigger -> making the collider NOT collide with objects
            collider.isTrigger = true;
        }
    }
}


