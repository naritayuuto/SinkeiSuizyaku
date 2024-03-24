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
    [SerializeField, Header("デバッグ確認のため、セットしないこと"),Tooltip("めくるカード")]
    List<Cord> _cords = new List<Cord>();
    [Tooltip("1枚めくった後の待ち時間")]
    int _coolTime = 1;
    [Tooltip("要素数零番目を示す値")]
    const int _zero = 0;
    [Tooltip("要素数一番目を示す値")]
    const int _one = 1;
    [Tooltip("ペアとなるカードの最大値")]
    const int _maxPairNum = 2;
    // Start is called before the first frame update
    void Start()
    {
        if(_cordGenerater == null)
        {
            Debug.LogError($"CordGeneraterを{gameObject.name}のEnemyにセットしてください");
        }
        if (_cordJudge == null)
        {
            Debug.LogError($"CordJudgeを{gameObject.name}のEnemyにセットしてください");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnemyTurnStart()
    {
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(_coolTime);
        //開くカードを取得し開く
        for (int i = 0; i < _cords.Count; i++)
        {
            _cords.Add(_cordGenerater.ReturnOpenCord());
            if (_cords.Count == _maxPairNum && _cords[_zero].CordData._numImage == _cords[_one].CordData._numImage)
            {
                _cords.Remove(_cords[_one]);
                i--;
            }
            else
            {
                _cordJudge.CordOpen(_cords[i]);
                yield return new WaitForSeconds(_coolTime);
            }
        }
        yield return new WaitForSeconds(_coolTime);
        yield break;
    }
}
