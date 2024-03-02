using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cord : MonoBehaviour
{
    [SerializeField]
    Sprite _defoImage = null;

    Sprite _numImage = null;

    Image _objImage = null;

    [SerializeField]
    DiamondCordNum DiamondCordNum = null;
    [SerializeField]
    Animator _anim = null;

    RectTransform rectTransform = null;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        _objImage = GetComponent<Image>();
        _objImage.sprite = _defoImage;
        _numImage = DiamondCordNum._cordNumImages[0]._numImage;
    }

    // Update is called once per frame
    void Update()
    {
    }
    /// <summary>
    /// �A�j���[�V�����C�x���g
    /// </summary>
    public void ReturnCord()
    {
        //�����J�[�h�̊G���f�t�H���g�̕��������琔���������Ă���G�ɐ؂�ւ�
        _objImage.sprite = _objImage.sprite ? _numImage : _defoImage;
    }
    public void Anim()
    {
        _anim.Play("ReturnCord");
    }
    //public void Move()
    //{
    //    _anim.Play("Move");
    //}

    public void Move()
    {
        GetComponent<RectTransform>().anchoredPosition = new Vector2(100, 100);
    }
}
