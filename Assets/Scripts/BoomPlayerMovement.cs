using Dawnosaur;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
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
    public PlayerAnimator AnimHandler { get; private set; }
    [Space(5)]
    [SerializeField] private LayerMask groundLayer;

    #region State Params
    public bool IsFacingRight { get; private set; }//面朝左边还是右边
    public bool IsJumping { get; private set; }//是否正在跳跃
    public bool isJumpFalling { get; private set; }
    public ExplodeState explodeState { get; private set; } = 0; //当前爆炸状态
    public int CurrentBombCount { get; private set; }//当前剩余炸弹数目
    #endregion

    #region Check Params
    //Set all of these up in the inspector
    [Header("Checks")]
    [SerializeField] private Transform _groundCheckPoint;
    //Size of groundCheck depends on the size of your character generally you want them slightly small than width (for ground) and height (for the wall check)
    [SerializeField] private Vector2 _groundCheckSize = new Vector2(0.49f, 0.03f);
    #endregion

    [Header("Configs")]
    [SerializeField]
    private MovementConfig movementConfig;

    #region Input Params
    private float moveInput;
    public float LastPressedJumpTime { get; private set; }
    public float LastPressedExplodeTime { get; private set; }
    #endregion

    #region Timers 
    public float LastOnGroundTime { get; private set; }

    #endregion
    void Awake()
    {
        movement = inputRouter.Movement;
        rb = GetComponent<Rigidbody2D>();
        CurrentBombCount = movementConfig.BombCountMax;
        AnimHandler = GetComponent<PlayerAnimator>();
    }

    // Update is called once per frame
    private void Update()
    {
        moveInput = movement.Move.ReadValue<float>();
        var jumpStart = movement.Jump.WasPressedThisFrame();
        var jumpEnd = movement.Jump.WasReleasedThisFrame();
        var explodePressed = movement.Explode.IsPressed();

        #region Timer Refresh
        LastOnGroundTime -= Time.deltaTime;
        LastPressedJumpTime -= Time.deltaTime;
        #endregion

        #region Movement
        //ground check
        if (Physics2D.OverlapBox(_groundCheckPoint.position, _groundCheckSize, 0, groundLayer)) //checks if set box overlaps with ground
        {
            if (LastOnGroundTime < -0.1f)
            {
                AnimHandler.justLanded = true;
                explodeState = ExplodeState.Default;
            }

            LastOnGroundTime = movementConfig.coyoteTime; //if so sets the lastGrounded to coyoteTime
        }
        #endregion



        #region JUMP CHECKS
        if (jumpStart)
        {
            LastPressedJumpTime = movementConfig.jumpInputBufferTime;
        }
        if (IsJumping && rb.velocity.y < 0)
        {
            IsJumping = false;

            isJumpFalling = true;
        }
        if (CanJump() && LastPressedJumpTime > 0)
        {
            IsJumping = true;
            isJumpFalling = false;
            Jump();

            AnimHandler.startedJumping = true;
        }
        #endregion

        #region Explode
        if (IsJumping || isJumpFalling)
        {
            if (explodeState == ExplodeState.Default)
            {
                if (explodePressed) {
                    StartNormalExplode();
                }
            }
        }
        if (explodeState == ExplodeState.FirstExplode)
        {
            if (explodePressed)
            {
                StartTickerExplode();
            }
        }
        #endregion
    }
    private void Jump()
    {
        //Ensures we can't call Jump multiple times from one press
        LastPressedJumpTime = 0;
        LastOnGroundTime = 0;

        #region Perform Jump
        //We increase the force applied if we are falling
        //This means we'll always feel like we jump the same amount 
        //(setting the player's Y velocity to 0 beforehand will likely work the same, but I find this more elegant :D)
        float force = movementConfig.jumpForce;
        if (rb.velocity.y < 0)
            force -= rb.velocity.y;

        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        #endregion
    }
    private void StartNormalExplode()
    {
        explodeState = ExplodeState.FirstExplode;
        rb.AddForce(movementConfig.jumpForce * Vector2.up, ForceMode2D.Impulse);
    }

    private void StartTickerExplode()
    {
        explodeState = ExplodeState.SecondExplode;
        StartCoroutine(TickerBombCountdownCoroutine(ApplyTickerExplodeForce)) ;
    }

    private void FixedUpdate()
    {
        Run(1);
    }

    private void ApplyTickerExplodeForce()
    {
        rb.AddForce(movementConfig.jumpForce * 1.5f * Vector2.up, ForceMode2D.Impulse);
    }


    //FIXME TODO 已经实现，待验证，另一种实现：开一个coroutine不断循环waitforseconds，外面写一个coroutine用来计时并Stop它

    IEnumerator TickerBombCountdownCoroutine(Action OnTickerFinish)
    {
        StartCoroutine(TickerBombLoopCoroutine());
        yield return new WaitForSeconds(movementConfig.TickerBombExplodeDelay);
        StopCoroutine(TickerBombLoopCoroutine());
        OnTickerFinish.Invoke();
    }
    IEnumerator TickerBombLoopCoroutine()
    {
        while(true)
        {
            yield return TickerBombBlinkCoroutine();
        }
    }
    IEnumerator TickerBombBlinkCoroutine()
    {
        doTickerBombBlink();
        yield return new WaitForSeconds(movementConfig.TickerBombBlinkInterval);
        yield break;
    }

    private void doTickerBombBlink()
    {
        Debug.Log("Bomb Blinked");
        //FIXME experiment only!!!
        AnimHandler.startedJumping= true;
    }

    private void Run(float lerpAmount)
    {
        //Calculate the direction we want to move in and our desired velocity
        float targetSpeed = moveInput * movementConfig.runMaxSpeed;
        //We can reduce are control using Lerp() this smooths changes to are direction and speed
        targetSpeed = Mathf.Lerp(rb.velocity.x, targetSpeed, lerpAmount);

        #region Calculate AccelRate
        float accelRate;

        //Gets an acceleration value based on if we are accelerating (includes turning) 
        //or trying to decelerate (stop). As well as applying a multiplier if we're air borne.
        if (LastOnGroundTime > 0)
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? movementConfig.runAccelAmount : movementConfig.runDeccelAmount;
        else
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? movementConfig.runAccelAmount * movementConfig.accelInAir : movementConfig.runDeccelAmount * movementConfig.deccelInAir;
        #endregion

        #region Add Bonus Jump Apex Acceleration
        //Increase are acceleration and maxSpeed when at the apex of their jump, makes the jump feel a bit more bouncy, responsive and natural
        if ((IsJumping) && Mathf.Abs(rb.velocity.y) < movementConfig.jumpHangTimeThreshold)
        {
            accelRate *= movementConfig.jumpHangAccelerationMult;
            targetSpeed *= movementConfig.jumpHangMaxSpeedMult;
        }
        #endregion

        #region Conserve Momentum
        //We won't slow the player down if they are moving in their desired direction but at a greater speed than their maxSpeed
        if (movementConfig.doConserveMomentum && Mathf.Abs(rb.velocity.x) > Mathf.Abs(targetSpeed) && Mathf.Sign(rb.velocity.x) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f && LastOnGroundTime < 0)
        {
            //Prevent any deceleration from happening, or in other words conserve are current momentum
            //You could experiment with allowing for the player to slightly increae their speed whilst in this "state"
            accelRate = 0;
        }
        #endregion

        //Calculate difference between current velocity and desired velocity
        float speedDif = targetSpeed - rb.velocity.x;
        //Calculate force along x-axis to apply to thr player

        float movement = speedDif * accelRate;

        //Convert this to a vector and apply to rigidbody
        rb.AddForce(movement * Vector2.right, ForceMode2D.Force);

        /*
         * For those interested here is what AddForce() will do
         * RB.velocity = new Vector2(RB.velocity.x + (Time.fixedDeltaTime  * speedDif * accelRate) / RB.mass, RB.velocity.y);
         * Time.fixedDeltaTime is by default in Unity 0.02 seconds equal to 50 FixedUpdate() calls per second
        */
    }

    public void CheckDirectionToFace(bool isMovingRight)
    {
        if (isMovingRight != IsFacingRight)
            Turn();
    }

    private void Turn()
    {
        //stores scale and flips the player along the x axis, 
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        IsFacingRight = !IsFacingRight;
    }

    private bool CanJump()
    {
        return LastOnGroundTime > 0 && !IsJumping;
    }
}

public enum ExplodeState
{
    Default, FirstExplode, SecondExplode
}
