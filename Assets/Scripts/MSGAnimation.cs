using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSGAnimation : MonoBehaviour
{
    [SerializeField]
    SceneState _sceneState = null;
    public void StageStateChange()
    {
        _sceneState.StageState = StageState.erabu;
    }
}
