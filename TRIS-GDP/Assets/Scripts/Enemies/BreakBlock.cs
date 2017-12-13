﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBlock : MonoBehaviour
{
    private enum State
    {
        ready = 0,
        breaking = 1,
        broken = 2,
        restoring = 3
    }
    private State state;
    private Animator anim;

    void Start()
    {
        state = State.ready;
        anim = GetComponentInParent<Animator>();
    }


    void OnTriggerStay2D(Collider2D other)
    {
        BreakIfTrisAndReady(other);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        BreakIfTrisAndReady(other);
    }

    void BreakIfTrisAndReady(Collider2D other)
    {
        if(state == State.ready && (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Brutus")))
        {
            GameObject tris = other.gameObject;
            //PlayerMovement pm = tris.GetComponent<PlayerMovement>();
            SpriteRenderer sr = tris.GetComponent<SpriteRenderer>();


            //Debug.Log("Colisión con TRIS. inverted: " + );

            if((tris.transform.position.y >= this.transform.position.y && !sr.flipY) 
            || (tris.transform.position.y <= this.transform.position.y && sr.flipY)
            )
                this.breaking();
        }
    }

    private void breaking()
    {
        Debug.Log("Breaking block at " + transform.position.ToString());
        anim.SetTrigger("break");
        state = State.breaking;
    }

    private void broken()
    {
        this.gameObject.layer = Layer.IGNORE_RAYCAST;
        state = State.broken;
    }

    private void restored()
    {
        this.gameObject.layer = Layer.GROUND;
        state = State.ready;
    }

}
