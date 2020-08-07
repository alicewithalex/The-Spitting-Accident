using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Weapons")]
public class WeaponData : ScriptableObject
{
    public GameObject prefab;
    public Image inventoryIcon;
    public float bonusValue;
    [Range(0,100)]
    public float crashPercentage;

    public bool positionInHand;
}
