﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool shaking = false;
    private bool bumped = false;
    public GameObject cursor;
    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            cursor.GetComponent<Cursor>().bumped = true;
        }
        if (Input.GetKeyDown("s"))
        {
            cursor.GetComponent<Cursor>().shaking = true;
        }
        if (Input.GetKeyDown("d"))
        {
            canvas.GetComponent<Canvas>().rotate = true;
        }
        if (Input.GetKeyDown("f") && !canvas.GetComponent<Canvas>().jump && !canvas.GetComponent<Canvas>().fall)
        {
            canvas.GetComponent<Canvas>().jump = true;
        }
    }


}