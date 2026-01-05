using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droppable : MonoBehaviour {
    private const float flashTime = 3f;
    private const float totalTime = 10f;
    private GameObject player;
    private SpriteRenderer spriteRenderer;
    private bool isGoingToPlayer = false;
    private float moveSpeed = 2f;

    [SerializeField]
    public int value;
    [SerializeField]
    public bool canDecay;

    void Start() {
        player = GameObject.Find("Charlie");
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (canDecay) {
            StartCoroutine("Decay");
        }
    }

    void Update() {
        if ((player.transform.position - transform.position).magnitude < 0.5f) {
            isGoingToPlayer = true;   
        }

        if (isGoingToPlayer) {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
            moveSpeed += 0.01f;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            Destroy(this.gameObject);
        }
    }

    IEnumerator Decay() {
        if (!isGoingToPlayer) {
            yield return new WaitForSeconds(totalTime - flashTime);
            StartCoroutine("WarningFlash");
            yield return new WaitForSeconds(flashTime);
            Destroy(this.gameObject);
        }
    }

    IEnumerator WarningFlash() {
        for (float increment = 0; increment < flashTime; increment += .1f) {
            if (spriteRenderer.color.a == 1f) {
                spriteRenderer.color = Color.clear;
            } else {
                spriteRenderer.color = Color.white;
            }
            yield return new WaitForSeconds(.1f);
        }
    }
}
