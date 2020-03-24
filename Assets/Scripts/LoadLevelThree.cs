using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelThree : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(4);

        EquipItems.pickedUp = false;
        EquipItems.objectDraged = false;            //When Loading the level, the variables that check certain conditions...
        EquipItems.objectLeft = false;              //...(flower being picked up, dragging / pushing objects... 
        EquipItems.objectRight = false;             //...and facing directions) will be reset - Erik
        CharacterController.facingRight = true;
    }


}
