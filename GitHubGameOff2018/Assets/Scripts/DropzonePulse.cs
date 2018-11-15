using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DRopzonePulse : MonoBehaviour
{
    // Grow parameters
    public float approachSpeed = 0.02f;
    public float alphaUpperBound = 255f;
    public float alphaLowerBound = 100f;
    private float currentRatio = 1;

    // The text object we're trying to manipulate
    private Outline outline;
    private float originalFontSize;

    // And something to do the manipulating
    private Coroutine routine;
    private bool keepGoing = true;
    private bool closeEnough = false;

    // Attach the coroutine
    //void Awake()
    //{
    //    // Find the Outline element we want to use
    //    this.outline = this.gameObject.GetComponent<Outline>();
    //    // Start the coroutine
    //    this.routine = StartCoroutine(this.Pulse());
    //}

    //IEnumerator Pulse()
    //{
        /*
        // Run this indefinitely
        while (keepGoing)
        {
            // Get bigger for a few seconds
            while (this.currentRatio != this.alphaUpperBound)
            {
                // Determine the new ratio to use
                currentRatio = Mathf.MoveTowards(currentRatio, alphaUpperBound, approachSpeed);

                // Update our text element
                this.text.transform.localScale = Vector3.one * currentRatio;
                this.text.text = "Growing!";

                yield return new WaitForEndOfFrame();
            }

            // Shrink for a few seconds
            while (this.currentRatio != this.alphaLowerBound)
            {
                // Determine the new ratio to use
                currentRatio = Mathf.MoveTowards(currentRatio, alphaLowerBound, approachSpeed);

                // Update our text element
                this.text.transform.localScale = Vector3.one * currentRatio;
                this.text.text = "Shrinking!";

                yield return new WaitForEndOfFrame();
            }
        }
        */
    //}
}