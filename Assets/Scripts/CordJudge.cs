using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CordJudge : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    SceneState _sceneState = null;
    [SerializeField]
    CordGenerater _cordGenerater = null;
    [SerializeField, Header("�f�o�b�O�m�F�̂��߁A�Z�b�g���Ȃ�����")]
    List<Cord> _cords = new List<Cord>();
    [SerializeField, Header("clear�m�F�p�̃p�l��")]
    GameObject _panel = null;
    [Tooltip("�v�f����Ԗڂ������l")]
    const int _zero = 0;
    [Tooltip("�v�f����Ԗڂ������l")]
    const int _one = 1;
    [Tooltip("�y�A�ƂȂ�J�[�h�̍ő�l")]
    const int _maxPairNum = 2;
    [Tooltip("�y�A���ǂ������蒆")]
    bool _judge = false;
    [Tooltip("�y�A���������ꍇTrue")]
    bool _pair = false;
    // Start is called before the first frame update
    void Start()
    {
        if (_cordGenerater == null)
        {
            Debug.LogError($"CordGenerater��{gameObject.name}��CordJudge�ɃZ�b�g���Ă�������");
        }
        if (_sceneState == null)
        {
            Debug.LogError($"SceneState��{gameObject.name}��CordJudge�ɃZ�b�g���Ă�������");
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
    /// �J�[�h���߂���
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
        //�m�F�J�n
        _judge = true;
        _sceneState.StageState = StageState.pairJudge;
        yield return new WaitForSeconds(_one);
        if (_cords[_zero].CordData._num == _cords[_one].CordData._num)
        {
            foreach (var cord in _cords)
            {
                cord.DisappearCord();
            }
            _pair = true;
        }
        else
        {
            foreach (var cord in _cords)
            {
                cord.CloseAnim();
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
    /// �y�A�������Ă����瓯���l�ɁA�����łȂ���Ύ��̐l�֎�Ԃ��ڍs����B
    /// </summary>
    public void TurnJudge()
    {
        _sceneState.StageState = StageState.ennsyutu;
        if (!_pair)
        {
            _sceneState.TurnChange();
        }
        else
        {//�����Ă����瓯���l���߂���
            _sceneState.TurnState = _sceneState.TurnState;
        }
    }

    void ClearJudge()
    {
        bool clear = true;
        foreach (var cord in _cordGenerater.Cords)
        {
            if (cord.Disappear == false)
            {
                clear = false;
                break;
            }
        }
        if (clear)
        {
            _panel.SetActive(true);
        }
    }
}
