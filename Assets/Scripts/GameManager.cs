using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    [SerializeField,Header("0�Ȃ烉���_���A1�Ȃ��U,2�Ȃ��U"),Tooltip("0�Ȃ烉���_���A1�Ȃ��U,2�Ȃ��U")]
    int _playNum = 0;

    [SerializeField, Header("�G�̋���(0�Ȃ�ȒP,1�Ȃ���)")]
    int _enemyPower = 0;
    public static GameManager Instance => _instance;

    public int PlayNum { get => _playNum; set => _playNum = value; }
    public int EnemyPower { get => _enemyPower; set => _enemyPower = value; }

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
