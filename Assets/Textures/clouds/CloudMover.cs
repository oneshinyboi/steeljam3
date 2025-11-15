using UnityEngine;

public class CloudMover : MonoBehaviour
{
    public float baseSpeed = 1f; // Base speed towards negative z
    public float speedVariation = 0.5f; // Variation in speed along z
    public float lateralVariation = 0.2f; // Max lateral speed in x and y

    private Vector3 velocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float zSpeed = -baseSpeed + Random.Range(-speedVariation, speedVariation);
        float xSpeed = Random.Range(-lateralVariation, lateralVariation);

        velocity = new Vector3(xSpeed, 0, zSpeed);

        Destroy(gameObject, 30f); // Despawn after 30 seconds
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity * Time.deltaTime;
    }

    // LateUpdate ensures billboard rotation happens after camera movement
    void LateUpdate()
    {
        if (Camera.main != null)
        {
            // Make the cloud face the camera only on Y-axis rotation
            Vector3 directionToCamera = Camera.main.transform.position - transform.position;
            directionToCamera.y = 0; // Keep clouds upright by ignoring y-axis rotation

            if (directionToCamera != Vector3.zero)
            {
                // Only rotate around Y-axis to face camera horizontally
                float yRotation = Mathf.Atan2(directionToCamera.x, directionToCamera.z) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, yRotation, 0);
            }
        }
    }
}
