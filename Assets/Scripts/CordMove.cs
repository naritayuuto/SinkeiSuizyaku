using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CordMove : MonoBehaviour
{
    [SerializeField, Header("�v���C���[���������J�[�h�̒u����̍��W")]
    Vector2 _peaCordPos;
    
    RectTransform _rectTransform;
    [Tooltip("���g�̍ŏ��̍��W")]
    Vector2 _myStartPos;
    [Tooltip("�J�[�h�u����Ǝ��g�̌��ݒn�Ƃ̋���")]
    float _dis = 0;
    [SerializeField, Header("�����ňړ����鎞�̑��x")]
    float _moveSpeed = 1;
    [SerializeField, Tooltip("�J�[�h�̍��W�ƃJ�[�h�u����̊Ԃ̋���")]
    bool _move = false;
    // Start is called before the first frame update
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        //_myStartPos = GetComponent<RectTransform>().position;
        //_dis = Vector2.Distance(_myPos, _peaCordPos);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = _rectTransform.position;
     pos.x = 5;
        pos.y = 5;
        _rectTransform.position = pos;
    }

    public void Move()
    {
        
    }
}
