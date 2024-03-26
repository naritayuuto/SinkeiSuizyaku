using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    [SerializeField,Header("0‚È‚çƒ‰ƒ“ƒ_ƒ€A1‚È‚çæU,2‚È‚çŒãU"),Tooltip("0‚È‚çƒ‰ƒ“ƒ_ƒ€A1‚È‚çæU,2‚È‚çŒãU")]
    int _playNum = 0;

    [SerializeField, Header("“G‚Ì‹­‚³(0‚È‚çŠÈ’P,1‚È‚ç“ï‚µ‚¢)")]
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
