using BOOOM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPoint : MonoBehaviour
{
    [SerializeField] private SceneLoader loader;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.Player))
        {
            Debug.Log("玩家进入传送门");
            _ = loader.TeleportToSafeHouse();
        }
    }
}
