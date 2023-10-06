using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour, ISetSpawnPosition
{
    [Header("����")]
    public Transform playerTransform;
    [Header("��������")]
    public GameSceneSO firstLoadScene;
    public GameSceneSO menuScene;

    [SerializeField] private GameSceneSO currentLoadedScene;

    private GameSceneSO sceneToLoad;
    private Vector3 positionToGo;
    public Vector3 PositionToGo;
    private bool isLoading;
    private bool fadeScreen;

    public void SetSpawnPosition(Vector3 spawnpos)
    {
        this.PositionToGo = spawnpos;
    }

    public async UniTaskVoid TeleportToSafeHouse()
    {
        await currentLoadedScene.sceneReference.UnLoadScene();
    }


    public async UniTaskVoid SwitchScene(GameSceneSO targetScene, Vector3 posToGo, bool fadeScreen)
    {
        if (isLoading)
            return;
        isLoading = true;
        sceneToLoad = targetScene;
        positionToGo = posToGo;
        this.fadeScreen = fadeScreen;

        if (currentLoadedScene != null)
        {
            await UnLoadPreviousScene();
            await LoadNewScene();
        }
        else
        {
            await LoadNewScene();
        }
    }

    private async UniTask UnLoadPreviousScene()
    {
        await currentLoadedScene.sceneReference.UnLoadScene();
    }

    private async UniTask LoadNewScene()
    {
        await sceneToLoad.sceneReference.LoadSceneAsync(loadMode: LoadSceneMode.Additive);

        currentLoadedScene = sceneToLoad;

        playerTransform.position = positionToGo;
        playerTransform.gameObject.SetActive(true);
        if (fadeScreen)
        {
            
        }
        isLoading = false;

    }
}
 public interface ISetSpawnPosition
{
    public void SetSpawnPosition(Vector3 spawnpos);
}