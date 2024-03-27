using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class Pause : MonoBehaviour
{
    [SerializeField, Header("ポーズ中と表示するテキスト")]
    TextMeshProUGUI _textColor = null;
    [SerializeField, Header("ポーズ機能を持たせるボタン")]
    Button _pauseButton = null;
    [SerializeField, Header("ポーズを解除する機能を持たせるボタン")]
    Button _notPauseButton = null;
    Enemy enemy = null;
    Tween _loopColor = null;
    [Tooltip("色が変わりきる時間")]
    float _fadeSpeed = 1f;
    [Tooltip("無限ループ用の変数")]
    int _minusOne = -1;
    private void Start()
    {
        enemy = FindObjectOfType<Enemy>();
        _pauseButton.onClick.AddListener(PauseStart);
        _notPauseButton.onClick.AddListener(PauseFinish);
    }
    public void PauseStart()
    {
        GameManager.Instance.Pause = true;
        if (_loopColor == null)
        {
            _loopColor = _textColor.DOFade(0.0f, _fadeSpeed).SetLoops(_minusOne, LoopType.Yoyo);
        }
        else
        {
            _loopColor.Play();
        }
        enemy.PauseEnemy();
    }
    public void PauseFinish()
    {
        GameManager.Instance.Pause = false;
        _loopColor.Pause();
        enemy.NotPauseEnemy();
    }
}
