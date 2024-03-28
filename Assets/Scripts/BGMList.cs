using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class BGMList : ScriptableObject
{
    public BGMData[] BGMDatas;
}
[Serializable]
public class BGMData
{
    public AudioClip Clip;
    public BGMType BGMType;
}

public enum BGMType
{
    Title,
    Game,
    Victory,
    Lose,
    Draw
}

