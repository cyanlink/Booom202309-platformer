using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "Game Scene/GameSceneSO")]
public class GameSceneSO : ScriptableObject
{
    public AssetReference sceneReference;
    public SceneType sceneType;
}

public enum SceneType
{
    Level, Menu, SafeHouse
}