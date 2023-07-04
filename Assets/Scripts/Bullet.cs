using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    private const float BULLET_SPEED = 1f;
    private Rigidbody2D body;
    private GameObject player;
    private int damage;

    // Start is called before the first frame update
    void Start() {
        body = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Charlie");
        transform.right = player.transform.position - gameObject.transform.position;
        body.velocity = transform.right.normalized * BULLET_SPEED;
        // No fucking clue why it faces 270deg in the wrong direction but I'm not gonna do the math so this shit is staying.
        transform.Rotate(new Vector3(0, 0, 270));
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            Player playerToDamage = other.gameObject.GetComponent<Player>();
            playerToDamage.TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }

    internal void SetDamage(int damage) {
        this.damage = damage;
    }
}
