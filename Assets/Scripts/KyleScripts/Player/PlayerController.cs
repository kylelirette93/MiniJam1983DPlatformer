using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

/// <summary>
/// Player Controller that handles both input and movement along a spline track.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float movementSpeed = 10f;

    [Header("Track Settings")]
    SplineContainer _trackSpline; // Center spline for track.
    [SerializeField] float laneWidth = 0.35f; // Width between lanes.
    [SerializeField] float yOffset = 1f; // Height offset from spline.
    [SerializeField] float laneSwitchSpeed = 10f; // Speed of lane switching.
    private float splineProgress = 0f; // Progress along spline, which is normalized.
    private int currentLane = 1; // Current lane index, 0 = right, 1 = center, 2 = left.
    private float currentLaneOffset = 0f; // Current offset from the center.

    [Header("Dash Settings")]
    [SerializeField] float dashDistance = 3f; // Distance of dash.
    [SerializeField] float dashDuration = 0.3f; // How long player dashes for.
    [SerializeField] AnimationCurve dashCurve = AnimationCurve.EaseInOut(0, 0, 1, 1); // Dash speed curve.
    private bool isDashing = false; // Is the player dashing?
    private float dashTimer = 0f; // Timer to track dash duration.

    [Header("Jump Settings")]
    [SerializeField] float jumpHeight = 3f; // Height of jump.
    [SerializeField] float jumpDuration = 0.6f; // Duration of jump.
    [SerializeField] AnimationCurve jumpCurve = AnimationCurve.EaseInOut(0, 0, 1, 1); // Curve for jump height over time.
    private bool isJumping = false; // Is the player jumping?
    private float jumpTimer = 0f; // Timer to track jump duration.

    bool isMoving = false;

    Animator animator;

    // Reference to game state manager.
    GameStateManager gameStateManager => GameManager.Instance.GameStateManager;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Set the lanes based on provided spline track.
    /// </summary>
    /// <param name="trackSpline">Spline track that's grabbed when scene loads.</param>
    public void SetLanes(SplineContainer trackSpline)
    {
        // Set through on scene loaded, after grabbing center spline from scene.
        _trackSpline = trackSpline;
        // Update initial position.
        UpdateTrackPosition();
    }

    /// <summary>
    /// Updates players position along spline track, accounting for lane switching and jumping.
    /// </summary>
    private void UpdateTrackPosition()
    {
        // Evaluate spline to get position and tangent(direction of spline at the point).
        _trackSpline.Evaluate(splineProgress, out float3 position, out float3 tangent, out float3 upVector);

        #region Calculate Lane Offset
        // Target offset based on current lane.
        float targetOffset = (currentLane - 1) * laneWidth;
        // Smoothly interpolate current offset to target offset.
        currentLaneOffset = Mathf.Lerp(currentLaneOffset, targetOffset, Time.deltaTime * laneSwitchSpeed);
        // Calculate right vector based on tangent and up vector.
        Vector3 right = Vector3.Cross(tangent, upVector).normalized;
        #endregion

        #region Calculate Jump Offset
        float jumpOffset = 0f;
        if (isJumping)
        {
            // Get normalized time for curve evaluation (which is between 0 and 1).
            float normalizedJumpTime = jumpTimer / jumpDuration;
            // Evaluate jump curve to get offset for player's y pos.
            jumpOffset = jumpCurve.Evaluate(normalizedJumpTime) * jumpHeight;
        }
        #endregion

        #region Calculate Final Position & Rotation
        // Calculates final position with lane offset and y offset.
        Vector3 finalPosition = new Vector3(position.x, position.y + yOffset + jumpOffset, position.z) + right * currentLaneOffset;

        // Update player position and rotation.
        transform.position = finalPosition;
        transform.forward = tangent;
        #endregion
    }

    private void Update()
    {
        if (_trackSpline == null) return;

        #region Handle Lane Switching Input
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Move to left lane.
            currentLane = Mathf.Min(2, currentLane + 1);
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Move to right lane.
            currentLane = Mathf.Max(0, currentLane - 1);
        }
        #endregion

        #region Handle Dash Input
        // Handle dash input.
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing)
        {
            float stamina = Stamina.GetCurrentStamina();
            if (stamina > 0)
            {
                HandleDash();
            }
            else
            {
                Debug.Log("Shouldn't be able to dash.");
            }
        }
        #endregion

        #region Handle Jump Input
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            HandleJump();
        }
        #endregion

        #region Handle Dashing
        float currentSpeed = movementSpeed;
        if (isDashing)
        {
            animator.SetBool("IsMoving", false);
            animator.SetBool("IsDashing", true);
            dashTimer += Time.deltaTime;
            if (dashTimer >= dashDuration)
            {
                isDashing = false;
                dashTimer = 0f;
                animator.SetBool("IsDashing", false);
            }
            else
            {
                float normalizedTime = dashTimer / dashDuration;
                float curveValue = dashCurve.Evaluate(normalizedTime);
                currentSpeed = movementSpeed + (dashDistance / dashDuration) * curveValue;
            }
        }
        #endregion

        #region Handle Jumping
        if (isJumping)
        {
            jumpTimer += Time.deltaTime;
            if (jumpTimer >= jumpDuration)
            {
                isJumping = false;
                jumpTimer = 0f;
            }
        }
        #endregion

        #region Handle Movement on Spline
        // Get length of spline.
        float splineLength = _trackSpline.Spline.GetLength();
        // Track progress along the spline and clamp between 0 and 1.
        splineProgress += (currentSpeed / splineLength) * Time.deltaTime;
        splineProgress = Mathf.Clamp01(splineProgress);
        if (!isDashing)
        {
            animator.SetBool("IsMoving", true);
        }
        #endregion

        #region Handle Spline Looping.
        if (splineProgress == 1)
        {
            // Reached end of spline.
            gameStateManager.LoadNextLevel();
            splineProgress = 0f;
        }
        #endregion

        // Update track position based on new progress and lane.
        UpdateTrackPosition();
    }

    /// <summary>
    /// Handles dash action. Decreasing stamina, resetting timer and flag.
    /// </summary>
    private void HandleDash()
    {
        isMoving = false;
        isDashing = true;
        dashTimer = 0f;
        Stamina.Decrease(20f);
    }
    /// <summary>
    /// Handles jump action. Resetting timer and flag.
    /// </summary>
    private void HandleJump()
    {
        isJumping = true;
        jumpTimer = 0f;
    }
}