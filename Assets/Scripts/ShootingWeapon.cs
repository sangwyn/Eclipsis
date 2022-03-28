using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShootingWeapon : MonoBehaviour
{
    public FloatingJoystick _joystick;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _bulletSpawner;
    [SerializeField] private float _recoilTime = 0.3f;
    [SerializeField] private int _clipCapacity = 3;
    [SerializeField] private float _reloadTime = 1.5f;
    private int _shotMade = 0;
    private bool _canShoot = true;
    
    public AudioSource ShootSound1, ShootSound2, ShootSound3;

    private GameObject player;
    private Weapon weapon;
    private GameObject dots;
    private bool reloading = false;

    private void Start()
    {
        player = GameObject.Find("Player");
        weapon = this.gameObject.GetComponent<Weapon>();
        dots = GameObject.Find("Dots");
    }

    private void Update()
    {
        if (!_joystick)
            return;
        if (_joystick.Direction != Vector2.zero && _canShoot)
        {
            StartCoroutine(ShootingDelay());
        }
    }

    private void Shoot()
    {
        ++_shotMade;
        GameObject bullet = Instantiate(_bulletPrefab);
        bullet.GetComponent<Bullet>().SetDamage(weapon.GetWeaponDamage());
        bullet.transform.position = _bulletSpawner.transform.position;
        bullet.transform.rotation = this.transform.rotation;
        if (Math.Abs(player.transform.localScale.x - (-1)) < 0.1)
        {
            bullet.transform.Rotate(new Vector3(0, 180, 0));
        }
    }

    IEnumerator ShootingDelay()
    {
        ShootSoundPlay(Random.Range(0, 3));
        
        Shoot();
        _canShoot = false;
        yield return new WaitForSeconds(_recoilTime);
        _canShoot = true;
        if (_shotMade >= _clipCapacity)
        {
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        reloading = true;
        _canShoot = false;
        dots.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(_reloadTime / 3.0f);
        dots.transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(_reloadTime / 3.0f);
        dots.transform.GetChild(2).gameObject.SetActive(true);
        yield return new WaitForSeconds(_reloadTime / 3.0f);
        _shotMade = 0;
        _canShoot = true;
        dots.transform.GetChild(0).gameObject.SetActive(false);
        dots.transform.GetChild(1).gameObject.SetActive(false);
        dots.transform.GetChild(2).gameObject.SetActive(false);
        reloading = false;
    }

    public void ReloadFunction()
    {
        if (_shotMade == 0 || reloading)
            return;
        StartCoroutine(Reload());
    }
    
    public void BeforeSwitch()
    {
        StopAllCoroutines();
        _shotMade = 0;
        _canShoot = true;
        dots.transform.GetChild(0).gameObject.SetActive(false);
        dots.transform.GetChild(1).gameObject.SetActive(false);
        dots.transform.GetChild(2).gameObject.SetActive(false);
    }
    
    private void ShootSoundPlay(int soundNum)
    {
        if (soundNum == 0)
        {
            ShootSound1.Play();
        }
        if (soundNum == 1)
        {
            ShootSound2.Play();
        }
        if (soundNum == 2)
        {
            ShootSound3.Play();
        }
    }
}
