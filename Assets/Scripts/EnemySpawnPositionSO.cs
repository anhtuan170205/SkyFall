using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemySpawnPositionSO", menuName = "ScriptableObject/EnemySpawnPositionSO", order = 0)]
public class EnemySpawnPositionSO : ScriptableObject 
{
    public float enemyPositionX;
    public int enemyPositionY;
}

