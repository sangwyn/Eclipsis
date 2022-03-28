using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed = 5f;
    [SerializeField] private float _lifeTime = 2f;
    [SerializeField] private LayerMask whatIsSolid;
    [SerializeField] private int damage;
    [SerializeField] private bool isEnemyBullet = false;

    private void Start()
    {
        // уничтожение через время
        StartCoroutine(DestroyDelay());
    }

    private void FixedUpdate()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, 1, whatIsSolid);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy") && !isEnemyBullet)
            {
                hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage);
                Destroy(gameObject);
            }
            if (hitInfo.collider.CompareTag("EnemyDistanceAttack") && !isEnemyBullet)
            {
                hitInfo.collider.GetComponent<EnemyDistanceAttack>().TakeDamage(damage);
                Destroy(gameObject);
            }

            if (hitInfo.collider.CompareTag("Wall")) {
                Destroy(gameObject);
            }
        }
        // пуля летит
        transform.Translate(transform.right * _bulletSpeed * Time.deltaTime, Space.World);
    }

    IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(_lifeTime);
        Destroy(this.gameObject);
    }

    public int GetDamage()
    {
        return damage;
    }

    public void SetDamage(int dmg)
    {
        damage = dmg;
    }
}
