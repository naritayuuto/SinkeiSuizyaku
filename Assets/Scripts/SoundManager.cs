using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SoundManager : MonoBehaviour
{
    public static SoundManager _instance;
    [SerializeField,Tooltip("SEのリスト")]
    SEList _SEList = null;
    [SerializeField,Tooltip("BGMのリスト")]
    BGMList _BGMList = null;
    [SerializeField, Tooltip("BGMのAudioSource")]
    AudioSource _BGMSouse;
    [SerializeField, Tooltip("SEのAudioSource")]
    AudioSource _SESouse;
    [SerializeField, Header("最初に流すBGMの種類")]
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
            Debug.LogError("BGMを見つけられませんでした"); 
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
            Debug.LogError("SEを見つけられませんでした"); 
            return; 
        }
        _SESouse.PlayOneShot(SE.Clip);
    }
    /// <summary>
    /// ループのオンオフ切り替え用
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
