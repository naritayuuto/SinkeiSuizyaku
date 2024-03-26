using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum TurnState
{
    None,
    player1,
    player2
}
public enum StageState
{
    None,
    ennsyutu,
    erabu,
    pairJudge
}
public class SceneState : MonoBehaviour
{
    [Tooltip("誰の手番か")]
    TurnState _turnState = TurnState.None;
    [Tooltip("画面内の状況")]
    StageState _stageState = StageState.None;
    [SerializeField,Header("敵の行動script")]
    Enemy _enemy = null;
    [SerializeField,Header("誰の手番かを知らせるアニメーションテキスト")]
    TextMeshProUGUI _msgText = null;
    [SerializeField, Header("AnimationText")]
    Animator _textAnim = null;
    [Tooltip("プレイヤー1の手番であることを示す値")]
    const int _zero = 0;
    [Tooltip("プレイヤー2の手番であることを示す値")]
    const int _one = 1;
    [Tooltip("ランダムな手番であることを示す値")]
    const int _two = 2;

    public TurnState TurnState
    { 
        get => _turnState;
        set
        {
            _turnState = value;
            PlayTurn();
        }
    }
    public StageState StageState 
    { 
        get => _stageState;
        set
        {
            _stageState = value;
            if(_stageState == StageState.erabu && _turnState == TurnState.player2)
            {
                _enemy.EnemyTurnStart();
            }
        } 
    }

    private void Start()
    {
        if (!_textAnim)
        {
            Debug.LogError($"{gameObject.name}のSceneStateにtextのアニメーションをセットしてください");
        }
        switch (GameManager.Instance.PlayNum)
        {
            case _zero:
                int num = Random.Range(_one, _two);
                _turnState = (TurnState)num;
                break;
            case _one:
                _turnState = TurnState.player1;
                break;
            case _two:
                _turnState = TurnState.player2;
                break;
        }
        PlayTurn();
        _stageState = StageState.ennsyutu;
    }

    /// <summary>
    /// カードをめくれるようにする、アニメーションイベントで行う。
    /// </summary>
    public void TurnStart()
    {
        _stageState = StageState.erabu;
    }
    
    /// <summary>
    /// 手番を入れ替える
    /// </summary>
    public void TurnChange()
    {
        _turnState = _turnState == TurnState.player1 ? TurnState.player2 : TurnState.player1;
        PlayTurn();
    }

    void PlayTurn()
    {
        _msgText.text = _turnState == TurnState.player1 ? "あなたの手番です" : "相手の手番です";
        _textAnim.Play("Text", 0, 0.0f);
    }
}
