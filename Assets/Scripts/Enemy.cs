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
    [SerializeField, Header("�f�o�b�O�m�F�̂��߁A�Z�b�g���Ȃ�����"),Tooltip("�߂���J�[�h")]
    List<Cord> _cords = new List<Cord>();
    [Tooltip("1���߂�������̑҂�����")]
    int _coolTime = 1;
    [Tooltip("�v�f����Ԗڂ������l")]
    const int _zero = 0;
    [Tooltip("�v�f����Ԗڂ������l")]
    const int _one = 1;
    [Tooltip("�y�A�ƂȂ�J�[�h�̍ő�l")]
    const int _maxPairNum = 2;
    // Start is called before the first frame update
    void Start()
    {
        if(_cordGenerater == null)
        {
            Debug.LogError($"CordGenerater��{gameObject.name}��Enemy�ɃZ�b�g���Ă�������");
        }
        if (_cordJudge == null)
        {
            Debug.LogError($"CordJudge��{gameObject.name}��Enemy�ɃZ�b�g���Ă�������");
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
        //�J���J�[�h���擾���J��
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
