using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TitleAnimation : MonoBehaviour
{
    [SerializeField, Header("�߂��鎞��")]
    float returnTime = 1f;
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
    [Tooltip("�J�[�h�̓��ڈȍ~�̏������v�Z�Ɏg��")]
    const int _two = 2;
    [Tooltip("�J�[�h�̎O��ڈȍ~�̏������v�Z�Ɏg��")]
    const int _three = 3;
    [SerializeField]
    GridLayoutGroup _gridLayoutGroup = null;
    [SerializeField, Header("��������J�[�h��prefab")]
    Cord _cordPrefab = null;
    [Tooltip("�V�[����ɑ��݂���J�[�h")]
    Cord[] _cords = null;
    [Tooltip("��������J�[�h�̏��")]
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
