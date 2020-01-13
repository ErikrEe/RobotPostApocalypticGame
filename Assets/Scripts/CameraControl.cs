using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Camera Camera1;
    public GameObject player;
    private Vector3 playerP;
    public GameObject LevelP;

    public float CameraZoomIn;
    public float CameraZoomOut;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
       
       
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("CameraFollow"))   // Checks if the player is colliding with the trigger
        {
            cameraFollow();       //if player is colliding, start the camerafollow method
            Camera1.transform.position = playerP;   // Cameras position switches to players position
            Camera1.orthographicSize = Mathf.Lerp(Camera1.orthographicSize, CameraZoomIn, Time.deltaTime);    //zoom in the camera smoothly
    
        }
      if (collision.CompareTag("CameraSwitch"))   //Checks if the player is colliding with the trigger
        {

            
            Camera1.transform.position = LevelP.transform.position;    // if player is colliding, move the camera to the position of the empty gameobject


            Camera1.orthographicSize = Mathf.Lerp(Camera1.orthographicSize, CameraZoomOut, Time.deltaTime); // zoom out smoothly
         

        }
       
    }

 




    void cameraFollow()
    {
        playerP = new Vector3 (Mathf.Lerp(Camera1.transform.position.x, player.transform.position.x, 2 * Time.deltaTime ), Camera1.transform.position.y, player.transform.position.z - 4); // makes the position for the camera be the same as the player on the x axis
      
    }
}
