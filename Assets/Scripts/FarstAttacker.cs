using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FarstAttacker : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _text = null;
    const int _zero = 0;
    const int _one = 1;
    const int _two = 2;
    public void FarstAttackerNumPlus()
    {
        GameManager.Instance.PlayNum = GameManager.Instance.PlayNum < _two ? GameManager.Instance.PlayNum + _one : _zero;
        TextChange();
    }
    public void FarstAttackerNumMinus()
    {
        GameManager.Instance.PlayNum = _zero < GameManager.Instance.PlayNum ? GameManager.Instance.PlayNum - _one : _two;
        TextChange();
    }

    public void TextChange()
    {
        switch(GameManager.Instance.PlayNum)
        {
            case _zero:
                _text.text = "ƒ‰ƒ“ƒ_ƒ€";
                break;
            case _one:
                _text.text = "1P";
                break;
            case _two:
                _text.text = "‘ŠŽè";
                break;
        }
    }
}
