using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    // Array to hold all your individual cloud sprites
    public Sprite[] cloudSprites;
    public Plane spawningPlane;

    // The GameObject prefab to spawn (the one with the Sprite Renderer)
    public GameObject cloudPrefab;

    // The area where clouds can spawn
    public float minX = -10f;
    public float maxX = 10f;
    public float minZ = 20f;
    public float maxZ = 30f;

    // Minimum and maximum spawn interval (in seconds)
    public float minSpawnInterval = 0.1f;
    public float maxSpawnInterval = 1.0f;

    private float spawnInterval;
    private float timer;

    void Start()
    {
        // Initialize spawnInterval to a random value within the range
        spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    void Update()
    {
        // Add the time since the last frame to our timer
        timer += Time.deltaTime;

        // If the timer has reached the spawn interval
        if (timer >= spawnInterval)
        {
            SpawnCloud();
            // Reset the timer
            timer = 0;
            // Set spawnInterval to a new random value within the range
            spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
        }
    }

    void SpawnCloud()
    {
        // 1. Pick a random sprite from the array
        int randomIndex = Random.Range(0, cloudSprites.Length);
        Sprite randomSprite = cloudSprites[randomIndex];

        // 2. Pick a random position within the defined spawn area
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);
        Vector3 spawnPosition = new Vector3(randomX, 4, randomZ);

        // 3. Create an instance of the cloud prefab at that position
        GameObject newCloud = Instantiate(cloudPrefab, spawnPosition, Quaternion.identity);

        // 4. Assign the random sprite to its Sprite Renderer
        newCloud.GetComponent<SpriteRenderer>().sprite = randomSprite;
    }
}