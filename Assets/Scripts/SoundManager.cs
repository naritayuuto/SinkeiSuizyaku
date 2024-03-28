using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SoundManager : MonoBehaviour
{
    public static SoundManager _instance;
    [SerializeField,Tooltip("SE�̃��X�g")]
    SEList _SEList = null;
    [SerializeField,Tooltip("BGM�̃��X�g")]
    BGMList _BGMList = null;
    [SerializeField, Tooltip("BGM��AudioSource")]
    AudioSource _BGMSouse;
    [SerializeField, Tooltip("SE��AudioSource")]
    AudioSource _SESouse;
    [SerializeField, Header("�ŏ��ɗ���BGM�̎��")]
    BGMType _farstBGM;
    public static SoundManager Instance => _instance;
    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        BGMPlay(_farstBGM);
    }
    public void BGMPlay(BGMType type)
    {
        var BGM = _BGMList.BGMDatas.Where(b => b.BGMType == type).FirstOrDefault();
        if (BGM == null)
        { 
            Debug.LogError("BGM���������܂���ł���"); 
            return; 
        }
        _BGMSouse.clip = BGM.Clip;
        _BGMSouse.Play();
         BGMLoop(true);
    }
    public void SEPlay(SEType type)
    {
        var SE = _SEList.SEDatas.Where(s => s.SEType == type).FirstOrDefault();
        if (SE == null) 
        {
            Debug.LogError("SE���������܂���ł���"); 
            return; 
        }
        _SESouse.PlayOneShot(SE.Clip);
    }
    /// <summary>
    /// ���[�v�̃I���I�t�؂�ւ��p
    /// </summary>
    /// <param name="active"></param>
    public void BGMLoop(bool active)
    {
        if (_BGMSouse == null) 
        {
            return; 
        }
        _BGMSouse.loop = active;
    }

    public void StopBGM()
    {
        if (_BGMSouse == null)
        {
            return;
        }
        _BGMSouse.Stop();
    }
}
