using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理整个游戏的状态和玩家状态
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    #region 玩家状态
    public int MaxHealth = 4;//这种最大值可以在编辑器里配置初始默认值

    [HideInInspector]
    public int Health;
    [HideInInspector]
    public int TempHealth;//临时血量护盾

    public int MaxLuckValue = 50;

    [HideInInspector]
    public int LuckValue;

    [HideInInspector]
    public int ArrowCount;
    public const int MaxArrowCount = 5;

    [HideInInspector]
    public int BombCount;
    public const int MaxBombCount = 3;

    [HideInInspector]
    public int BoostPackCount, LifeSaverCount;

    [HideInInspector]
    public int SlotMachineHackerCount, MidasArrowCount, MaskMadnessCount;
    #endregion

    //指的是老虎机roll出来的关卡状态，需要持久化在这里
    #region 关卡状态

    #endregion

    private void Start()
    {
        Health = MaxHealth;//一进游戏是最大血量
    }
}
