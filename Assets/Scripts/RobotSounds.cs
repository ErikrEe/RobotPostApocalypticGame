using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class RobotSounds : MonoBehaviour
{

    //lägger in componenten själv
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
            Debug.LogError("timeCounter worked");
        }

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

        //## LÄGGA TILL RANDOM WAIT TIME MELLAN ALLT SÅ ATT LJUDEN SPELAS AT RANDOM POINTS!
        //if the intNumber is equal to 1, if the intNumber is equal to 2, if the intNumber is equal to 3...
        if (intNumber == 1)
        {
            ambient1.Play(0);
            Debug.LogError("played 1");
        }
        else if (intNumber == 2)
        {
            ambient2.Play(0);
            Debug.LogError("played 2");
        }
        else if (intNumber == 3)
        {
            ambient3.Play(0);
            Debug.LogError("played 3");
        }

    }

    //This function chooses a random number between 1-3 whenever it is called, and sets the intNumber equal to the randomly picked number
    int GetRandom(int min, int max)
    { 
        min = 1;
        max = 3;
        int rand = Random.Range(min, max);

        rand = intNumber;
        return rand;

        Debug.LogError("GetRandom worked" + intNumber);
    }

}
