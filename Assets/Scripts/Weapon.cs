using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    [SerializeField]
    private Player player;
    [SerializeField]
    private int damage;
    private Animator anim;
    private float armLength = 0.4f;
    private float secondsFromLastAttack;
    [SerializeField]
    private float attackDelay;
    private float knockbackForce = 200;

    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
        secondsFromLastAttack = attackDelay;
    }

    // Update is called once per frame
    void Update() {
        secondsFromLastAttack += Time.deltaTime;
        if (player.isAlive) {
            Vector2 mouseDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position;
            gameObject.transform.position = new Vector2(player.transform.position.x, player.transform.position.y) + (armLength * mouseDirection.normalized);
            if (mouseDirection.y < 0) {
                // Need to flip the axis we rotate on so it can move in negative angles.
                gameObject.transform.rotation = Quaternion.AngleAxis(Vector2.Angle(Vector2.right, gameObject.transform.position - player.transform.position), Vector3.back);
            } else {
                gameObject.transform.rotation = Quaternion.AngleAxis(Vector2.Angle(Vector2.right, gameObject.transform.position - player.transform.position), Vector3.forward);
            }

            if (secondsFromLastAttack >= attackDelay) {
                if (player.isAutoAttacking) {
                    anim.SetTrigger("Attack");
                    secondsFromLastAttack = 0;
                } else if (Input.GetButton("Fire1")) {
                    anim.SetTrigger("Attack");
                    secondsFromLastAttack = 0;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Enemy")) {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(player.calculateDamage() + damage);
            StartCoroutine(enemy.KnockBack(player.transform.position, knockbackForce));
        }
    }
}
