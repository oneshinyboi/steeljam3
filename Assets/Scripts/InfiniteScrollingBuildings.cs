using UnityEngine;

public class InfiniteScrollingBuildings : MonoBehaviour
{
    [Header("Building Prefabs")]
    public GameObject[] buildingPrefabs;   // Drop your 3D building prefabs here

    [Header("Pool Settings")]
    public int poolSize = 30;              // How many buildings existing at once

    [Header("Side Positions")]
    public float leftX = -1.3f;             // X position for left side of the road
    public float rightX = 4f;             // X position for right side of the road

    [Header("Spacing Along Z")]
    public float minSpacing = 1f;          // Min Z distance between buildings
    public float maxSpacing = 2f;         // Max Z distance between buildings

    [Header("Scrolling")]
    public float scrollSpeed = 10f;        // How fast the world moves towards the camera
    public float despawnZ = -30f;          // When a building passes this Z, recycle it

    private Transform[] pool;

    void Start()
    {
        pool = new Transform[poolSize];

        // Start spawning a bit in front of the camera
        float z = 10f; // starting distance in front of camera

        for (int i = 0; i < poolSize; i++)
        {
            z += Random.Range(minSpacing, maxSpacing);

            bool leftSide = Random.value < 0.5f;
            float x = leftSide ? leftX : rightX;

            GameObject prefab = buildingPrefabs[Random.Range(0, buildingPrefabs.Length)];
            GameObject instance = Instantiate(prefab, new Vector3(x, 0f, z), Quaternion.identity, transform);

            // Optional random rotation & scale
            //instance.transform.rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
            //float randomScale = Random.Range(0.8f, 1.3f);
            //instance.transform.localScale *= randomScale;

            pool[i] = instance.transform;
        }
    }

    void Update()
    {
        float moveAmount = scrollSpeed * Time.deltaTime;

        // First find the furthest building in front
        float maxZ = float.MinValue;
        for (int i = 0; i < pool.Length; i++)
        {
            Transform t = pool[i];
            // Move all buildings towards the camera (negative Z)
            t.position += new Vector3(0f, 0f, -moveAmount);

            if (t.position.z > maxZ)
                maxZ = t.position.z;
        }

        // Recycle buildings that have gone behind the camera
        for (int i = 0; i < pool.Length; i++)
        {
            Transform t = pool[i];

            if (t.position.z < despawnZ)
            {
                float newZ = maxZ + Random.Range(minSpacing, maxSpacing);
                bool leftSide = Random.value < 0.5f;
                float x = leftSide ? leftX : rightX;

                t.position = new Vector3(x, t.position.y, newZ);

                // Randomize again so it doesn't feel copy-pasted
                t.rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
                float randomScale = Random.Range(0.8f, 1.3f);
                t.localScale = Vector3.one * randomScale;
            }
        }
    }
}
