using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TitleAnimation : MonoBehaviour
{
    [SerializeField, Header("めくる時間")]
    float returnTime = 1f;
    [SerializeField]
    DiamondCordNum diamondCordNum = null;
    [SerializeField]
    SpadeCordNum spadeCordNum = null;
    [SerializeField]
    HeartCordNum heartCordNum = null;
    [SerializeField]
    CloverCordNum cloverCordNum = null;
    [SerializeField, Header("縦軸の大きさ")]
    int _rows = 4;
    [SerializeField, Header("横軸の大きさ")]
    int _columns = 5;
    [Tooltip("生成回数のカウント")]
    int _count = 0;
    [Tooltip("各柄のカード枚数の上限")]
    int _cordListMax = 10;
    [Tooltip("カードの二列目以降の条件式計算に使う")]
    const int _two = 2;
    [Tooltip("カードの三列目以降の条件式計算に使う")]
    const int _three = 3;
    [SerializeField]
    GridLayoutGroup _gridLayoutGroup = null;
    [SerializeField, Header("生成するカードのprefab")]
    Cord _cordPrefab = null;
    [Tooltip("シーン上に存在するカード")]
    Cord[] _cords = null;
    [Tooltip("生成するカードの情報")]
    CordData[] cordDatas = null;

    private void Awake()
    {
        CordDataSet();
        CordGenerate();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(OpenCord());
    }
    IEnumerator OpenCord()
    {
        foreach(var cord in _cords)
        {
            cord.OpenAnim();
            yield return new WaitForSeconds(returnTime);
        }
        StartCoroutine(CloseCord());
        yield break;
    }
    IEnumerator CloseCord()
    {
        foreach (var cord in _cords)
        {
            cord.CloseAnim();
            yield return new WaitForSeconds(returnTime);
        }
        StartCoroutine(OpenCord());
        yield break;
    }

    void CordDataSet()
    {
        int cordMax = _rows * _columns;
        cordDatas = new CordData[cordMax];
        for (int i = 0; i < cordDatas.Length; i++)
        {
            if (i < _cordListMax)
            {
                cordDatas[i] = diamondCordNum._cordData[i];
            }
            else if (_cordListMax <= i && i < _cordListMax * _two)
            {
                cordDatas[i] = spadeCordNum._cordData[i - _cordListMax];
            }
            else if(_cordListMax * _two <= i && i < _cordListMax * _three)
            {
                cordDatas[i] = cloverCordNum._cordData[i - _cordListMax * _two];
            }
            else
            {
                cordDatas[i] = heartCordNum._cordData[i - _cordListMax * _three];
            }
        }
    }
    void CordGenerate()
    {
        _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        _gridLayoutGroup.constraintCount = _columns;

        int cordMax = _rows * _columns;
        _cords = null;
        _cords = new Cord[cordMax];

        for (int i = 0; i < cordMax; i++)
        {
            var cord = Instantiate(_cordPrefab, _gridLayoutGroup.transform);
            _cords[i] = cord;
            _cords[i].CordDataSet(cordDatas[_count]);
            _count++;
        }
    }
}
