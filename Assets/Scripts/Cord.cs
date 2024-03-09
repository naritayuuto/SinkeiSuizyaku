using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Cord : MonoBehaviour
{
    [SerializeField]
    Sprite _image = null;
    [SerializeField]
    Sprite _defoImage = null;
    [Tooltip("���g�̉摜")]
    Image _objImage = null;
    [SerializeField]
    Animator _anim = null;
    [Tooltip("�߂����Ă�����True")]
    public bool _open = false;
    [Tooltip("�y�A���������ꍇTrue")]
    bool _disappear = false;
    [Tooltip("�����ɂȂ�܂ł̎���")]
    float _fadeTime = 1f;

    [Tooltip("�J�[�h�̏��B�����A���̕����A�摜")]
    CordData cordData = null;

    // Start is called before the first frame update
    void Start()
    {
        _objImage = GetComponent<Image>();
        _objImage.sprite = _defoImage;
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
        _objImage.sprite = _objImage.sprite == _defoImage ? cordData._numImage : _defoImage;        
    }
    public void OpenAnim()
    {
        _anim.Play("OpenCord");
    }
    public void CloseAnim()
    {
        _anim.Play("CloseCord");
    }
    /// <summary>
    /// �y�A���������ꍇ�A�����ɂ��Ĕ�\���ɂ���
    /// </summary>
    public void Disappear()
    {
        _objImage.DOColor(Color.clear, _fadeTime).OnComplete(() => _disappear = true);
    }
    /// <summary>
    /// �J�[�h�̏��ǂݍ���
    /// </summary>
    /// <param name="_image"></param>
    public void CordDataSet(CordData data)
    {
        cordData = data;
    }
}
