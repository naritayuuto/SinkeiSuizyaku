using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // �V�[���J�ڂ��s�����߂ɒǉ����Ă���
using DG.Tweening;

public class SceneLoader : MonoBehaviour
{
    [SerializeField, Header("�t�F�[�h�ɂ����鎞��"), Tooltip("�t�F�[�h�ɂ����鎞��")]
    float _fadeSpeed = 1f;
    [SerializeField]
    Image _fadePanel = null;

    public void LoadScene(int sceneNum)
    {
        if (_fadePanel)
        {
            _fadePanel.DOColor(Color.black, _fadeSpeed).OnComplete(() => SceneManager.LoadScene(sceneNum));
        }
        else
        {
            SceneManager.LoadScene(sceneNum);
        }
    }
}
