using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static TFR.InputAsset;

public class BoomPlayerMovement : MonoBehaviour
{
    #region Input
    [Header("Input Router")]
    [SerializeField]
    private InputRouter inputRouter;
    private MovementActions movement;
    #endregion

    private Rigidbody2D rb;
    [Space(5)]
    [SerializeField] private LayerMask groundLayer;

    #region State Params
    public bool IsFacingRight { get; private set; }
    public bool IsJumping { get; private set; }
    public bool IsInExplosionCooldown { get; private set; }
    #endregion

    #region Check Params
    //Set all of these up in the inspector
    [Header("Checks")]
    [SerializeField] private Transform _groundCheckPoint;
    //Size of groundCheck depends on the size of your character generally you want them slightly small than width (for ground) and height (for the wall check)
    [SerializeField] private Vector2 _groundCheckSize = new Vector2(0.49f, 0.03f);
    #endregion
    void Awake()
    {
        movement = inputRouter.Movement;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var move = movement.Move.ReadValue<float>();
        var jumpStart = movement.Jump.WasPressedThisFrame();
        var jumpEnd = movement.Jump.WasReleasedThisFrame();
        var explodePressed = movement.Explode.IsPressed();
    }
}
