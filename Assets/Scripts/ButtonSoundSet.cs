using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonSoundSet : MonoBehaviour
{
    [SerializeField,Header("ボタンを押す音を付ける")]
    Button[] _buttons = null;
    [SerializeField, Header("SelectButtonの音を付ける")]
    Button[] _selectButtons = null; 
    // Start is called before the first frame update
    void Start()
    {
        if(_buttons != null)
        {
            foreach(var button in _buttons)
            {
                button.onClick.AddListener(() => SoundManager.Instance.SEPlay(SEType.Button));
            }
        }
        if(_selectButtons != null)
        {
            foreach (var button in _selectButtons)
            {
                button.onClick.AddListener(() => SoundManager.Instance.SEPlay(SEType.SelectButton));
            }
        }
    }
}
