using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemySpawner : MonoBehaviour
{
    public int maxEnemyQuantity;
    public int enemiesAllowed = 3;
    public float enemyInterval = 0.5f;
    public float portalSpan = 1f;
    public AstarData data;

    [SerializeField]
    private GameObject impPrefab;
    [SerializeField]
    private GameObject portalPrefab;
    private ArrayList locations = new ArrayList();
    private Vector3 location;
    private float spawnTime = 0f;
    private int enemiesSpawned = 0;
    private int curEnemies = 0;

    void Start()
    {
        AstarPath.active.data.graphs[0].GetNodes (node => {
                if (node.Walkable) {
                    locations.Add((Vector3)node.position);
                }
            });
    }

    void Update() {
        if (enemiesSpawned < maxEnemyQuantity) {
            if (CheckSpawnRoom() && curEnemies < enemiesAllowed && (Time.time > spawnTime + enemyInterval)) {
                // finding valid position
                Vector3 position = (Vector3) locations[Random.Range(0, locations.Count)];

                // spawning portal and enemy
                GameObject portal = Instantiate(portalPrefab, position, Quaternion.identity);
                GameObject newEnemy = Instantiate(impPrefab, position, Quaternion.identity);

                // increment curEnemies, enesmiesSpawned, and destroy portal
                curEnemies++;
                enemiesSpawned++;
                Destroy (portal, portalSpan);

                // set spawnTime
                spawnTime = Time.time;
            }
        } else {
            this.enabled = false;
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
