using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Enemy/Enemy_Data", fileName = "Enemy_Data")]
public class Enemy_Data : ScriptableObject
{
    public float willGain;
    public float guiltGain;
    public bool hasRequirement;
    public float altWillGain;
    public float altGuiltGain;
}
