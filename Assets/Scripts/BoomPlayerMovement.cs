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
    [Space(5)]
    [SerializeField] private LayerMask groundLayer;

    #region State Params
    public bool IsFacingRight { get; private set; }//面朝左边还是右边
    public bool IsJumping { get; private set; }//是否正在跳跃
    public bool IsInExplosionCooldown { get; private set; }//当前是否允许爆炸（爆炸冷却）
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
    private GameplayConfig gameplayConfig;
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
        CurrentBombCount = gameplayConfig.BombCountMax;
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
        #endregion

        #region Movement

        #endregion
    }

    private void FixedUpdate()
    {
        
    }


    //FIXME 另一种实现：开一个coroutine不断循环waitforseconds，外面写一个coroutine用来计时并Stop它
    IEnumerator TickerBombCountDownCoroutine()
    {
        var time = Time.time;
        var maxTime = time + gameplayConfig.TickerBombExplodeDelay;
        while(true)
        {
            time = Time.time;
            if(time >= maxTime)
            {
                yield break;
            }
            yield return TickerBombBlinkCoroutine();
        }
    }
    IEnumerator TickerBombBlinkCoroutine()
    {
        doTickerBombBlink();
        yield return new WaitForSeconds(gameplayConfig.TickerBombBlinkInterval);
        yield break;
    }

    private void doTickerBombBlink()
    {
        Debug.Log("Bomb Blinked");
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
}
