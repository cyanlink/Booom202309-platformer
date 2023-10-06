using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Level Config")]
public class LevelConfigSO : ScriptableObject
{
    public List<GameSceneSO> Levels;
    public GameSceneSO SafeHouseScene;
    public GameSceneSO MenuScene;

    //下标从0开始
    public int MaxLevel { get =>  Levels.Count - 1; }
}
