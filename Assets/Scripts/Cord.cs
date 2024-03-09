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
    [Tooltip("自身の画像")]
    Image _objImage = null;
    [SerializeField]
    Animator _anim = null;
    [Tooltip("めくっていたらTrue")]
    public bool _open = false;
    [Tooltip("ペアが揃った場合True")]
    bool _disappear = false;
    [Tooltip("透明になるまでの時間")]
    float _fadeTime = 1f;

    [Tooltip("カードの情報。数字、その柄か、画像")]
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
    /// アニメーションイベント
    /// </summary>
    public void ReturnCord()
    {
        //もしカードの絵がデフォルトの物だったら数字が書いてある絵に切り替え
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
    /// ペアが揃った場合、透明にして非表示にする
    /// </summary>
    public void Disappear()
    {
        _objImage.DOColor(Color.clear, _fadeTime).OnComplete(() => _disappear = true);
    }
    /// <summary>
    /// カードの情報読み込み
    /// </summary>
    /// <param name="_image"></param>
    public void CordDataSet(CordData data)
    {
        cordData = data;
    }
}
