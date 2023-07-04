using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {
    private const float flashTime = 6;
    [SerializeField]
    private Enemy enemy;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine("SpawningFlash");
    }

    // Update is called once per frame
    void Update() {

    }

    IEnumerator SpawningFlash() {
        for (int increment = 0; increment < flashTime; increment++) {
            if (spriteRenderer.color.a == 1f) {
                spriteRenderer.color = Color.clear;
            } else {
                spriteRenderer.color = Color.white;
            }
            yield return new WaitForSeconds(.1f);
        }

        Instantiate(enemy, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
