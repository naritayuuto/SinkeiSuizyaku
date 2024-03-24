using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // シーン遷移を行うために追加している
using DG.Tweening;

public class SceneLoader : MonoBehaviour
{
    enum SceneNum
    {
        Title,
        Main
    }

    [SerializeField, Header("フェードにかかる時間"), Tooltip("フェードにかかる時間")]
    float _fadeSpeed = 1f;
    [SerializeField]
    Image _fadePanel = null;
    [SerializeField,Header("ロードしたいシーン")]
    SceneNum _sceneNum = SceneNum.Title;

    public void LoadScene()
    {
        if (_fadePanel)
        {
            _fadePanel.DOColor(Color.black, _fadeSpeed).OnComplete(() => SceneManager.LoadScene((int)_sceneNum));
        }
        else
        {
            SceneManager.LoadScene((int)_sceneNum);
        }
    }
}
