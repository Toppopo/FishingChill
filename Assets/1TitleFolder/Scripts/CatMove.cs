using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class CatMove : MonoBehaviour
{
    private Rigidbody2D rb;

    private float elapsedTime = 0.0f;
    private float durationTime = 6.0f;

    private bool isMove = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if(isMove)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                elapsedTime += Time.deltaTime;
                rb.velocity += new Vector2(1f, 0);
                if(elapsedTime >= durationTime)
                {
                    isMove = false;
                    elapsedTime = 0.0f;
                }
            }
        }
    }
}
