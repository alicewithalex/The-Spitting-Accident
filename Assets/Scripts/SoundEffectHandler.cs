using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    Explosion=0,
    Splat =1 
}

[CreateAssetMenu(fileName = "Sound Data")]
public class SoundData : ScriptableObject
{
    public SoundType Type;
    public GameObject Effect;
}

public class SoundEffectHandler : MonoBehaviour
{


    [Header("Sounde Effect")] 
    [SerializeField]
    private List<SoundData> SoundDatas;

    
    public void SpawnSoundEffect(SoundType soundType)
    {
        if (SoundDatas != null && SoundDatas.Count > 0)
        {
            Instantiate(SoundDatas.Find(x=>x.Type==soundType).Effect, transform.position, Quaternion.identity, null);
        }
    }
}