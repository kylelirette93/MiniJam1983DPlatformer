using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float movementSpeed = 10f;
    Vector2 velocity;
    Transform[] lanes;
    private int currentLane = 1; // Center lane.
    Vector3 targetPosition;

    public void SetLanes(Transform left, Transform center, Transform right)
    {
        lanes = new Transform[3];
        lanes[0] = left;
        lanes[1] = center;
        lanes[2] = right;
        targetPosition = new Vector3(lanes[currentLane].position.x, transform.position.y, transform.position.z);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLane(-1);
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveLane(1);
        }
    }

    void MoveLane(int direction)
    {
        currentLane += direction;
        currentLane = Mathf.Clamp(currentLane, 0, lanes.Length - 1);
        targetPosition = new Vector3(lanes[currentLane].position.x, transform.position.y, transform.position.z);
    }
}
