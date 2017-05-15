using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneController : MonoBehaviour
{
    private void Awake()
    {
        // もしシングルトンインスタンスがなければ生成
        SingletonUtility<GameManager>.EnsureInstance();
    }
}
