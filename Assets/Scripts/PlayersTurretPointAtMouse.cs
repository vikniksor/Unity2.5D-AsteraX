using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersTurretPointAtMouse : MonoBehaviour
{

    private Vector3 mousePoint3D;

    // Update is called once per frame
    void Update()
    {
        PointAtMouse();
    }

    private void PointAtMouse()
    {
        mousePoint3D = Camera.main.ScreenToWorldPoint(Input.mousePosition +
                            Vector3.back * Camera.main.transform.position.z);

        transform.LookAt(mousePoint3D, Vector3.back);
    }
}
