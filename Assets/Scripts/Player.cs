using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private const float flashTime = 4;
    [SerializeField]
    private int maxHealth;
    [SerializeField]
    private HUDController hud;
    [SerializeField]
    private float moveSpeed;
    private int currentHealth;
    private Rigidbody2D body;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private int codePoints;
    public bool isAlive = true;
    public bool isAutoAttacking = false;

    // Start is called before the first frame update
    void Start() {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        hud.UpdateHealth(currentHealth, maxHealth);
        codePoints = 0;

        spriteRenderer.color = Color.white;
    }

    // Update is called once per frame
    void Update() {
        MovePlayer(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));

        if (Input.GetButtonDown("Fire3")) {
            isAutoAttacking = !isAutoAttacking;
        }
    }

    private void MovePlayer(Vector2 inputVector) {
        body.velocity = (new Vector2(inputVector.x, inputVector.y)) * moveSpeed;
        anim.SetFloat("Speed", body.velocity.magnitude);
    }

    public void TakeDamage(int damage) {
        if (isAlive) {
            StartCoroutine("DamageIndicator");
            if (currentHealth - damage > 0) {
                currentHealth -= damage;
                hud.UpdateHealth(currentHealth, maxHealth);
            } else {
                currentHealth = 0;
                hud.UpdateHealth(currentHealth, maxHealth);
                anim.SetTrigger("IsDead");
                body.velocity = Vector2.zero;
                isAlive = false;
                StartCoroutine("EndScreen");
                this.enabled = false;
            }
        }
    }

    IEnumerator EndScreen() {
        yield return new WaitForSeconds(2f);
        hud.ShowGameOver(codePoints);
        Time.timeScale = 0;
    }

    IEnumerator DamageIndicator() {
        for (int increment = 0; increment < flashTime; increment++) {
            if (spriteRenderer.color == Color.white) {
                spriteRenderer.color = Color.red;
            } else {
                spriteRenderer.color = Color.white;
            }
            yield return new WaitForSeconds(.1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("CodePoint")) {
            codePoints += other.gameObject.GetComponent<CodePoint>().value;
            hud.UpdateCodePoints(codePoints);
        }
    }
}
