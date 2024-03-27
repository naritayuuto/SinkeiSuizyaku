using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager _instance;
    [SerializeField, Header("タイマーにするテキスト")]
    TextMeshProUGUI _timerText = null;
    [SerializeField, Header("誰の手番か画面内で表示するテキスト")]
    TextMeshProUGUI _playTurnText = null;
    [SerializeField, Header("誰の手番かアニメーションで表示するテキスト")]
    TextMeshProUGUI _animPlayTurnText = null;
    [SerializeField, Header("ペア数をカウントするテキスト")]
    TextMeshProUGUI _playerPairText = null;
    [SerializeField, Header("ペア数をカウントするテキスト")]
    TextMeshProUGUI _enemyPairText = null;
    [SerializeField, Header("結果画面に表示するMSGテキスト")]
    TextMeshProUGUI _finishTextOne = null;
    [SerializeField, Header("結果画面に表示するMSGテキスト")]
    TextMeshProUGUI _finishTextTwo = null;
    [SerializeField, Header("結果画面に表示する勝負の結果")]
    TextMeshProUGUI _playerResult = null;
    [SerializeField, Header("結果画面に表示する勝負の結果")]
    TextMeshProUGUI _enemyResult = null;
    [SerializeField, Header("結果画面、得点")]
    TextMeshProUGUI _playerPairResult = null;
    [SerializeField, Header("結果画面、得点")]
    TextMeshProUGUI _enemyPairResult = null;
    [SerializeField, Header("結果画面")]
    TextMeshProUGUI _TimerResult = null;
    [Tooltip("ターンが切り替わった時に表示する")]
    string _playerTurnMSG = "自分の手番";
    [Tooltip("ターンが切り替わった時に表示する")]
    string _enemyTurnMSG = "相手の手番";
    [Tooltip("プレイヤーのペア数")]
    int _playerPair = 0;
    [Tooltip("敵のペア数")]
    int _enemyPair = 0;
    [Tooltip("分")]
    float _minutes = 0;
    [SerializeField,Header("デバッグ用"),Tooltip("秒")]
    float _seconds = 0;
    [Tooltip("アニメーションの値")]
    const int _animINTNum = 0;
    [Tooltip("アニメーションの値")]
    const float _animFLOATNum = 0.0f;
    [Tooltip("秒数の最大値")]
    const float _secondsMax = 60f;
    [SerializeField, Header("_animPlayTurnTextのアニメーション")]
    Animator _textAnim = null;
    [SerializeField, Header("終了時のアニメーション")]
    public Animator ClearAnim = null;

    public static UIManager Instance => _instance;
    public int PlayerPair
    {
        get => _playerPair;
        set
        {
            _playerPair = value;
            _playerPairText.text = $"{_playerPair}";
        }
    }
    public int EnemyPair
    {
        get => _enemyPair;
        set
        {
            _enemyPair = value;
            _enemyPairText.text = $"{_enemyPair}";
        }
    }

    private void Awake()
    {
        _instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.Pause || !GameManager.Instance.Finish)
        {
            Timer();
        }
    }

    void Timer()
    {
        _seconds += Time.deltaTime;
        if (_seconds >= _secondsMax)
        {
            _minutes++;
            _seconds -= _secondsMax;
        }
        _timerText.text = $"{_minutes.ToString("00")}:{Mathf.Floor(_seconds).ToString("00")}";
    }


    public void PlayTurn(TurnState turnState)
    {
        _animPlayTurnText.text = turnState == TurnState.player1 ? _playerTurnMSG : _enemyTurnMSG;
        _playTurnText.text = _animPlayTurnText.text;
        //値を指定しないと二回目の再生が出来なかった為以下の処理にしています
        _textAnim.Play("Text", _animINTNum, _animFLOATNum);
    }

    public void Result()
    {
        _playerPairResult.text = $"{_playerPair}";
        _enemyPairResult.text = $"{_enemyPair}";
        _TimerResult.text = _timerText.text;
        if (_playerPair == _enemyPair)
        {
            _playerResult.text = "Draw";
            _enemyResult.text = "Draw";
            DrawText();
            ClearAnim.Play("Draw");
        }
        else if (_playerPair > _enemyPair)
        {
            _playerResult.text = "Winner";
            _enemyResult.text = "Loser";
            ClearText();
            ClearAnim.Play("Clear");
        }
        else
        {
            _playerResult.text = "Loser";
            _enemyResult.text = "Winner";
            GameOverText();
            ClearAnim.Play("GameOver");
        }
    }

    public void ClearText()
    {
        _finishTextOne.text = "C";
        _finishTextTwo.text = "ongratulation";
    }
    public void GameOverText()
    {
        _finishTextOne.text = "Game";
        _finishTextTwo.text = "Over…";
    }
    public void DrawText()
    {
        _finishTextOne.text = "Draw";
        _finishTextTwo.enabled = false;
    }
}
