using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class Car : MonoBehaviour
{
    public float speed = 10f;
    public float explosionForce = 100f;
    public float torqueForce = 10f;
    public float explosionRadius = 10f;
    public float deactivateAfter = 3f;
    private Rigidbody _rigidbody;
    private ParticleSystem _vfx;

    private bool broken = false;

    public void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _vfx = GetComponentInChildren<ParticleSystem>();
        _rigidbody.WakeUp();
    }

    private void FixedUpdate()
    {
        if(!broken)
            _rigidbody.AddForce(_rigidbody.transform.forward*speed*Time.fixedDeltaTime,ForceMode.VelocityChange);
    }

    private void OnEnable()
    {
        if (_rigidbody != null)
        {
            _rigidbody.WakeUp();
            broken = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       
    }

    public void Explode(Vector3 position)
    {
        if (!broken)
        {
            broken = true;
            print("Explode");
            _rigidbody.AddExplosionForce(explosionForce,position+Random.Range(-1f,1f)*transform.forward,explosionRadius);
            _rigidbody.AddTorque(Vector3.forward*torqueForce);
            GameSingleton.instance.AddScore(GameSingleton.instance.scorePerCar);

            if (_vfx.isPlaying == false)
            {
                _vfx.Play();
                GameSingleton.instance._soundEffectHandler.SpawnSoundEffect(SoundType.Explosion);
            }

            StartCoroutine(DeactivateAfterTime());
        }
    }
    
    private IEnumerator DeactivateAfterTime()
    {
        yield return  new WaitForSeconds(deactivateAfter);
        ToggleCar(false);
    }

    public void ToggleCar(bool value)
    {

        _rigidbody.useGravity = value;
        _rigidbody.isKinematic = !value;
        
        
        if (!value)
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }
        gameObject.SetActive(value);
    }
}
