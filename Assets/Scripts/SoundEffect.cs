using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    private AudioSource _source;
    void Awake()
    {
        _source = GetComponent<AudioSource>();
        _source.loop = false;
        _source.Play();
        Destroy(gameObject,_source.clip.length*3);
    }

}