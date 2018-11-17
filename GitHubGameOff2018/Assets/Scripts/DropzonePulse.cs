using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropzonePulse : MonoBehaviour
{
    public float approachSpeed = 0.015f;
    public float alphaUpperBound = 1f;
    public float alphaLowerBound = 0.15f;

    private Image img;

    void Awake()
    {
        img = gameObject.GetComponent<Image>();
    }

    public void StartPulse()
    {
        StopAllCoroutines();
        StartCoroutine(Pulse());
    }

    public void StopPulse()
    {
        StopAllCoroutines();
        StartCoroutine(RevertPulse());
    }

    IEnumerator RevertPulse()
    {
        // Make sure we set the alpha back to the original value
        Color c = img.color;
        while (c.a != alphaUpperBound)
        {
            c.a = Mathf.MoveTowards(c.a, alphaUpperBound, approachSpeed);
            img.color = c;
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator Pulse()
    {
        // Run this indefinitely
        while (true)
        {
            Color c = img.color;
            // Raise the alpha for a few seconds
            while (c.a != alphaUpperBound)
            {
                c.a = Mathf.MoveTowards(c.a, alphaUpperBound, approachSpeed);
                img.color = c;
                yield return new WaitForEndOfFrame();
            }
            // Lower the alpha for a few seconds
            while (c.a != alphaLowerBound)
            {
                c.a = Mathf.MoveTowards(c.a, alphaLowerBound, approachSpeed);
                img.color = c;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}