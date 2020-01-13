﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Camera Camera1;
   // public GameObject Camera2;
    public GameObject player;
    private Vector3 playerP;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
           playerP = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - 4);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("CameraSwitch"))
        {
            Camera1.transform.position = playerP;
            Camera1.orthographicSize = 2;
        }
    }
}