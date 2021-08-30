using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(OffScreenWrapper))]
public class PlayersBullet : MonoBehaviour
{

    static private Transform _BULLET_ANCHOR;

    static Transform BULLET_ANCHOR
    {
        get
        {
            if (_BULLET_ANCHOR == null)
            {
                GameObject bullet = new GameObject("BulletAnchor");
                _BULLET_ANCHOR = bullet.transform;
            }
            return _BULLET_ANCHOR;
        }
    }

    public float bulletSpeed = 20f;
    public float lifeTime = 2f;


    private void Start()
    {
        transform.SetParent(BULLET_ANCHOR, true);

        Invoke("DestroyMe", lifeTime);  // Set bullet to self-destruct 

        GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
    }

    void DestroyMe()
    {
        Destroy(gameObject);
    }

}
