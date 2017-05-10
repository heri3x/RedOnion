using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public EnemyController Enemy;

    public EnemyData[] LevelData;

    [Range(1, 5)]
    public int Level;

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
