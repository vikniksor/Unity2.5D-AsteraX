using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltWVelocity : MonoBehaviour
{

    [Tooltip("The number of degrees that the ship will tilt at its maximum speed.")]
    [SerializeField] int degrees = 30;
    public bool tiltTowards = true;

    private int  prevDegrees = int.MaxValue;
    private float tan;

    Rigidbody rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        if (degrees != prevDegrees)
        {
            prevDegrees = degrees;
            tan = Mathf.Tan(Mathf.Deg2Rad * degrees);
        }
        Vector3 pitchDir = (tiltTowards) ? -rigidBody.velocity : rigidBody.velocity;
        pitchDir += Vector3.forward / tan * PlayersShip.MAX_SPEED;
        transform.LookAt(transform.position + pitchDir);
    }

}
