using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public CollectibleType collectibleType;
    [Tooltip("Range 0 - 100")]
    public int dropChange;
    public float effectTime;
}
