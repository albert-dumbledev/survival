using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Loot
{
    [SerializeField]
    public int dropPercentage;
    [SerializeField]
    public int dropAmount;
    [SerializeField]
    public GameObject dropabble;
}
