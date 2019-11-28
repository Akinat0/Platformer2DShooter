using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private float speed = 10;

    public Vector3 shootingDirection;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        transform.position += shootingDirection * (speed * Time.deltaTime);
    }
}
