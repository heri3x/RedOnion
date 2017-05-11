using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public EnemyController Enemy;

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


    void Start()
    {
        //level <= arrya size
        if (Level > LevelData.Length)
        {
            Level = LevelData.Length;
        }

        //pass SO info into our enemy
        Enemy.Data = LevelData[Level - 1];
    }

}
