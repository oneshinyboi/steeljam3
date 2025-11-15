using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;

    void Update() {
        var position = player.position;
        position.z += 0.01f;
        player.position = position;
    }

}
