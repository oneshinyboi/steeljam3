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
}
