﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Singleton("Game Manager", true)]
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


    public EnemyData GetLevelData(int level)
    {
        level = Mathf.Clamp(level, 1, LevelData.Length);
        return LevelData[level - 1];
    }

    private void Awake()
    {
        Debug.Log("GameManager.Awake");

        //pass SO info into our enemy
        var objects = UnityEngine.Object.FindObjectsOfType<EnemyController>();
        int level = Level;
        foreach (var v in objects)
        {
            Debug.Log("Setup Enemy: " + v.name);
            v.Data = GetLevelData(level);
            level++;
        }

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
