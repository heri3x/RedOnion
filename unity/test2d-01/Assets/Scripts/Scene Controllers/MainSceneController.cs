using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Test/MainSceneController")]
public class MainSceneController : MonoBehaviour
{
    public int TestValue1;

    [Space(30)]

    public int TestValue2;



    [ContextMenu("MainScene Do Something")]
    void DoSomething()
    {
        Debug.Log("Perform operation");
    }


    private void Awake()
    {
        // もしシングルトンインスタンスがなければ生成
        SingletonUtility<GameManager>.EnsureInstance();
    }
}
