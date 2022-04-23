using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemySpawner : MonoBehaviour
{
    public int maxEnemyQuantity;
    public int enemiesAllowed = 3;
    public float enemyInterval = 0.5f;
    public AstarData data;

    [SerializeField]
    private GameObject impPrefab;
    [SerializeField]
    private GameObject portalPrefab;
    private ArrayList locations = new ArrayList();
    private Vector3 location;
    private int enemiesSpawned = 0;
    private int curEnemies = 0;

    void Start()
    {
        AstarPath.active.data.graphs[0].GetNodes (node => {
                if (node.Walkable) {
                    locations.Add((Vector3)node.position);
                }
            });
        StartCoroutine(spawnEnemy(enemyInterval, impPrefab));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy) {
        Debug.Log("spawned enemy");
        while (!CheckSpawnRoom()) {
            // stopping call to prevent spawning with no room
        }

        // time in-between spawning
        yield return new WaitForSeconds(interval);

        // finding available node to spawn in
        Vector3 position = (Vector3) locations[Random.Range(0, locations.Count)];

        // initializing portal & waiting temp
        GameObject portal = Instantiate(portalPrefab, position, Quaternion.identity);
        yield return new WaitForSeconds(1f);

        // initializing enemy & removing portal
        GameObject newEnemy = Instantiate(enemy, position, Quaternion.identity);
        curEnemies++;
        enemiesSpawned++;
        Destroy (portal, 1.0f);

        if (enemiesSpawned >= maxEnemyQuantity) {
            StartCoroutine(spawnEnemy(interval, enemy));
        }
    }

    private bool CheckSpawnRoom() {
        if (curEnemies >= enemiesAllowed) {
            return false;
        } else {
            return true;
        }
    }

    public void EnemyDead() {
        curEnemies--;
    }
}
