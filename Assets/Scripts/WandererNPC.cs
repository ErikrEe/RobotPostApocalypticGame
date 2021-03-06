﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandererNPC : MonoBehaviour
{

    //Erik {

    public Animator animator;  //Animator variable for controlling the animations of the "NPC"

    internal Transform thisTransform;   //Transform variable that will access this objects transform and use it to move the object

    // The movement speed of the object
    public float moveSpeed = 0.2f;

    // A minimum and maximum time delay for taking a decision, *choosing a direction to move in*
    public Vector2 decisionTime = new Vector2(1, 4);
    internal float decisionTimeCount = 0;

    // The possible directions that the object can move in, right, left, up, down, and zero for staying in place
    //I added left & right twice to give those directions a higher probability of it happening
    internal Vector3[] moveDirections = new Vector3[] { Vector3.right, Vector3.left, Vector3.right, Vector3.left, Vector3.zero, Vector3.zero };
    internal int currentMoveDirection;

    // Use this for initialization
    void Start()
    {
        // Cache the transform for quicker access
        thisTransform = this.transform;

        // Set a random time delay for taking a decision ( changing direction, or standing in place for a while )
        decisionTimeCount = Random.Range(decisionTime.x, decisionTime.y);

        // Choose a movement direction, or stay in place
        ChooseMoveDirection();
    }

    // Update is called once per frame
    void Update()
    {
        // Move the object in the chosen direction at the set speed
         thisTransform.position += moveDirections[currentMoveDirection] * Time.deltaTime * moveSpeed;

        if (decisionTimeCount > 0) decisionTimeCount -= Time.deltaTime;  //If the decisionTimer hasn't reached 0...
        else                                                             //...Then subtract the current value by the amount of time passed...
        {                                                                //... (I.e - Subtract one frame every frame)

            // Choose a random time delay for taking a decision ( changing direction, or standing in place for a while )
            decisionTimeCount = Random.Range(decisionTime.x, decisionTime.y);

            // Choose a movement direction, or stay in place
            ChooseMoveDirection();
        }




        

    }

    void ChooseMoveDirection()
    {
        // Choose whether to move R or L or stay in place ?
        currentMoveDirection = Mathf.FloorToInt(Random.Range(0, moveDirections.Length));

        animator.SetBool("Speed", true);  //Play the "Speed" animation

        #region Useless Code (For now)
        /*
        if (moveDirections.Length > 1)
        {
            animator.SetBool("Speed", true);
        }

        if (moveDirections.Length < 1)
        {
            animator.SetBool("Speed", false);
        } */
        #endregion


        //Erik }
    }
}
