using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Test/MainSceneController")]
public class MainSceneController : MonoBehaviour
{
    public UnityEngine.UI.RawImage ScreenFadeImage;


    //-------------------------------------------------------------
    public void TestBlackOut()
    {
        GameManager.Instance.ScreenFadeController.BlackOut(0.5f);
    }

    public void TestWhiteOut()
    {
        GameManager.Instance.ScreenFadeController.WhiteOut(0.5f);
    }

    public void TestRedOut()
    {
        GameManager.Instance.ScreenFadeController.FadeOut(Color.red, 0.5f);
    }

    public void TestFadeIn()
    {
        GameManager.Instance.ScreenFadeController.FadeIn(0.5f);
    }
    //-------------------------------------------------------------


    [ContextMenu("MainScene Do Something")]
    void DoSomething()
    {
        Debug.Log("Perform operation");
    }


    private void Awake()
    {
        // GameManagerのシーン初期化処理を呼び出し
        GameManager.Instance.SceneInit(ScreenFadeImage);
    }

    private void OnDestroy()
    {
        if (SingletonUtility<GameManager>.IsInstanceExists())
        {
            // GameManagerのシーン終了処理を呼び出し
            GameManager.Instance.SceneTerm();
        }
    }
}
