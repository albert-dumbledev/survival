using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodePoint : MonoBehaviour {
    private const float flashTime = 3f;
    private const float totalTime = 10f;
    private GameObject player;
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    public int value;
    void Start() {
        player = GameObject.Find("Charlie");
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine("Decay");
    }

    void Update() {
        if ((player.transform.position - transform.position).magnitude < 0.5f) {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 2f * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            Destroy(this.gameObject);
        }
    }

    IEnumerator Decay() {
        yield return new WaitForSeconds(totalTime - flashTime);
        StartCoroutine("WarningFlash");
        yield return new WaitForSeconds(flashTime);
        Destroy(this.gameObject);
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
