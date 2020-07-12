﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public GameObject bg;
    public bool isDay;
    private bool gameOver = false;
    public GameObject ImageComparer;
    public GameObject bikemen;
    public GameObject angery;
    public GameObject nerbous;
    public GameObject elefun;
    public GameObject wind;
    public float timer = 30f;
    public float rngTimer = 5f;
    private float rngMin;
    public GameObject cursor;
    public GameObject canvas;
    public GameObject timerBarObj;
    private Slider timerBar;
    // Start is called before the first frame update
    void Start()
    {
        timerBar = timerBarObj.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {

        angery.SetActive(cursor.GetComponent<Cursor>().bumped);
        nerbous.SetActive(cursor.GetComponent<Cursor>().shaking);
        timerBar.value = timer;
        if (timer <= 0f && !gameOver)
        {
            gameOver = true;
            Finish();
        }
        else
        {
            if (!canvas.GetComponent<Canvas>().isClicking || Time.frameCount % 2 == 0)
            {
                timer -= Time.deltaTime;
            }
        }
        if (timer > 5f)
        {
            if (rngTimer < 0)
            {
                rngTimer = Random.Range(3f, 3f + rngMin);
                rngMin = (int)(5 * (timer / 30f));
                if (isDay)
                {
                    DoRandomEvent();
                }
                else
                {
                    DoRandomEventWithLight();
                }
            }
            rngTimer -= Time.deltaTime;
        }
    }

    void DoRandomEvent()
    {
        int select = Random.Range(0, 4);
        switch (select)
        {
            case 0:
                BumpEvent();
                break;
            case 1:
                ShakeEvent();
                break;
            case 2:
                RotateEvent();
                break;
            case 3:
                JumpEvent();
                break;
        }
    }

    void DoRandomEventWithLight()
    {
        int select = Random.Range(0, 5);
        switch (select)
        {
            case 0:
                BumpEvent();
                break;
            case 1:
                ShakeEvent();
                break;
            case 2:
                RotateEvent();
                break;
            case 3:
                JumpEvent();
                break;
            case 4:
                LightEvent();
                break;
        }
    }

    void BumpEvent()
    {
        Instantiate(bikemen);
        cursor.GetComponent<Cursor>().bumped = true;
    }

    void ShakeEvent()
    {
        cursor.GetComponent<Cursor>().shaking = true;
    }

    void RotateEvent()
    {
        Instantiate(wind);
        canvas.GetComponent<Canvas>().rotate = true;
    }

    void JumpEvent()
    {
        if (!canvas.GetComponent<Canvas>().jump && !canvas.GetComponent<Canvas>().fall)
        {
            Instantiate(elefun);
            canvas.GetComponent<Canvas>().jump = true;
        }
    }

    void LightEvent()
    {
        canvas.GetComponent<Canvas>().lights = true;

    }


    void Finish()
    {
        ImageComparer.GetComponent<TestCVScript>().gameEnded = true;
    }
}
