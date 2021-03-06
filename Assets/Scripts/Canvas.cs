﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas : MonoBehaviour
{
    public GameObject bg;
    private SpriteRenderer bg_render;
    public Sprite no_light_bg;
    private Sprite orig_bg;
    public bool isClicking = false;
    public GameObject mask;
    public bool jump = false;
    public bool fall = false;
    public bool rotate = false;
    public bool lights = false;
    private float jump_timer = 0.25f;
    private float rotate_timer = 1f;
    // Start is called before the first frame update
    void Start()
    {
        bg_render = bg.GetComponent<SpriteRenderer>();
        orig_bg = bg_render.sprite;
        mask.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (lights)
        {
            this.gameObject.GetComponent<AudioSource>().Play();
            StartCoroutine(WaitAndDoSomething());
            lights = false;
        }

        if (rotate)
        {
            this.gameObject.transform.Rotate(new Vector3(0f, 0f, 1f) * 360 * Time.deltaTime);
            rotate_timer -= Time.deltaTime;
            if (rotate_timer < 0)
            {
                this.gameObject.transform.rotation = Quaternion.identity;
                rotate_timer = 1f;
                rotate = false;
            }
        }
        if (jump)
        {
            float step = 10 * Time.deltaTime;
            this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, this.gameObject.transform.position + new Vector3(0, 100, 0), step);
            jump_timer -= Time.deltaTime;
            if (jump_timer < 0)
            {
                jump_timer = 0.25f;
                jump = false;
                fall = true;
            }
        }
        if (fall)
        {
            float step = 10 * Time.deltaTime;
            this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, this.gameObject.transform.position - new Vector3(0, 100, 0), step);
            jump_timer -= Time.deltaTime;
            if (jump_timer < 0)
            {
                jump_timer = 0.25f;
                fall = false;
            }
        }
    }
    IEnumerator WaitAndDoSomething()
    {
        bg_render.sprite = no_light_bg;
        mask.SetActive(true);
        yield return new WaitForSeconds(1f);//change duration using this
        bg_render.sprite = orig_bg;
        mask.SetActive(false);
    }

    void OnMouseDown()
    {
        isClicking = true;
    }

    void OnMouseUp()
    {
        isClicking = false;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "cursor")
        {
            isClicking = false;
        }
    }
}