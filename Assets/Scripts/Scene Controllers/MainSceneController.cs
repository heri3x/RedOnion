using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneController : MonoBehaviour
{
    public Camera MainCamera;
    public UnityEngine.UI.RawImage ScreenFadeImage;
    public GameObject Cube1;
    public GameObject Cube2;

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
    public SpriteRenderer CharaYuko;
    public RawImage CharaKohaku;

    public void TestSetCharaColorValue(float v)
    {
        Color color;

        color = CharaMisaki.color;
        CharaMisaki.color = new Color(v, color.g, color.b, color.a);

        color = CharaYuko.color;
        CharaYuko.color = new Color(1.0f - v, 1.0f - v, 1.0f - v, color.a);

        color = CharaKohaku.color;
        CharaKohaku.color = new Color(v, v, v, color.a);
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

    public MovieController BigBuckBunnyMovie;
    public MovieController UnityChanTurnMovie;

    public void TestPlayMovieBigBuckBunny()
    {
        BigBuckBunnyMovie.TogglePlay();
    }

    public void TestPlayMovieUnityChanTurn()
    {
        UnityChanTurnMovie.TogglePlay();
    }

    //-------------------------------------------------------------

    private void rotateObject(GameObject go)
    {
        // 回転ループ
        iTween.RotateTo(go, iTween.Hash(
          "y", 360.0f * 1000
        , "time", 5.0f * 1000
        , "easetype", "linear"
        , "looptype", "loop"
        , "islocal", false
        ));
    }

    //-------------------------------------------------------------

    private void Awake()
    {
        // GameManagerのシーン初期化処理を呼び出し
        GameManager.Instance.SceneInit(MainCamera, ScreenFadeImage);
    }

    private void Start()
    {
        rotateObject(Cube1);
        rotateObject(Cube2);
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
