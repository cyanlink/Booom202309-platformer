using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayConfig : ScriptableObject
{
    [Header("Explode")]
    public int BombCountMax;
    public Vector2 NormalExplosionForce;
    public Vector2 TickerExplosionForce;
    public float TickerBombBlinkInterval;
    public float TickerBombExplodeDelay;
}
