using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class SEList : ScriptableObject
{
    public SEData[] SEDatas; 
}
[Serializable]
public class SEData
{
    public AudioClip Clip;
    public SEType SEType;
}

public enum SEType
{
   Button,
   SelectButton,
   CordReturn,
   Pair,
   NotPair
}

