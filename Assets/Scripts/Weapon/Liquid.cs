using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liquid : MonoBehaviour
{
    public float lifetime = 5f; // S�v�n�n ne kadar s�re sonra yok olaca��n� belirler

    void Start()
    {
        // S�v�y� belirli bir s�re sonra yok et
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Player'a �arpt���nda s�v�y� yok et
        if (collision.collider.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

        if (!collision.collider.CompareTag("Liquid") && collision.collider.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
