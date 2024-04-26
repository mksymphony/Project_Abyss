using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SimpleRandomWalkParameters_",menuName = "PCG/SimpleRandomWalkData")]
public class SimpleRandomWalkSo : ScriptableObject
{
    public int interations = 10;
    public int walkLength = 10;

    public bool startRandomlyEachIteration = true;
}
