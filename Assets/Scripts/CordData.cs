using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum CordType
{
    diamond,
    spade,
    clover,
    heart
}
[Serializable]
public class CordData
{
    public Sprite _numImage;
    public int _num;
    public CordType _cordType;
}
