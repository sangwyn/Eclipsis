using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingWeapon : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _bulletSpawner;
    [SerializeField] private float _recoilTime = 0.3f;
    private bool _canShoot = true;

    private void Update()
    {
        if (transform.parent.GetComponent<EnemyDistanceAttack>().isShooting && _canShoot)
        {
            StartCoroutine(ShootingDelay());
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(_bulletPrefab);
        bullet.transform.position = _bulletSpawner.transform.position;
        bullet.transform.rotation = this.transform.rotation;
    }

    IEnumerator ShootingDelay()
    {
        Shoot();
        _canShoot = false;
        yield return new WaitForSeconds(_recoilTime);
        _canShoot = true;
    }
}
