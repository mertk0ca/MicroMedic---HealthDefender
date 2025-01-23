using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro namespace'ini ekledik

public class FatCount : MonoBehaviour
{
    public TextMeshProUGUI fatCountText; // UI'da Fat objeleri say�s�n� g�sterecek TextMeshProUGUI component
    public int fatCount = 0; // Fat objelerinin say�s�n� tutan de�i�ken
    public GameObject objectToDestroy; // Yok edilecek obje referans�

    void Start()
    {
        UpdateFatCount(); // Ba�lang��ta say�y� g�ncelle
    }

    void Update()
    {
        UpdateFatCount(); // Her frame'de Fat objesi say�s�n� g�ncelle
    }

    // Fat objelerinin say�s�n� g�ncelleyen fonksiyon
    private void UpdateFatCount()
    {
        // "Fat" tag'�na sahip t�m aktif objeleri bul
        GameObject[] fatObjects = GameObject.FindGameObjectsWithTag("Fat");

        // Fat objelerinin say�s�n� al
        fatCount = fatObjects.Length;

        // UI'da say�y� g�ster (e�er UI TextMeshProUGUI objesi atanm��sa)
        if (fatCountText != null)
        {
            fatCountText.text = fatCount.ToString();
        }

        // E�er Fat objesi kalmad�ysa belirli bir objeyi yok et
        if (fatCount == 0 && objectToDestroy != null)
        {
            Destroy(objectToDestroy);
        }
    }
}
