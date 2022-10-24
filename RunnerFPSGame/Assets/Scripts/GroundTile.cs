using UnityEngine;

public class GroundTile : MonoBehaviour
{
    GroundSpawner groundSpawner;
    [SerializeField] GameObject obstaclePreFab;
    [SerializeField] GameObject coinPreFab;
    [SerializeField] GameObject tallObstaclePreFab;
    [SerializeField] float tallObstacleChance = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();        
    }

    private void OnTriggerExit(Collider other) {
        groundSpawner.SpawnTile(true);
        Destroy(gameObject, 2);
    }

        
    public void SpawnObstacle()
    {
        GameObject obstacleToSpawn = obstaclePreFab;
        float random = Random.Range(0f, 1f);
        if (random < tallObstacleChance)
        {
            obstacleToSpawn = tallObstaclePreFab;
        }

        int obstacleSpawnIndex = Random.Range(2, 5);
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;

        Instantiate(obstacleToSpawn, spawnPoint.position, Quaternion.identity, transform);
    }

    

    public void SpawnCoins()
    {
        int coinsToSpawn = 10;

        for(int i = 0; i < coinsToSpawn; i++)
        {
            GameObject temp = Instantiate(coinPreFab,transform);
            temp.transform.position = GetRandomPointCollider(GetComponent<Collider>());
        }
    }

    Vector3 GetRandomPointCollider(Collider collider)
    {
        Vector3 point = new Vector3(
            Random.Range(collider.bounds.min.x, collider.bounds.max.x),
            Random.Range(collider.bounds.min.y, collider.bounds.max.y),
            Random.Range(collider.bounds.min.z, collider.bounds.max.z)
            );
        if (point != collider.ClosestPoint(point))
        {
            point = GetRandomPointCollider(collider);
        }

        point.y = 1;
        return point;
    }
}
