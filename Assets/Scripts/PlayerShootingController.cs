using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingController : MonoBehaviour
{
    [SerializeField]
    Transform weapon;
    [SerializeField]
    Rigidbody2D bulletPrefab;
    [SerializeField]
    float bulletSpeed = 10;
    [SerializeField]
    int bulletDamage = 10;


    private void Update()
    {
        Aim();
        Shoot();
    }

    private void Aim()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimDirection = (mousePos - weapon.position).normalized;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        weapon.rotation = Quaternion.Euler(0, 0, aimAngle);
    }

    private void Shoot()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 shootDirection = (mousePos - weapon.position);
            shootDirection = new Vector3(shootDirection.x, shootDirection.y, 0).normalized;
            float shootAngle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
            Rigidbody2D bullet = Instantiate(bulletPrefab, weapon.GetChild(0).position, Quaternion.Euler(0, 0, shootAngle));
            bullet.velocity = shootDirection * bulletSpeed;
            bullet.gameObject.GetComponent<Bullet>().damage = bulletDamage;
        }
    }

}
