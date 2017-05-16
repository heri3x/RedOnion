using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Test/MainSceneController")]
public class MainSceneController : MonoBehaviour
{
    public Camera MainCamera;
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

    public SpriteRenderer CharaMisaki;
    public RawImage CharaKohaku;
    public RawImage CharaYuko;

    public void TestSetCharaColorValue(float v)
    {
        Color color;

        color = CharaMisaki.color;
        CharaMisaki.color = new Color(v, color.g, color.b, color.a);

        color = CharaKohaku.color;
        CharaKohaku.color = new Color(1.0f - v, color.g, 1.0f - v, color.a);

        color = CharaYuko.color;
        CharaYuko.color = new Color(v, v, v, color.a);
    }

    public void TestTogglePostEffectBloom(bool v)
    {
        var behavior = MainCamera.GetComponent<UnityEngine.PostProcessing.PostProcessingBehaviour>();
        if (behavior != null)
        {
            var prof = behavior.profile;
            if (prof != null)
            {
                prof.bloom.enabled = v;
            }
        }
    }

    public void TestTogglePostEffectVignette(bool v)
    {
        var behavior = MainCamera.GetComponent<UnityEngine.PostProcessing.PostProcessingBehaviour>();
        if (behavior != null)
        {
            var prof = behavior.profile;
            if (prof != null)
            {
                prof.vignette.enabled = v;
            }
        }
    }

    public void TestEffectSepia(bool v)
    {
        var postEffect = MainCamera.GetComponent<PostEffect>();
        if (postEffect != null)
        {
            postEffect.enabled = v;
        }
    }

    public void TestShakeScreen()
    {
        GameManager.Instance.ObjectShakeController.Shake(MainCamera.gameObject);
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
        GameManager.Instance.SceneInit(MainCamera, ScreenFadeImage);
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
