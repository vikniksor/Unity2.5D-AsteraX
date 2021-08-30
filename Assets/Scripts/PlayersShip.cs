using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(Rigidbody))]  // We won`t need to check if rigidBody is null.
public class PlayersShip : MonoBehaviour
{

    static private PlayersShip _S;
    static public PlayersShip S
    {
        get { return _S; } 
        private set
        {
            if (_S != null) { Debug.LogWarning("Second attempt to set PLayerShip singleton _S."); }
            _S = value;
        }
    }


    [SerializeField] float shipSpeed = 10f;
    [SerializeField] GameObject bulletPrefab;

    Rigidbody rigidBody;


    private void Awake()
    {
        S = this;

        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float aX = CrossPlatformInputManager.GetAxis("Horizontal");
        float aY = CrossPlatformInputManager.GetAxis("Vertical");

        Vector3 velocity = new Vector3(aX, aY);
        if (velocity.magnitude > 1) { velocity.Normalize(); } // Avoid speed multiplying when moving at a diagonal.

        rigidBody.velocity = velocity * shipSpeed;


        // Mouse input for fire
        if (CrossPlatformInputManager.GetButtonDown("Fire1")) { Fire(); }

    }

    private void Fire()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos);

        GameObject bullet = Instantiate<GameObject>(bulletPrefab);
        // Set bullets direction
        bullet.transform.position = transform.position;
        bullet.transform.LookAt(mousePos3D);

    }

    static public float MAX_SPEED
    {
        get { return S.shipSpeed; }
    }
}
