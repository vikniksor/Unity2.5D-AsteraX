//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// <para>This class should be attached to a child of Camera.main. It triggers various
///  behaviors to happen when a GameObject exits the screen.</para>
/// <para>NOTE: Camera.main must be orthographic.</para>
/// <para>NOTE: This GameObject must have a BoxCollider attached.</para>
/// <para>NOTE: If Camera.main is going to move or rotate over time, then it will need
///  to have a Rigidbody attached so that the physics engine properly updates the 
///  position and rotation of this BoxCollider.</para>
/// </summary>
[RequireComponent(typeof(BoxCollider))]
public class ScreenBound : MonoBehaviour
{
    static private ScreenBound S;  // Private but unpotected Singleton.


    public float zScale = 10;

    Camera cam;
    BoxCollider boxCollider;
    float cachedOrthographicSize, cachedAspect;
    Vector3 cachedCamScale;


    private void Awake()
    {
        S = this;

        cam = Camera.main;

        if (!cam.orthographic)
        {
            Debug.LogError("ScaleToCamera:Start() - Camera.main needs to be orthographic " +
                           "for ScaleToCamera to work, but this camera is not orthographic.");
        }

        boxCollider = GetComponent<BoxCollider>();
        // Setting boxCollider.size to 1 ensures that other calculations will be correct.
        boxCollider.size = Vector3.one;

        transform.position = Vector3.zero;
        ScaleSelf();
    }

    private void Update()
    {
        ScaleSelf();
    }

    private void ScaleSelf()
    {
        if (cam.orthographicSize != cachedOrthographicSize || cam.aspect != cachedAspect
            || cam.transform.localScale != cachedCamScale)
        {
            transform.localScale = CalculateScale();
        }
    }

    private Vector3 CalculateScale()
    {
        cachedOrthographicSize = cam.orthographicSize;
        cachedAspect = cam.aspect;
        cachedCamScale = cam.transform.localScale;

        Vector3 scaleDesired, scaleColl;

        scaleDesired.z = zScale;
        scaleDesired.y = cam.orthographicSize * 2;
        scaleDesired.x = scaleDesired.y * cam.aspect;

        // This line makes use of the Vector3 extension method defined in Vector3Extensions
        scaleColl = scaleDesired.ComponentDivide(cachedCamScale);
        return scaleColl;
    }


    static public Vector3 RANDOM_ON_SCREEN_LOC
    {
        get
        {
            Vector3 min = S.boxCollider.bounds.min;
            Vector3 max = S.boxCollider.bounds.max;
            Vector3 local = new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), 0);
            return local;
        }
    }


    static public Bounds BOUNDS
    {
        get
        {
            if (S == null)
            {
                Debug.LogError("ScreenBounds.BOUNDS - ScreenBounds.s is null");
                return new Bounds();            
            }
            if (S.boxCollider == null)
            {
                Debug.LogError("ScreenBounds.BOUNDS - ScreenBounds.S.boxCollider is null");
                return new Bounds();
            }
            return S.boxCollider.bounds;
        }
    }


    static public bool OOB(Vector3 worldPos)
    {
        Vector3 localPos = S.transform.InverseTransformPoint(worldPos);
        // Find in which dimension the locPos is furthest from the origin
        float maxDistance = Mathf.Max(Mathf.Abs(localPos.x), Mathf.Abs(localPos.y), Mathf.Abs(localPos.z));
        // If that furthest distance is 0.5f, then worldPos is out of bounds.
        return (maxDistance > 0.5f);
    }

    static public int OOB_X(Vector3 worldPos)
    {
        Vector3 localPos = S.transform.InverseTransformPoint(worldPos);
        return OOB_(localPos.x);
    }
    static public int OOB_Y(Vector3 worldPos)
    {
        Vector3 localPos = S.transform.InverseTransformPoint(worldPos);
        return OOB_(localPos.y);
    }
    static public int OOB_Z(Vector3 worldPos)
    {
        Vector3 localPos = S.transform.InverseTransformPoint(worldPos);
        return OOB_(localPos.z);
    }


    static private int OOB_(float num)
    {
        if (num > 0.5f) return 1;
        if (num < -0.5f) return -1;
        return 0;
    }
}
