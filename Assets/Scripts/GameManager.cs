using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    [SerializeField,Header("0ならランダム、1なら先攻,2なら後攻"),Tooltip("0ならランダム、1なら先攻,2なら後攻")]
    int _playNum = 0;

    [SerializeField, Header("敵の強さ(0なら簡単,1なら難しい)")]
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
