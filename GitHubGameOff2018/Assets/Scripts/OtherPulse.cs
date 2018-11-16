using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OtherPulse : MonoBehaviour
{
    public float approachSpeed = 0.015f;
    public float alphaUpperBound = 1f;
    public float alphaLowerBound = 0.15f;
    private float currentAlpha = 0.5f;

    private Image img;

    private Coroutine routine;
    private bool keepGoing = true;

    void Awake()
    {
        img = gameObject.GetComponent<Image>();
        // Start the coroutine
        routine = StartCoroutine(Pulse());
    }

    IEnumerator Pulse()
    {
        // Run this indefinitely
        while (keepGoing)
        {
            Color c = img.color;

            // Raise the alpha for a few seconds
            while (currentAlpha != alphaUpperBound)
            {
                currentAlpha = Mathf.MoveTowards(currentAlpha, alphaUpperBound, approachSpeed);
                c.a = currentAlpha;
                img.color = c;
                Debug.Log("Current Alpha: " + currentAlpha);
                yield return new WaitForEndOfFrame();
            }

            // Lower the alpha for a few seconds
            while (currentAlpha != alphaLowerBound)
            {
                currentAlpha = Mathf.MoveTowards(currentAlpha, alphaLowerBound, approachSpeed);
                c.a = currentAlpha;
                img.color = c;
                Debug.Log("Current Alpha: " + currentAlpha);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}