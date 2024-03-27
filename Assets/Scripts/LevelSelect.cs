using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelSelect : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _text = null;
    const int _zero = 0;
    const int _one = 1;
    private void Start()
    {
        TextChange();
    }
    public void EnemyPowerNumPlus()
    {
        GameManager.Instance.EnemyPower = GameManager.Instance.EnemyPower < _one ? GameManager.Instance.EnemyPower + _one : _zero;
        TextChange();
    }
    public void EnemyPowerNumMinus()
    {
        GameManager.Instance.EnemyPower = _zero < GameManager.Instance.EnemyPower ? GameManager.Instance.EnemyPower - _one : _one;
        TextChange();
    }

    public void TextChange()
    {
        switch (GameManager.Instance.EnemyPower)
        {
            case _zero:
                _text.text = "‚©‚ñ‚½‚ñ";
                break;
            case _one:
                _text.text = "‚Þ‚¸‚©‚µ‚¢";
                break;
        }
    }
}
