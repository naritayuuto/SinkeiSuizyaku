using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CordGenerater : MonoBehaviour
{
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
    [Tooltip("配列の最初の要素数を宣言するために使用")]
    const int _zero = 0;
    [Tooltip("Random.Rangeをintで使用するため、要素数+1の値を作り出すために使用")]
    const int _one = 1;
    [Tooltip("数値を半分にするため使用")]
    const int _two = 2;
    [SerializeField]
    GridLayoutGroup _gridLayoutGroup = null;
    [SerializeField, Header("生成するカードのprefab")]
    Cord _cordPrefab = null;
    [Tooltip("シーン上に存在するカード")]
    Cord[] _cords = null;
    [Tooltip("生成するカードの情報")]
    CordData[] cordDatas = null;

    public Cord[] Cords { get => _cords; set => _cords = value; }

    // Start is called before the first frame update
    void Start()
    {
        CordDataShuffle();
        CordGenerate();
    }
    /// <summary>
    /// 現在、ダイヤモンドとスペードの二種類しか含まれていません
    /// </summary>
    void CordDataShuffle()
    {
        int cordMax = _rows * _columns;
        _cordListMax = cordMax / _two;
        cordDatas = new CordData[cordMax];
        for (int i = 0; i < cordDatas.Length; i++)
        {
            cordDatas[i] = i < _cordListMax ? diamondCordNum._cordData[i] : spadeCordNum._cordData[i - _cordListMax];
        }
        for (int num = 0; num < cordMax; num++)
        {
            int randomNum = Random.Range(num, cordMax);
            //データ入れ替え
            CordData cordData = cordDatas[num];
            cordDatas[num] = cordDatas[randomNum];
            cordDatas[randomNum] = cordData;
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
