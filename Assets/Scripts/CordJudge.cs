using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class CordJudge : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    SceneState _sceneState = null;
    [SerializeField]
    CordGenerater _cordGenerater = null;
    [SerializeField, Header("デバッグ確認のため、セットしないこと")]
    List<Cord> _cords = new List<Cord>();
    [Tooltip("消えていないカードのリスト")]
    List<Cord> _notDSPCords = new List<Cord>();
    [Tooltip("めくられたカードのリスト")]
    List<Cord> _openCords = new List<Cord>();
    [SerializeField, Header("clear確認用のパネル")]
    GameObject _panel = null;
    [Tooltip("要素数零番目を示す値")]
    const int _zero = 0;
    [Tooltip("要素数一番目を示す値")]
    const int _one = 1;
    [Tooltip("ペアとなるカードの最大値")]
    const int _maxPairNum = 2;
    [Tooltip("ペアかどうか判定中")]
    bool _judge = false;
    [Tooltip("ペアが揃った場合True")]
    bool _pair = false;
    [Tooltip("カードが全てめくられたらTrue")]
    bool _clear = true;

    public List<Cord> OpenCords { get => _openCords; }

    // Start is called before the first frame update
    void Start()
    {
        _notDSPCords = _cordGenerater.Cords.ToList();
        if (_cordGenerater == null)
        {
            Debug.LogError($"CordGeneraterを{gameObject.name}のCordJudgeにセットしてください");
        }
        if (_sceneState == null)
        {
            Debug.LogError($"SceneStateを{gameObject.name}のCordJudgeにセットしてください");
        }
        _panel.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_judge == false && _sceneState.TurnState == TurnState.player1 && _sceneState.StageState == StageState.erabu)
        {
            GameObject obj = eventData.pointerCurrentRaycast.gameObject;
            if (obj.TryGetComponent(out Cord cord))
            {
                CordOpen(cord);
            }
            else
            {
                return;
            }
        }
    }

    /// <summary>
    /// カードをめくる
    /// </summary>
    /// <param name="cord"></param>
    public void CordOpen(Cord cord)
    {
        if (_cords.Count < _maxPairNum)
        {
            cord.OpenAnim();
            _cords.Add(cord);
            if (_cords.Count == _maxPairNum)
            {
                if (_cords[_zero].CordData._numImage == _cords[_one].CordData._numImage)
                {
                    _cords.Remove(_cords[_one]);
                }
                else
                {
                    StartCoroutine(Judge());
                }
            }
        }
    }
    IEnumerator Judge()
    {
        //確認開始
        _judge = true;
        _sceneState.StageState = StageState.pairJudge;
        yield return new WaitForSeconds(_one);
        if (_cords[_zero].CordData._num == _cords[_one].CordData._num)
        {
            foreach (var cord in _cords)
            {
                cord.DisappearCord();
                _notDSPCords.Remove(cord);
                if(_openCords.Contains(cord))
                {
                    _openCords.Remove(cord);
                }
            }
            _pair = true;
        }
        else
        {
            foreach (var cord in _cords)
            {
                cord.CloseAnim();
                //開いたカードの数字を記録
                if (!_openCords.Contains(cord))
                {
                    _openCords.Add(cord);
                }
            }
            _pair = false;
        }
        _cords = new List<Cord>();
        yield return new WaitForSeconds(_one);
        _judge = false;
        ClearJudge();
        TurnJudge();
        yield break;
    }

    /// <summary>
    /// ペアが揃っていたら同じ人に、そうでなければ次の人へ手番を移行する。
    /// </summary>
    public void TurnJudge()
    {
        if (!_clear)
        {
            _sceneState.StageState = StageState.ennsyutu;
            if (!_pair)
            {
                _sceneState.TurnChange();
            }
            else
            {//揃っていたら同じ人がめくる
                _sceneState.TurnState = _sceneState.TurnState;
            }
        }
    }

    void ClearJudge()
    {
        _clear = true;
        foreach (var cord in _cordGenerater.Cords)
        {
            if (cord.Disappear == false)
            {
                _clear = false;
                break;
            }
        }
        if (_clear)
        {
            _panel.SetActive(true);
        }
    }

    /// <summary>
    /// ペアが揃っていないカードを渡す。
    /// </summary>
    /// <returns></returns>
    public Cord ReturnOpenCordJudge()
    {
           int randomNum = Random.Range(_zero, _notDSPCords.Count);
            return _notDSPCords[randomNum];
    }


    public List<Cord> PairCord()
    {
        bool pair = false;
        List<Cord> cords = new List<Cord>();
        for(int i = 0; i < _openCords.Count; i++)
        {
            if (!pair)
            {
                for (int count = 0; count < _openCords.Count; count++)
                {
                    //自分以外で同じ数字のカードを探す
                    if (_openCords[i] != _openCords[count] && _openCords[i].CordData._num == _openCords[count].CordData._num)
                    {
                        cords.Add(_openCords[i]);
                        cords.Add(_openCords[count]);
                        pair = true;
                        break;
                    }
                }
            }
            else
            {
                break;
            }
        }
        return cords;
    }
}
