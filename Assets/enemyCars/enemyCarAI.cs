using UnityEngine;
using DefaultNamespace;

public class enemyCarAI : MonoBehaviour
{
    public float minBackwardVelocity;
    public float maxBackwardVelocity;

    [Header("Lane Changing")]
    public float laneChangeChance = 0.02f; // Chance per frame to attempt lane change
    public float laneChangeSpeed = 2f;

    private float currentVelocity;
    private int currentLaneIndex;
    private bool isChangingLanes = false;
    private float targetXPosition;

    void Start()
    {
        // Set random backward velocity
        currentVelocity = Random.Range(minBackwardVelocity, maxBackwardVelocity);

        // Find closest lane to current position
        FindCurrentLane();
    }

    void Update()
    {
        // Move backward along Z-axis
        transform.Translate(0, 0, -currentVelocity * Time.deltaTime);

        // Handle lane changing
        if (isChangingLanes)
        {
            MoveBetweenLanes();
        }
        else if (Random.value < laneChangeChance)
        {
            AttemptLaneChange();
        }
    }

    void FindCurrentLane()
    {
        if (RoadInfo.current == null) return;

        float closestDistance = float.MaxValue;
        for (int i = 0; i < RoadInfo.current.laneXPositions.Length; i++)
        {
            float distance = Mathf.Abs(transform.position.x - RoadInfo.current.laneXPositions[i]);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                currentLaneIndex = i;
            }
        }
    }

    void AttemptLaneChange()
    {
        if (RoadInfo.current == null || isChangingLanes) return;

        // Randomly choose left or right lane
        int direction = Random.value > 0.5f ? 1 : -1;
        int targetLaneIndex = currentLaneIndex + direction;

        // Check if target lane exists
        if (targetLaneIndex < 0 || targetLaneIndex >= RoadInfo.current.laneXPositions.Length)
            return;

        // Check for colliders in target lane
        float targetX = RoadInfo.current.laneXPositions[targetLaneIndex];
        if (IsLaneClear(targetX))
        {
            StartLaneChange(targetLaneIndex, targetX);
        }
    }

    bool IsLaneClear(float targetX)
    {
        // Check for colliders within lane distance in X direction
        Vector3 checkPosition = new Vector3(targetX, transform.position.y, transform.position.z);
        Collider[] colliders = Physics.OverlapSphere(checkPosition, 0.3f);

        foreach (Collider col in colliders)
        {
            return false; // Lane is blocked
        }

        return true; // Lane is clear
    }

    void StartLaneChange(int newLaneIndex, float newTargetX)
    {
        currentLaneIndex = newLaneIndex;
        targetXPosition = newTargetX;
        isChangingLanes = true;
    }

    void MoveBetweenLanes()
    {
        // Smoothly move to target lane position
        float newX = Mathf.MoveTowards(transform.position.x, targetXPosition, laneChangeSpeed * Time.deltaTime);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        // Check if lane change is complete
        if (Mathf.Abs(transform.position.x - targetXPosition) < 0.1f)
        {
            isChangingLanes = false;
        }
    }
}
