using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 画像のフェード管理
/// </summary>
public class RawImageFadeController : MonoBehaviour
{
    private RawImage m_image;
    private State m_fadeState;
    private float m_fadeTimeSec;
    private float m_currentTimeSec;
    private Color m_targetColor;

    public enum State
    {
        FadedIn,        // フェードイン済み（デフォルト状態）
        FadedOut,       // フェードアウト済み
        FadingIn,       // フェードイン中
        FadingOut,      // フェードアウト中
    }

    // 初期化
    public void Init(RawImage image)
    {
        m_image = image;
        m_fadeState = State.FadedIn;
        m_fadeTimeSec = 0;
        m_currentTimeSec = 0;
        m_targetColor = Color.white;
    }

    // 終了
    public void Term()
    {
        if (m_image != null)
        {
            m_image.gameObject.SetActive(false);
        }
        m_image = null;
    }

    void Update()
    {
        if (m_image == null)
        {
            return;
        }

        if (!IsFading())
        {
            return;
        }

        // フェード処理
        m_currentTimeSec += Time.deltaTime;
        if (m_currentTimeSec >= m_fadeTimeSec)
        {
            // フェードの終了処理
            if (m_fadeState == State.FadingOut)
            {
                m_image.color = m_targetColor;
                m_fadeState = State.FadedOut;
            }
            else
            {
                m_image.gameObject.SetActive(false);
                m_fadeState = State.FadedIn;
            }
        }
        else
        {
            // フェード色の更新処理
            Color color = m_targetColor;
            float t = Mathf.InverseLerp(0.0f, m_fadeTimeSec, m_currentTimeSec);
            if (m_fadeState == State.FadingOut)
            {
                color.a = Mathf.Lerp(0.0f, color.a, t);
            }
            else
            {
                color.a = Mathf.Lerp(color.a, 0.0f, t);
            }
            m_image.color = color;
        }
    }

    // ブラックアウト
    public void BlackOut(float fadeTimeSec, bool ignoreRedundant = false)
    {
        FadeOut(Color.black, fadeTimeSec);
    }

    // ホワイトアウト
    public void WhiteOut(float fadeTimeSec, bool ignoreRedundant = false)
    {
        FadeOut(Color.white, fadeTimeSec);
    }

    // 任意の色でフェードアウト
    public void FadeOut(Color targetColor, float fadeTimeSec, bool ignoreRedundant = false)
    {
        if (ignoreRedundant)
        {
            if (m_fadeState == State.FadingOut)
            {
                if (targetColor == m_targetColor)
                {
                    // 同じ色にフェードアウト中なら無視
                    return;
                }
            }
            if (m_fadeState == State.FadedOut)
            {
                if (targetColor == m_targetColor)
                {
                    // 同じ色にフェードアウト済みなら無視
                    return;
                }
                else
                {
                    // 別の色にフェードアウト済みならすぐに変更
                    fadeTimeSec = 0;
                }
            }
        }

        // フェードアウト開始
        m_currentTimeSec = 0;
        m_targetColor = targetColor;
        if (fadeTimeSec == 0)
        {
            m_fadeState = State.FadedOut;
            m_fadeTimeSec = 0;
        }
        else
        {
            m_fadeState = State.FadingOut;
            m_fadeTimeSec = fadeTimeSec;
            targetColor.a = 0.0f;
        }
        m_image.color = targetColor;
        m_image.gameObject.SetActive(true);
    }

    // フェードイン
    public void FadeIn(float fadeTimeSec, bool ignoreRedundant = false)
    {
        if (ignoreRedundant)
        {
            if (m_fadeState == State.FadedIn)
            {
                // フェードイン済みなら何もしない
                return;
            }
            if (m_fadeState == State.FadingIn)
            {
                // フェードイン中ならすぐに変更
                m_image.gameObject.SetActive(false);
                return;
            }
        }

        // フェードイン開始
        m_fadeState = State.FadingIn;
        m_fadeTimeSec = fadeTimeSec;
        m_currentTimeSec = 0;
        m_image.gameObject.SetActive(true);
    }


    //画面がフェードアウト済みならtrueを返す
    public State GetState()
    {
        return m_fadeState;
    }

    //画面がフェードイン／フェードアウト処理中ならtrueを返す
    public bool IsFading()
    {
        return (m_fadeState == State.FadingIn || m_fadeState == State.FadingOut);
    }

    //現在の画面フェード色を返す
    public Color GetTargetColor()
    {
        return m_targetColor;
    }

}
