using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateOnTrigger : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.transform.root.tag.Equals("Car"))
      {
         Car car = other.GetComponentInParent<Car>();
         car.ToggleCar(false);
      }
   }
}
