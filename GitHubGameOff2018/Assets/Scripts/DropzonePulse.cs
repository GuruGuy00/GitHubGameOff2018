﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropzonePulse : MonoBehaviour
{
    public float approachSpeed = 0.02f;
    public float alphaUpperBound = 1f;
    public float alphaLowerBound = 0.25f;
    private float currentAlpha = 0.5f;

    private Outline outline;

    private Coroutine routine;
    private bool keepGoing = true;

    void Awake()
    {
        outline = gameObject.GetComponent<Outline>();
        // Start the coroutine
        routine = StartCoroutine(Pulse());
    }

    public void StartPulse()
    {
        StopAllCoroutines();
        routine = StartCoroutine(Pulse());
    }

    IEnumerator Pulse()
    {
        // Run this indefinitely
        while (keepGoing)
        {
            Color outlineColor = outline.effectColor;

            // Raise the alpha for a few seconds
            while (currentAlpha != alphaUpperBound)
            {
                currentAlpha = Mathf.MoveTowards(currentAlpha, alphaUpperBound, approachSpeed);
                outlineColor.a = currentAlpha;
                outline.effectColor = outlineColor;
                Debug.Log("Current Alpha: " + currentAlpha);
                yield return new WaitForEndOfFrame();
            }

            // Lower the alpha for a few seconds
            while (currentAlpha != alphaLowerBound)
            {
                currentAlpha = Mathf.MoveTowards(currentAlpha, alphaLowerBound, approachSpeed);
                outlineColor.a = currentAlpha;
                outline.effectColor = outlineColor;
                Debug.Log("Current Alpha: " + currentAlpha);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}