using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CordMove : MonoBehaviour
{
    [SerializeField, Header("プレイヤーが揃えたカードの置き場の座標")]
    Vector2 _peaCordPos;
    
    RectTransform _rectTransform;
    [Tooltip("自身の最初の座標")]
    Vector2 _myStartPos;
    [Tooltip("カード置き場と自身の現在地との距離")]
    float _dis = 0;
    [SerializeField, Header("自動で移動する時の速度")]
    float _moveSpeed = 1;
    [SerializeField, Tooltip("カードの座標とカード置き場の間の距離")]
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
