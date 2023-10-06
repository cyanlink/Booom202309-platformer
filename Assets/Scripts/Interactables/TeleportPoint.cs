using BOOOM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPoint : MonoBehaviour
{
    public SceneLoaderSO sceneLoaderSO;
    private SceneLoader loader;

    private void Start()
    {
        loader = sceneLoaderSO.loader;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.Player))
        {
            Debug.Log("��ҽ��봫����");
            _ = loader.TeleportToSafeHouse();
        }
    }
}
