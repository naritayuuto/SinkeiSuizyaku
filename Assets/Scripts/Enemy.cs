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
    [Tooltip("Enemy�̃R���[�`��")]
    IEnumerator _enemyCol = null;
    [SerializeField, Header("�f�o�b�O�m�F�̂��߁A�Z�b�g���Ȃ�����"), Tooltip("�߂���J�[�h")]
    List<Cord> _cords = new List<Cord>();
    [Tooltip("1���߂�������̑҂�����")]
    int _coolTime = 1;
    [Tooltip("�v�f����Ԗڂ������l")]
    const int _zero = 0;
    [Tooltip("�v�f����Ԗڂ������l")]
    const int _one = 1;
    [Tooltip("�y�A�ƂȂ�J�[�h�̍ő�l")]
    const int _maxPairNum = 2;
    [Tooltip("�y�A�����擾�����ꍇTrue")]
    bool _pair = false;
    // Start is called before the first frame update
    void Start()
    {
        if (_cordGenerater == null)
        {
            Debug.LogError($"CordGenerater��{gameObject.name}��Enemy�ɃZ�b�g���Ă�������");
        }
        if (_cordJudge == null)
        {
            Debug.LogError($"CordJudge��{gameObject.name}��Enemy�ɃZ�b�g���Ă�������");
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
        //�J���J�[�h���擾���J��
        for (int i = 0; i < _maxPairNum; i++)
        {
            if(_pair)
            {
                _pair = false;
                break;
            }
            _cords.Add(_cordJudge.ReturnOpenCordJudge());
            //�����J�[�h���߂��낤�Ƃ��Ă������蒼���B
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
                    //�y�A�ƂȂ�J�[�h���Ȃ������ꍇ
                    if (paircord.Count == 0)
                    {
                        _cordJudge.CordOpen(_cords[i]);
                        yield return new WaitForSeconds(_coolTime);
                    }
                    else//�y�A�ƂȂ�J�[�h�������ꍇ
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
