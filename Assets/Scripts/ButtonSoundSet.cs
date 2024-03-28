using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonSoundSet : MonoBehaviour
{
    [SerializeField,Header("�{�^������������t����")]
    Button[] _buttons = null;
    [SerializeField, Header("SelectButton�̉���t����")]
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
