﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamper : MonoBehaviour
{
    float timer = 0;
    Vector3 stampPos;
    Vector3 currentPos;
    Vector3 mousePos;
    Vector3 stampSize;
    Quaternion rotation;
    bool canClick = false;
    bool dragStamp = false;

    public GameObject stampSprite;

    enum DragStates
    {
        Dragged,
        Free
    };

    DragStates dragState = DragStates.Free;


    void Start()
    {
        stampPos = gameObject.transform.position;
        stampSize = gameObject.transform.localScale;
        rotation = gameObject.transform.rotation;
    }

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = -1;

        switch (dragState)
        {
            case DragStates.Free:
                {
                    gameObject.transform.localScale = stampSize;
                    gameObject.transform.rotation = rotation;
                    currentPos = gameObject.transform.position;
                    if (currentPos != stampPos)
                    {
                        timer = 0;
                        timer += Time.deltaTime;
                        gameObject.transform.position = new Vector3(Mathf.Lerp(currentPos.x, stampPos.x, timer / 0.075f), Mathf.Lerp(currentPos.y, stampPos.y, timer / 0.075f), currentPos.z);
                    }

                    if (canClick)
                    {
                        if (Input.GetMouseButtonDown(0) && dragStamp == false)
                        {
                            dragState = DragStates.Dragged;
                        }
                    }


                    break;
                }

            case DragStates.Dragged:
                {
                    gameObject.transform.position = mousePos;
                    gameObject.transform.localScale = stampSize * 1.2f;
                    gameObject.transform.rotation = new Quaternion(0.0f, 0.0f, 90.0f, gameObject.transform.rotation.w);


                    if (Input.GetMouseButtonDown(0))
                    {
                        Vector3 spawnPos = new Vector3(mousePos.x, mousePos.y, -0.6f);
                        Instantiate(stampSprite, spawnPos, Quaternion.identity);
                        dragState = DragStates.Free;
                    }

                    break;
                }
        }
    }

    private void OnMouseOver()
    {
        canClick = true;
    }

    private void OnMouseExit()
    {
        canClick = false;
    }
}
