using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager _instance;
    [SerializeField, Header("�^�C�}�[�ɂ���e�L�X�g")]
    TextMeshProUGUI _timerText = null;
    [SerializeField, Header("�N�̎�Ԃ���ʓ��ŕ\������e�L�X�g")]
    TextMeshProUGUI _playTurnText = null;
    [SerializeField, Header("�N�̎�Ԃ��A�j���[�V�����ŕ\������e�L�X�g")]
    TextMeshProUGUI _animPlayTurnText = null;
    [SerializeField, Header("�y�A�����J�E���g����e�L�X�g")]
    TextMeshProUGUI _playerPairText = null;
    [SerializeField, Header("�y�A�����J�E���g����e�L�X�g")]
    TextMeshProUGUI _enemyPairText = null;
    [SerializeField, Header("���ʉ�ʂɕ\������MSG�e�L�X�g")]
    TextMeshProUGUI _finishTextOne = null;
    [SerializeField, Header("���ʉ�ʂɕ\������MSG�e�L�X�g")]
    TextMeshProUGUI _finishTextTwo = null;
    [SerializeField, Header("���ʉ�ʂɕ\�����鏟���̌���")]
    TextMeshProUGUI _playerResult = null;
    [SerializeField, Header("���ʉ�ʂɕ\�����鏟���̌���")]
    TextMeshProUGUI _enemyResult = null;
    [SerializeField, Header("���ʉ�ʁA���_")]
    TextMeshProUGUI _playerPairResult = null;
    [SerializeField, Header("���ʉ�ʁA���_")]
    TextMeshProUGUI _enemyPairResult = null;
    [SerializeField, Header("���ʉ��")]
    TextMeshProUGUI _TimerResult = null;
    [Tooltip("�^�[�����؂�ւ�������ɕ\������")]
    string _playerTurnMSG = "�����̎��";
    [Tooltip("�^�[�����؂�ւ�������ɕ\������")]
    string _enemyTurnMSG = "����̎��";
    [Tooltip("�v���C���[�̃y�A��")]
    int _playerPair = 0;
    [Tooltip("�G�̃y�A��")]
    int _enemyPair = 0;
    [Tooltip("��")]
    float _minutes = 0;
    [SerializeField,Header("�f�o�b�O�p"),Tooltip("�b")]
    float _seconds = 0;
    [Tooltip("�A�j���[�V�����̒l")]
    const int _animINTNum = 0;
    [Tooltip("�A�j���[�V�����̒l")]
    const float _animFLOATNum = 0.0f;
    [Tooltip("�b���̍ő�l")]
    const float _secondsMax = 60f;
    [SerializeField, Header("_animPlayTurnText�̃A�j���[�V����")]
    Animator _textAnim = null;
    [SerializeField, Header("�I�����̃A�j���[�V����")]
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
        //�l���w�肵�Ȃ��Ɠ��ڂ̍Đ����o���Ȃ������׈ȉ��̏����ɂ��Ă��܂�
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
        _finishTextTwo.text = "Over�c";
    }
    public void DrawText()
    {
        _finishTextOne.text = "Draw";
        _finishTextTwo.enabled = false;
    }
}
