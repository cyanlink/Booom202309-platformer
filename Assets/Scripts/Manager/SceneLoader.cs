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
    [Header("“¿¿µ")]
    public Transform playerTransform;
    public LevelConfigSO levelConfig;
    [Header("≈‰÷√")]
    public GameSceneSO sceneToLoad;

    public SceneLoaderSO sceneLoaderSO;

    private GameSceneSO currentLoadedScene;

    private Vector3 positionToGo;
    public Vector3 PositionToGo;
    private bool isLoading;
    private bool fadeScreen;

    private int currentLevel = 0 ;

    private void Awake()
    {
        sceneLoaderSO.loader = this;
    }
    private void Start()
    {
        _ = SwitchScene(sceneToLoad, playerTransform.position, false);
    }

    public void SetSpawnPosition(Vector3 spawnpos)
    {
        this.PositionToGo = spawnpos;
    }

    public async UniTask TeleportToSafeHouse()
    {
        await SwitchScene(levelConfig.SafeHouseScene, new Vector3(0, 0, 0), true);
    }

    public async UniTask SafeHouseToNextLevel()
    {
        if (!isLoading) {
            await SwitchScene(levelConfig.Levels[currentLevel], positionToGo, true);
            currentLevel++;

            //TODO: ÷’æ÷¬ﬂº≠‘ı√¥–¥
        }
    }

    public async UniTask BackToMenu()
    {
        await SwitchScene(levelConfig.MenuScene, positionToGo, true);
    }


    public async UniTask SwitchScene(GameSceneSO targetScene, Vector3 posToGo, bool fadeScreen)
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