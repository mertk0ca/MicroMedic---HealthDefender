using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // mermi prefabi
    public Transform firePoint; // merminin f�rlat�laca�� yer
    public float bulletSpeed = 10f; // mermi h�z�
    public bool hasWeapon = false;
    public int ammo = 50;

    private float cooldownDuration = 0.5f; // cooldown s�resi
    private bool isCooldown = false; // cooldown durumu

    // Ses efekti i�in AudioSource
    public AudioSource shootAudioSource; // AudioSource bile�eni

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && hasWeapon) // silah varsa
        {
            if (!isCooldown) // cooldown de�ilse
            {
                if (ammo > 0)
                {
                    Shoot();
                    ammo -= 1;
                    StartCoroutine(ShootCooldown()); // cooldown coroutine ba�lat
                    Debug.Log("Kalan Mermi: " + ammo);
                }
                else
                {
                    Debug.Log("Mermi Bitti!");
                }
            }
        }
    }

    private IEnumerator ShootCooldown()
    {
        isCooldown = true; // cooldown durumunu true yap
        yield return new WaitForSeconds(cooldownDuration); // belirtilen s�re boyunca bekle
        isCooldown = false; // cooldown durumunu false yap
    }

    void Shoot()
    {
        // Mermi olu�tur
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.right * bulletSpeed; // sa� y�ne ate� et

        // Ses �alma
        if (shootAudioSource != null)
        {
            shootAudioSource.Play(); // Ate� etme sesini �al
        }
    }
}
