using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static TFR.InputAsset;

public class BoomPlayerMovement : MonoBehaviour
{
    #region Input
    [SerializeField]
    private InputRouter inputRouter;
    private MovementActions movement;
    #endregion

    [SerializeField] private Rigidbody2D rb;
    void Awake()
    {
        movement = inputRouter.Movement;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.
    }
}
