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
    [SerializeField, Header("�c���̑傫��")]
    int _rows = 4;
    [SerializeField, Header("�����̑傫��")]
    int _columns = 5;
    [Tooltip("�����񐔂̃J�E���g")]
    int _count = 0;
    [Tooltip("�e���̃J�[�h�����̏��")]
    int _cordListMax = 10;
    [Tooltip("���l�𔼕��ɂ��邽�ߎg�p")]
    const int _two = 2;
    [SerializeField]
    GridLayoutGroup _gridLayoutGroup = null;
    [SerializeField, Header("��������J�[�h��prefab")]
    Cord _cordPrefab = null;
    [Tooltip("�V�[����ɑ��݂���J�[�h")]
    Cord[] _cords = null;
    [Tooltip("��������J�[�h�̏��")]
    CordData[] cordDatas = null;

    public Cord[] Cords { get => _cords; set => _cords = value; }

    // Start is called before the first frame update
    void Start()
    {
        CordDataShuffle();
        CordGenerate();
    }

    // Update is called once per frame
    void Update()
    {

    }
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
            //�f�[�^����ւ�
            CordData cordData = cordDatas[num];
            cordDatas[num] = cordDatas[randomNum];
            cordDatas[randomNum] = cordData;
        }
    }
    //void CordGenerate()
    //{
    //    _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
    //    _gridLayoutGroup.constraintCount = _columns;

    //    _cords = null;
    //    _cords = new Cord[_rows, _columns];

    //    for (var r = 0; r < _rows; r++)
    //    {
    //        for (var c = 0; c < _columns; c++)
    //        {
    //            var cord = Instantiate(_cordPrefab, _gridLayoutGroup.transform);
    //            _cords[r, c] = cord;
    //            _cords[r, c].CordDataSet(cordDatas[_count]);
    //            _count++;
    //        }
    //    }
    //}
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

    ///// <summary>
    ///// �S�ẴJ�[�h��\��
    ///// </summary>
    //public void Opens()
    //{
    //    for (var r = 0; r < _rows; r++)
    //    {
    //        for (var c = 0; c < _columns; c++)
    //        {
    //            _cords[r, c].OpenAnim();
    //        }
    //    }
    //}
}
