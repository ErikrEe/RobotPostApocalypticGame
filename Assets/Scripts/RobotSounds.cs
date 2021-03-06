﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class RobotSounds : MonoBehaviour
{
    //Harriet's script

    //used to find the right audiosources to play
    [SerializeField]
    AudioSource robotMovement, ambient1, ambient2, ambient3;

    int intNumber;

    private float randomTime = 120.0f;
    private float timeCounter = 0.0f;


    // Update is called once per frame
    void Update()
    {
        //if timeCounter has a higher value than randomTime
        if (timeCounter > randomTime)
        {
            //start the function GetRandom
            GetRandom(1, 4);
            //choose a new randomTime
            randomTime = Random.Range(60.0f, 180.0f);
            //set the timeCounter to 0.0f
            timeCounter = 0.0f;
            Debug.Log("timeCounter worked");
        }

        //every frame the time counter will increase
        timeCounter += Time.deltaTime;
    

        //if the player/robot is on the ground and moves...
        if (CharacterController.grounded && (CharacterController.move < 0 || CharacterController.move > 0))
        {
            //if the robotMovement is not playing...
            if (!robotMovement.isPlaying)
            {
                //play the robot sound
                robotMovement.Play(0);

            }
        }
        else
        {
            //stop playing the robotMovement sound
            robotMovement.Stop();
        }

        //if the intNumber is equal to 1...
        if (intNumber == 1)
        {
            //play ambient sound 1
            ambient1.Play(0);
            Debug.Log("played 1");
            intNumber = 0;
        }
        //if the intNumber is equal to 2..
        else if (intNumber == 2)
        {
            //play ambient sound 2
            ambient2.Play(0);
            Debug.Log("played 2");
            intNumber = 0;
        }
        //if the intNumber is equal to 3...
        else if (intNumber == 3)
        {
            //play ambient sound 3
            ambient3.Play(0);
            Debug.Log("played 3");
            intNumber = 0;
        }

    }

    //This function chooses a random number between 1-3 whenever it is called, and sets the intNumber equal to the randomly picked number
    int GetRandom(int min, int max)
    {
        //the int rand is equal to a random number between the max int and the min int
        int rand = Random.Range(min, max);
        Debug.Log("GetRandom worked and gave the value " + rand);
        intNumber = rand;
        return rand;


    }

}
