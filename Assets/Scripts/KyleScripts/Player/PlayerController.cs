using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float movementSpeed = 10f;

    SplineContainer _trackSpline; // Center spline for track.
    [SerializeField] float laneWidth = 0.35f; // Width between lanes.
    [SerializeField] float yOffset = 1f; // Height offset from spline.
    float laneSwitchSpeed = 10f; // Speed of lane switching.

    float splineProgress = 0f; // Progress along spline, which is normalized.
    private int currentLane = 1; // Current lane index, 0 = right, 1 = center, 2 = left.
    private float currentLaneOffset = 0f; // Current offset from the center.

    // Dash settings.
    float dashDistance = 3f;
    float dashDuration = 0.3f;
    [SerializeField] AnimationCurve dashCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    bool isDashing = false;
    float dashTimer = 0f;

    // Jump settings.
    [SerializeField] float jumpHeight = 3f;
    [SerializeField] float jumpDuration = 0.6f;
    [SerializeField] AnimationCurve jumpCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    bool isJumping = false;
    float jumpTimer = 0f;

    public void SetLanes(SplineContainer trackSpline)
    {
        // Set through on scene loaded, after grabbing center spline from scene.
        _trackSpline = trackSpline;
        // Update initial position.
        UpdateTrackPosition();
    }

    private void UpdateTrackPosition()
    {
        // Evaluate spline to get position and tangent(direction of spline at the point).
        _trackSpline.Evaluate(splineProgress, out float3 position, out float3 tangent, out float3 upVector);

        // Target offset based on current lane.
        float targetOffset = (currentLane - 1) * laneWidth;
        // Smoothly interpolate current offset to target offset.
        currentLaneOffset = Mathf.Lerp(currentLaneOffset, targetOffset, Time.deltaTime * laneSwitchSpeed);
        // Calculate right vector based on tangent and up vector.
        Vector3 right = Vector3.Cross(tangent, upVector).normalized;

        // Calculate jump offset.
        float jumpOffset = 0f;
        if (isJumping)
        {
            float normalizedJumpTime = jumpTimer / jumpDuration;
            jumpOffset = jumpCurve.Evaluate(normalizedJumpTime) * jumpHeight;
        }
        // Calculates final position with lane offset and y offset.
        Vector3 finalPosition = new Vector3(position.x, position.y + yOffset + jumpOffset, position.z) + right * currentLaneOffset;

        // Update player position and rotation.
        transform.position = finalPosition;
        transform.forward = tangent;
    }

    private void Update()
    {
        if (_trackSpline == null) return;

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

        // Handle jump input.
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            HandleJump();
        }

        float currentSpeed = movementSpeed;
        if (isDashing)
        {
            dashTimer += Time.deltaTime;
            if (dashTimer >= dashDuration)
            {
                isDashing = false;
                dashTimer = 0f;
            }
            else
            {
                float normalizedTime = dashTimer / dashDuration;
                float curveValue = dashCurve.Evaluate(normalizedTime);
                currentSpeed = movementSpeed + (dashDistance / dashDuration) * curveValue;
            }
        }

        if (isJumping)
        {
            jumpTimer += Time.deltaTime;
            if (jumpTimer >= jumpDuration)
            {
                isJumping = false;
                jumpTimer = 0f;
            }
        }
        // Get length of spline.
        float splineLength = _trackSpline.Spline.GetLength();
        // Track progress along the spline and clamp between 0 and 1.
        splineProgress += (currentSpeed / splineLength) * Time.deltaTime;
        splineProgress = Mathf.Clamp01(splineProgress);

        // Update track position based on new progress and lane.
        UpdateTrackPosition();
    }

    private void HandleDash()
    {
        isDashing = true;
        dashTimer = 0f;
        Stamina.Decrease(20f);
    }

    private void HandleJump()
    {
        isJumping = true;
        jumpTimer = 0f;
    }
}