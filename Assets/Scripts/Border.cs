using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour {
    private const float TELEPORT_OFFSET = .2f;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy")) {
            if (other.transform.position.x <= -8.8f) {
                other.transform.position = new Vector2(other.transform.position.x + TELEPORT_OFFSET, other.transform.position.y);
            }

            if (other.transform.position.y <= -4.8f) {
                other.transform.position = new Vector2(other.transform.position.x, other.transform.position.y + TELEPORT_OFFSET);
            }

            if (other.transform.position.x >= 8.8f) {
                other.transform.position = new Vector2(other.transform.position.x - TELEPORT_OFFSET, other.transform.position.y);
            }

            if (other.transform.position.y >= 4.8f) {
                other.transform.position = new Vector2(other.transform.position.x, other.transform.position.y - TELEPORT_OFFSET);
            }
        }
    }
}
