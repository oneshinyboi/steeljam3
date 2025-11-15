using UnityEngine;

public class InfiniteRoad : MonoBehaviour
{
    public Transform player;          // Drag your player here
    public float segmentLength = 2f; // Length of ONE road piece
    public Transform[] segments;      // Drag your road segments here

    void Update()
    {
        foreach (Transform segment in segments)
        {
            // How far ahead is the player compared to this segment?
            float distance = player.position.z - segment.position.z;

            // If the segment is behind the player by more than one segment length,
            // move it in front of the furthest segment.
            if (distance > segmentLength)
            {
                float newZ = segment.position.z + segmentLength * segments.Length;
                segment.position = new Vector3(segment.position.x, segment.position.y, newZ);
            }
        }
    }
}