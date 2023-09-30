using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����������Ϸ��״̬�����״̬
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

    #region ���״̬
    public int MaxHealth = 4;//�������ֵ�����ڱ༭�������ó�ʼĬ��ֵ

    [HideInInspector]
    public int Health;
    [HideInInspector]
    public int TempHealth;//��ʱѪ������

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

    //ָ�����ϻ���roll�����Ĺؿ�״̬����Ҫ�־û�������
    #region �ؿ�״̬

    #endregion

    private void Start()
    {
        Health = MaxHealth;//һ����Ϸ�����Ѫ��
    }
}
