using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    // AudioSource referans�
    public AudioSource pickupAudioSource; // Ses efekti i�in AudioSource

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerShooting playerShooting = collision.GetComponent<PlayerShooting>();

            if (playerShooting != null)
            {
                playerShooting.GetComponent<PlayerShooting>().hasWeapon = true;

                // Silah al�nd���nda ses �al
                if (pickupAudioSource != null)
                {
                    pickupAudioSource.Play(); // Silah alma sesini �al
                }
            }
            Destroy(gameObject);
        }
    }
}
