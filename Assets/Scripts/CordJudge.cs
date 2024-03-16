using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CordJudge : MonoBehaviour,IPointerClickHandler
{
    [SerializeField,Header("�f�o�b�O�m�F�̂��߁A�Z�b�g���Ȃ�����")]
    List<Cord> _cords = new List<Cord>();
    [SerializeField]
    CordGenerater cordGenerater = null;
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
    // Start is called before the first frame update
    void Start()
    {
        if(cordGenerater == null)
        {
            Debug.LogError($"cordGenerater��{gameObject.name}��CordJudge�ɃZ�b�g���Ă�������");
        }
        _panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_judge == false)
        {
            GameObject obj = eventData.pointerCurrentRaycast.gameObject;
            if (obj.TryGetComponent(out Cord cord))
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
            else
            {
                return;
            }
        }
    }
    IEnumerator Judge()
    {
        _judge = true;
        yield return new WaitForSeconds(_one);
        if (_cords[_zero].CordData._num == _cords[_one].CordData._num)
        {
            foreach (var cord in _cords)
            {
                cord.DisappearCord();
            }
        }
        else
        {
            foreach (var cord in _cords)
            {
                cord.CloseAnim();
            }
        }
        _cords = new List<Cord>();
        yield return new WaitForSeconds(_one);
        _judge = false;
        ClearJudge();
        yield break;
    }

    void ClearJudge()
    {
        bool clear = true;
        foreach(var cord in cordGenerater.Cords)
        {
            if(cord.Disappear == false)
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
