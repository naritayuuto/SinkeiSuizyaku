using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    CordGenerater _cordGenerater = null;
    [SerializeField]
    CordJudge _cordJudge = null;
    [Tooltip("Enemyのコルーチン")]
    IEnumerator _enemyCol = null;
    [SerializeField, Header("デバッグ確認のため、セットしないこと"), Tooltip("めくるカード")]
    List<Cord> _cords = new List<Cord>();
    [Tooltip("1枚めくった後の待ち時間")]
    int _coolTime = 1;
    [Tooltip("要素数零番目を示す値")]
    const int _zero = 0;
    [Tooltip("要素数一番目を示す値")]
    const int _one = 1;
    [Tooltip("ペアとなるカードの最大値")]
    const int _maxPairNum = 2;
    [Tooltip("ペア情報を取得した場合True")]
    bool _pair = false;
    // Start is called before the first frame update
    void Start()
    {
        if (_cordGenerater == null)
        {
            Debug.LogError($"CordGeneraterを{gameObject.name}のEnemyにセットしてください");
        }
        if (_cordJudge == null)
        {
            Debug.LogError($"CordJudgeを{gameObject.name}のEnemyにセットしてください");
        }
    }
    public void EnemyTurnStart()
    {
        _enemyCol = EnemyTurn();
        StartCoroutine(_enemyCol);
    }

    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(_coolTime);
        _cords = new List<Cord>();
        //開くカードを取得し開く
        for (int i = 0; i < _maxPairNum; i++)
        {
            if(_pair)
            {
                _pair = false;
                break;
            }
            _cords.Add(_cordJudge.ReturnOpenCordJudge());
            //同じカードをめくろうとしていたらやり直す。
            if (_cords.Count == _maxPairNum && _cords[_zero].CordData._numImage == _cords[_one].CordData._numImage)
            {
                _cords.Remove(_cords[_one]);
                i--;
            }
            else
            {
                if (GameManager.Instance.EnemyPower == _zero)
                {
                    _cordJudge.CordOpen(_cords[i]);
                    yield return new WaitForSeconds(_coolTime);
                }
                else
                {
                    List<Cord> paircord = _cordJudge.PairCord();
                    //ペアとなるカードがなかった場合
                    if (paircord.Count == 0)
                    {
                        _cordJudge.CordOpen(_cords[i]);
                        yield return new WaitForSeconds(_coolTime);
                    }
                    else//ペアとなるカードがいた場合
                    {
                        _pair = true;
                        foreach(var cord in paircord)
                        {
                            _cordJudge.CordOpen(cord);
                            yield return new WaitForSeconds(_coolTime);
                        }
                    }
                }
            }
        }
        yield break;
    }

    public void PauseEnemy()
    {
        if(_enemyCol == null)
        {
            return;
        }
        StopCoroutine(_enemyCol);
    }

    public void NotPauseEnemy()
    {
        StartCoroutine(_enemyCol);
    }
}
