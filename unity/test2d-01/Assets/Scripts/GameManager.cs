using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SingletonAttribute("Game Manager", true)]
public class GameManager : MonoBehaviour
{
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


    public static GameManager Instance
    {
        get { return SingletonUtility<GameManager>.Instance; }
    }


    private void Awake()
    {
        SingletonUtility<GameManager>.HanldeAwake(this);
    }

    private void OnDestroy()
    {
        SingletonUtility<GameManager>.HanldeOnDestroy(this);
    }

    void Start()
    {
        //level <= arrya size
        if (Level > LevelData.Length)
        {
            Level = LevelData.Length;
        }

        //pass SO info into our enemy
        var objects = UnityEngine.Object.FindObjectsOfType<EnemyController>();
        foreach (var v in objects)
        {
            v.Data = LevelData[Level - 1];
        }
    }

}
