using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float movementSpeed = 10f;

    [Header("Spline settings")]
    SplineContainer[] lanes;
    private float splineProgress = 0f;
    private int currentLane = 1;
    [SerializeField] float yOffset = 1f;

    public void SetLanes(SplineContainer left, SplineContainer center, SplineContainer right)
    {
        lanes = new SplineContainer[3];
        lanes[0] = left;
        lanes[1] = center;
        lanes[2] = right;

        splineProgress = 0f;
        //Debug.Log($"Lanes set. Left null? {left == null}, Center null? {center == null}, Right null? {right == null}");
        UpdateTrackPosition();
    }

    private void UpdateTrackPosition()
    {
        if (lanes == null || lanes[currentLane] == null)
        {
            Debug.LogError("Cannot update position - lanes not set!");
            return;
        }

        SplineContainer currentSpline = lanes[currentLane];

        currentSpline.Evaluate(splineProgress, out float3 position, out float3 tangent, out float3 up);

        transform.position = new Vector3(position.x, position.y + yOffset, position.z);

        transform.forward = tangent;
    }

    private void Update()
    {
        if (lanes == null || lanes[currentLane] == null) return;

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLane(-1);
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveLane(1);
        }

        SplineContainer currentSpline = lanes[currentLane];
        float splineLength = currentSpline.Spline.GetLength();
        splineProgress += (movementSpeed / splineLength) * Time.deltaTime;

        if (splineProgress >= 1f)
        {
            // Stops at end of spline, ideally trigger level end here.
            splineProgress = 1f;
        }

        UpdateTrackPosition();
    }

    void MoveLane(int direction)
    {
        int newLane = currentLane + direction;
        newLane = Mathf.Clamp(newLane, 0, lanes.Length - 1);

        if (newLane != currentLane && lanes[newLane] != null)
        {
            currentLane = newLane;
        }
    }
}