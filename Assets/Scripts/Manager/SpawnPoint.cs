using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public ISetSpawnPosition manager;
    private void Awake()
    {
        manager.SetSpawnPosition(this.transform.position);
    }
}
