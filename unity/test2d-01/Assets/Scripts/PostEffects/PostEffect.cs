//
//【Unityシェーダ入門】画面をセピア色にするポストエフェクトを作る
//http://nn-hokuson.hatenablog.com/entry/2017/05/09/202141
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostEffect : MonoBehaviour
{
    public Material PostEffectMaterial;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, PostEffectMaterial);
    }
}
