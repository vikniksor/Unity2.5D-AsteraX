using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// When a GameObject exits the bounds of the OnScreenBounds, screen wrap it;
/// </summary>

public class OffScreenWrappr : MonoBehaviour
{
   
    // Update is called once per frame
    void Update()
    {
        // Adding an Update() method shows the "enabled" checkbox in the Inspector
    }

    private void OnTriggerExit(Collider other)
    {
        if (!enabled) { return; }

        // Ensure that the is OnScreenBounds
        ScreenBound bounds = other.GetComponent<ScreenBound>();
        if (bounds == null) { return; }

        ScreenWrap(bounds);
    }

    private void ScreenWrap(ScreenBound bounds)
    {
        // Wrap whichever direction is necessary
        Vector3 relativeLocation = bounds.transform.InverseTransformPoint(transform.position);

        // relativeLoc is in the local coords of OnScreenBounds, 0.5f is the screen edge.
        if (Mathf.Abs(relativeLocation.x) > 0.5f) { relativeLocation.x *= -1; }
        if (Mathf.Abs(relativeLocation.y) > 0.5f) { relativeLocation.y *= -1; }

        transform.position = bounds.transform.TransformPoint(relativeLocation);
    }
}
