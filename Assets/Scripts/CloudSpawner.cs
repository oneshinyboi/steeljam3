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
    public float minY = 20f;
    public float maxY = 30f;

    // How often to spawn a new cloud (in seconds)
    public float spawnInterval = 0.3f;

    private float timer;

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
        }
    }

    void SpawnCloud()
    {
        // 1. Pick a random sprite from the array
        int randomIndex = Random.Range(0, cloudSprites.Length);
        Sprite randomSprite = cloudSprites[randomIndex];

        // 2. Pick a random position within the defined spawn area
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPosition = new Vector3(randomX, 3, randomY);

        // 3. Create an instance of the cloud prefab at that position
        GameObject newCloud = Instantiate(cloudPrefab, spawnPosition, Quaternion.identity);

        // 4. Assign the random sprite to its Sprite Renderer
        newCloud.GetComponent<SpriteRenderer>().sprite = randomSprite;
    }
}