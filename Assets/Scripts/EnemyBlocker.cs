using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlocker : Enemy {
    private float shootDelay = 5f;
    private float timeSinceLastShot = 0f;
    [SerializeField]
    private Bullet bullet;
    private bool isShooting = false;

    // Start is called before the first frame update
    new void Start() {
        base.Start();
    }

    // Update is called once per frame
    new void Update() {
        timeSinceLastShot += Time.deltaTime;

        if (base.col.enabled && !isKnocked) {
            if (timeSinceLastShot < shootDelay) {
                float distanceFromPlayer = (gameObject.transform.position - base.player.transform.position).magnitude;
                if (distanceFromPlayer > 3 && !isShooting) {
                    base.body.position = Vector2.MoveTowards(base.body.position, base.player.transform.position, base.moveSpeed * Time.deltaTime);
                } else if (distanceFromPlayer < 3 && !isShooting) {
                    base.body.position = Vector2.MoveTowards(base.body.position, -base.player.transform.position, base.moveSpeed * Time.deltaTime);
                }
            } else {
                timeSinceLastShot = 0f;
                StartCoroutine("Shoot");
                base.anim.SetTrigger("Shooting");
                isShooting = true;
            }
        }
    }

    IEnumerator Shoot() {
        yield return new WaitForSeconds(0.5f);
        Bullet newBullet = Instantiate(bullet, base.body.position, Quaternion.identity);
        newBullet.SetDamage(base.damage);
        isShooting = false;
    }
}
