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

    //�±��0��ʼ
    public int MaxLevel { get =>  Levels.Count - 1; }
}
