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
    [Tooltip("�N�̎�Ԃ�")]
    TurnState _turnState = TurnState.None;
    [Tooltip("��ʓ��̏�")]
    StageState _stageState = StageState.None;
    [SerializeField]
    Enemy _enemy = null;
    [Header("�N�̎�Ԃ���m�点��A�j���[�V�����e�L�X�g")]
    TextMeshProUGUI _msgText = null;
    [Tooltip("�v���C���[1�̎�Ԃł��邱�Ƃ������l")]
    const int _zero = 0;
    [Tooltip("�v���C���[2�̎�Ԃł��邱�Ƃ������l")]
    const int _one = 1;
    [Tooltip("�����_���Ȏ�Ԃł��邱�Ƃ������l")]
    const int _two = 2;

    public TurnState TurnState
    { 
        get => _turnState;
        set
        {
            _turnState = value;
            _msgText.text = _turnState == TurnState.player1 ? "���Ȃ��̎�Ԃł�" : "����̎�Ԃł�";
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

    private void Awake()
    {
        switch (GameManager.Instance.PlayNum)
        {
            case 0:
                int num = Random.Range(0, 1);
                _turnState = (TurnState)num;
                break;
            case 1:
                _turnState = TurnState.player1;
                break;
            case 2:
                _turnState = TurnState.player2;
                break;
        }
        _stageState = StageState.ennsyutu;
    }

    /// <summary>
    /// �J�[�h���߂����悤�ɂ���A�A�j���[�V�����C�x���g�ōs���B
    /// </summary>
    public void TurnStart()
    {
        _stageState = StageState.erabu;
    }
    
    /// <summary>
    /// ��Ԃ����ւ���
    /// </summary>
    public void TurnChange()
    {
        _turnState = _turnState == TurnState.player1 ? TurnState.player2 : TurnState.player1;
    }

}
