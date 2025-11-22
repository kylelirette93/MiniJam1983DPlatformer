using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float movementSpeed = 10f;

    SplineContainer _trackSpline; // Center spline for track.
    [SerializeField] float laneWidth = 0.35f;
    [SerializeField] float yOffset = 1f;
    float laneSwitchSpeed = 10f;

    float splineProgress = 0f;
    private int currentLane = 1;
    private float currentLaneOffset = 0f;

    public void SetLanes(SplineContainer trackSpline)
    {
        _trackSpline = trackSpline;

        UpdateTrackPosition();
    }

    private void UpdateTrackPosition()
    {
        _trackSpline.Evaluate(splineProgress, out float3 position, out float3 tangent, out float3 upVector);

        float targetOffset = (currentLane - 1) * laneWidth;
        currentLaneOffset = Mathf.Lerp(currentLaneOffset, targetOffset, Time.deltaTime * laneSwitchSpeed);

        Vector3 right = Vector3.Cross(tangent, upVector).normalized;

        Vector3 finalPosition = new Vector3(position.x, position.y + yOffset, position.z) + right * currentLaneOffset;

        transform.position = finalPosition;
        transform.forward = tangent;
    }

    private void Update()
    {
        if (_trackSpline == null) return;

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentLane = Mathf.Min(2, currentLane + 1);
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentLane = Mathf.Max(0, currentLane - 1);
        }

        float splineLength = _trackSpline.Spline.GetLength();
        splineProgress += (movementSpeed / splineLength) * Time.deltaTime;
        splineProgress = Mathf.Clamp01(splineProgress);

        UpdateTrackPosition();
    }
}