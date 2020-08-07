using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        Car car = other.GetComponentInParent<Car>();
        if (car)
        {
            car.Explode(transform.position);
        }
    }
    
}
