using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ゲーム管理
/// </summary>
/// <remarks>
/// GameManagerのPrefabは、PrefabsフォルダではなくResourceフォルダに配置してください。
/// </remarks>
[Singleton("Game Manager", true)]
public class GameManager : MonoBehaviour
{
    // 
    // publicフィールド
    // 
    //-------------------------------------------------
    [Space(20)]
    //-------------------------------------------------
    public EnemyData[] LevelData;

    [Range(1, 20)]
    public int Level;

    [System.SerializableAttribute]
    public class ParamGroup
    {
        public int Param1;
        public int Param2;
        public int Param3;
    }

    public ParamGroup TestParamGroup;

    //-------------------------------------------------
    [Space(20)]
    //-------------------------------------------------
    public Camera MainCamera;
    public RawImageFadeController ScreenFadeController;
    public ObjectShakeController ObjectShakeController;

    //-------------------------------------------------


    public static GameManager Instance
    {
        get { return SingletonUtility<GameManager>.Instance; }
    }


    // シーン初期化処理
    public void SceneInit(Camera mainCamera, RawImage screenFadeImage)
    {
        Debug.Log("GameManager.SceneInit()");
        MainCamera = mainCamera;
        ScreenFadeController = GetComponent<RawImageFadeController>();
        ScreenFadeController.Init(screenFadeImage);
        ObjectShakeController = GetComponent<ObjectShakeController>();
        ObjectShakeController.Init();
    }

    // シーン終了処理
    public void SceneTerm()
    {
        Debug.Log("GameManager.SceneTerm()");
        //MainCamera = null;
        //ScreenFadeController = null;
        ScreenFadeController.Term();
        ObjectShakeController.Term();
    }


    public EnemyData GetLevelData(int level)
    {
        level = Mathf.Clamp(level, 1, LevelData.Length);
        return LevelData[level - 1];
    }

    private void Awake()
    {
        Debug.Log("GameManager.Awake");

        //-----------------------------------
        //pass SO info into our enemy
        var objects = UnityEngine.Object.FindObjectsOfType<EnemyController>();
        int level = Level;
        foreach (var v in objects)
        {
            Debug.Log("Setup Enemy: " + v.name);
            v.Data = GetLevelData(level);
            level++;
        }
        //-----------------------------------

        SingletonUtility<GameManager>.HanldeAwake(this);
    }

    private void OnDestroy()
    {
        SingletonUtility<GameManager>.HanldeOnDestroy(this);
    }

    void Start()
    {
    }

}
