using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    private const float flashTime = 6;
    [SerializeField]
    private int maxHealth;
    private int currentHealth;
    protected GameObject player;
    [SerializeField]
    protected float moveSpeed;
    [SerializeField]
    protected int damage;
    [SerializeField]
    private Loot[] lootTable; 
    protected Rigidbody2D body;
    protected Collider2D col;
    protected Animator anim;
    private float timeBetweenDamage = 0.5f;
    private float lastTimeDamaged = 0;
    private SpriteRenderer spriteRenderer;
    protected bool isKnocked = false;

    // Start is called before the first frame update
    protected void Start() {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        player = GameObject.Find("Charlie");
    }

    // Update is called once per frame
    protected void Update() {
        // Shortcut.. prob want IsDead variable.
        if (col.enabled && !isKnocked) {
            body.position = Vector2.MoveTowards(body.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(int damage) {
        StartCoroutine("DamageIndicator");
        if (currentHealth - damage > 0) {
            currentHealth -= damage;
        } else {
            currentHealth = 0;
            anim.SetTrigger("IsDead");
            foreach (Loot loot in lootTable) {
                if (Random.Range(0, 100) <= loot.dropPercentage) {
                    float jitter = Random.Range(-0.2f, 0.2f);
                    Instantiate(loot.dropabble, new Vector2(transform.position.x + jitter, transform.position.y + jitter), Quaternion.identity);
                }
            }
            StartCoroutine("Die");
        }
    }

    IEnumerator Die() {
        col.enabled = false;
        yield return new WaitForSeconds(.75f);
        Destroy(this.gameObject);
    }

    internal IEnumerator KnockBack(Vector3 knockbackSourcePosition, float knockbackForce) {
        isKnocked = true;
        Vector2 knockbackPosition = transform.position + (transform.position - knockbackSourcePosition).normalized * knockbackForce;
        body.MovePosition(Vector2.MoveTowards(transform.position, knockbackPosition, 2f * Time.deltaTime));
        yield return new WaitForSeconds(0.2f);
        isKnocked = false;
    }

    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            if (Time.time > lastTimeDamaged + timeBetweenDamage) {
                lastTimeDamaged = Time.time;
                Player player = other.gameObject.GetComponent<Player>();
                player.TakeDamage(damage);
            }
        }
    }

    IEnumerator DamageIndicator() {
        for (int increment = 0; increment < flashTime; increment++) {
            if (spriteRenderer.color.a == 1f) {
                spriteRenderer.color = Color.clear;
            } else {
                spriteRenderer.color = Color.white;
            }
            yield return new WaitForSeconds(.05f);
        }
    }
}
