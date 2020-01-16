using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Camera Camera1;

    public GameObject player;
    public GameObject LevelP1;
    public GameObject LevelP2;
    
    private Vector3 playerP;
   
    public float CameraZoomIn;
    public float CameraZoomOut;
    public float CameraOffsetY;
    private void Update()
    {
        cameraFollow();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("CameraFollow"))   // Checks if the player is colliding with the trigger
        {
            cameraFollow();       //if player is colliding, start the camerafollow method
            Camera1.transform.position = playerP;   // Cameras position switches to players position
            Camera1.orthographicSize = Mathf.Lerp(Camera1.orthographicSize, CameraZoomIn, Time.deltaTime);    //zoom in the camera smoothly
    
        }
        
      if (collision.CompareTag("CameraSwitch1"))   //Checks if the player is colliding with the trigger
        {

            
            Camera1.transform.position = LevelP1.transform.position;    // if player is colliding, move the camera to the position of the empty gameobject


            Camera1.orthographicSize = Mathf.Lerp(Camera1.orthographicSize, CameraZoomOut, 3 * Time.deltaTime); // zoom out smoothly
           
        }
        if (collision.CompareTag("CameraSwitch2"))   //Checks if the player is colliding with the trigger
        {


            Camera1.transform.position = LevelP2.transform.position;    // if player is colliding, move the camera to the position of the empty gameobject


            Camera1.orthographicSize = Mathf.Lerp(Camera1.orthographicSize, CameraZoomOut, 3 * Time.deltaTime); // zoom out smoothly

        }
    }

    void cameraFollow()
    {
        playerP = new Vector3 (Mathf.Lerp(Camera1.transform.position.x, player.transform.position.x, 2 * Time.deltaTime ), this.transform.position.y + CameraOffsetY, player.transform.position.z - 4); // makes the position for the camera be the same as the player on the x axis
          

    }
}
