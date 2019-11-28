using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterControl : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform gunPosition;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = Instantiate(bulletPrefab, gunPosition.position, gunPosition.rotation);
            bullet.GetComponent<Bullet>().shootingDirection = gunPosition.right;
        }   
    }
}
