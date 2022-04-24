using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBounds : MonoBehaviour
{
    public LayerMask layers;
    public player2DController player;
    public Health playerHealth;
    public Transform spawnPoint;

    private ContactFilter2D filter = new ContactFilter2D();
    private Collider2D map;
    private Collider2D[] colliders = new Collider2D[8];
    private int quantity;
    private float OOBTimestamp = 0f;
    private float OOBPeriod = 0.5f;

    void Start() {
        map = GetComponent<Collider2D>();
        filter.SetLayerMask(layers);
    }

    void Update() {
        quantity = map.OverlapCollider(filter, colliders);

        if (playerHealth.Alive && quantity < 2 && !player.outOfBoundsFlag) {
            GameObject.FindWithTag("GameManager").GetComponent<GameManager>().update.SendMsg("The monsters are sure to follow.");
            ChangeAndFreezeDirection();
        } else if (quantity >= 2 && player.outOfBoundsFlag && Time.time > OOBTimestamp + OOBPeriod) {
            ReturnControl();
        } else if (player.outOfBoundsFlag && (Time.time > OOBTimestamp + (OOBPeriod*5)) && !DeathCheck()) {
            Debug.Log("go to spawn");
            RevertSpawn();
        }
    }

    void ChangeAndFreezeDirection() {
        player.targetVelocity *= -1;
        OOBTimestamp = Time.time;
        player.outOfBoundsFlag = true;
    }

    void ReturnControl() {
        player.outOfBoundsFlag = false;
    }

    void RevertSpawn() {
        OOBTimestamp = Time.time;
        player.gameObject.GetComponent<Rigidbody2D>().position = spawnPoint.position;
    }

    bool DeathCheck() {
        return player.gameObject.GetComponent<Animator>().GetBool("isDead");
    }

    // void OnDrawGizmosSelected() {
    //     Gizmos.DrawCube(transform.position, (Vector3) new Vector2(transform.localScale.x, transform.localScale.y));
    // }

}
