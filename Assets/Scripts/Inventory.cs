using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public GameObject weaponPrefab;

    public Transform weaponTransform;
    
    public float duration = 1f;
    public float weaponForce = 100f;
    
    public event Action OnWeaponDeactivated;

    private bool activated = false;
    
    public void ActivateWeapon()
    {
        if (!activated)
        {
            StartCoroutine(SpawnParticles());
        }
    }

    private IEnumerator SpawnParticles()
    {
        int amount = 10;
        int current = 0;
        while (current < amount)
        {
            SpawnParticle();
            current++;
            yield return  new WaitForSeconds(duration/amount);
        }

        OnWeaponDeactivated();
    }

    private void SpawnParticle()
    {
        GameObject go = Instantiate(weaponPrefab, null);
        go.transform.position = weaponTransform.transform.position;

        Rigidbody rigidbody = go.GetComponent<Rigidbody>();
        
        rigidbody.AddForce(weaponTransform.up*weaponForce,ForceMode.Force);
        
        Destroy(go,3f);
    }

  
    
}
