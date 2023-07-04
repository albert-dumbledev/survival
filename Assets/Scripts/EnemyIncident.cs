using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIncident : Enemy {
    private const float CHARGE_SPEED = 3f;
    private const float CHARGE_DELAY = 7f;
    private const float CHARGE_TIME = 3f;
    private float timeSinceLastCharge = 0f;
    private bool isCharging = false;

    // Start is called before the first frame update
    new void Start() {
        base.Start();
    }

    // Update is called once per frame
    new void Update() {
        if (!isCharging) {
            timeSinceLastCharge += Time.deltaTime;
        }

        if (col.enabled && !isKnocked) {
            if (timeSinceLastCharge < CHARGE_DELAY) {
                if (!isCharging) {
                    body.position = Vector2.MoveTowards(body.position, player.transform.position, moveSpeed * Time.deltaTime);
                }
            } else {
                timeSinceLastCharge = 0f;
                StartCoroutine("Charge");
                anim.SetTrigger("Charging");
                isCharging = true;
            }
        }
    }

    IEnumerator Charge() {
        yield return new WaitForSeconds(0.75f);
        transform.right = player.transform.position - gameObject.transform.position;
        body.velocity = transform.right.normalized * CHARGE_SPEED;
        yield return new WaitForSeconds(CHARGE_TIME);
        body.velocity = Vector2.zero;
        isCharging = false;
    }
}
