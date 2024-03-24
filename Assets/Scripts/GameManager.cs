using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    [Tooltip("0�Ȃ烉���_���A1�Ȃ��U,2�Ȃ��U")]
    int _playNum = 0;

    public static GameManager Instance => _instance;

    public int PlayNum { get => _playNum; set => _playNum = value; }

    private void Awake()
    {
        CheckInstance();
    }
    private void CheckInstance()
    {
        if (!_instance)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
}
