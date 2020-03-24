using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    //Harriet { THis script is used to add a parallax effect to the background layers 

    //used to assign variables for how long and where the background sprites spawn
    private float length, startpos;
    public GameObject cam;
    //how much speed/movement is applied
    public float parallaxEffect;

    // Start is called before the first frame update
    void Start()
    {
        //here we assign the start possition of the sprites
        startpos = transform.position.x;
        //here we assign the length of the sprites
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        //how far we've moved realative to the camera
        float temp = (cam.transform.position.x * (1 - parallaxEffect));

        //finds how far the camera has moved in the world space
        float dist = (cam.transform.position.x * parallaxEffect);

        //make the backgrounds move with the camera
        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        //make the backgrounds loop if they're far enought from the camera
        if (temp > startpos + length)
        {
            startpos += length;
        }
        else if (temp < startpos - length)
        {
            startpos -= length;
        }
    }

    //Harriet }
}
