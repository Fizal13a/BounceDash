using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform player;

    [Header("Spawning stats")]
    [SerializeField] private float spawnDistance = 10f;
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private float lastYSpawn;

    private void Update()
    {
        if (player.position.y + spawnDistance > lastYSpawn)
        {
            SpawnNext();
        }
    }

    void SpawnNext()
    {
        lastYSpawn += spawnInterval;
        Vector2 spawnPos = new Vector2(Random.Range(-2f, 2f), lastYSpawn);

        if (Random.value > 0.5f)
            ObjectPooler.Instance.SpawnFromPool("Coin", spawnPos, Quaternion.identity);
        else
            ObjectPooler.Instance.SpawnFromPool("Obstacle", spawnPos, Quaternion.identity);
    }
}
