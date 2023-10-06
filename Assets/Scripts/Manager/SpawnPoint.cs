using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public ISetSpawnPosition sceneloader;
    private void Awake()
    {
        sceneloader.SetSpawnPosition(this.transform.position);
    }
}
