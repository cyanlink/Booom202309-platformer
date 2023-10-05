using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class SceneLoader : MonoBehaviour, ISetSpawnPosition
{
    [Header("≥°æ∞≈‰÷√")]
    public GameSceneSO firstLoadScene;
    public GameSceneSO menuScene;

    [SerializeField] private GameSceneSO currentLoadedScene;

    private GameSceneSO sceneToLoad;

    public Vector3 PositionToGo;

    public void SetSpawnPosition(Vector3 spawnpos)
    {
        this.PositionToGo = spawnpos;
    }


}
 public interface ISetSpawnPosition
{
    public void SetSpawnPosition(Vector3 spawnpos);
}