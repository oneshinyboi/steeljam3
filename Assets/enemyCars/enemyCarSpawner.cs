using DefaultNamespace;
using UnityEngine;

namespace enemyCars
{
    public class EnemyCarSpawner : MonoBehaviour
    {
        public GameObject[] cars;
        public float minSpawnInterval = 1f;
        public float maxSpawnInterval = 3f;

        private float spawnTimer;
        public float carSpawnZPosition;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            spawnTimer = Random.Range(minSpawnInterval, maxSpawnInterval);
        }

        // Update is called once per frame
        void Update()
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0f)
            {
                int carIndex = Random.Range(0, cars.Length);
                int laneIndex = Random.Range(0, RoadInfo.current.laneXPositions.Length);

                Vector3 spawnPosition = new Vector3(RoadInfo.current.laneXPositions[laneIndex], 0.05f, carSpawnZPosition);
                Instantiate(cars[carIndex], spawnPosition, Quaternion.identity);

                spawnTimer = Random.Range(minSpawnInterval, maxSpawnInterval);
            }
        }
    }
}
