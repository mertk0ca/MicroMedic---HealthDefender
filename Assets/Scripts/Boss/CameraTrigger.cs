using System.Collections;
using UnityEngine;
using Cinemachine;
using TMPro;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera; // Cinemachine kamera referans�
    [SerializeField] private float newOrthoSize = 5f; // Yeni kamera boyutu
    [SerializeField] private Vector3 newCameraOffset; // Yeni kamera offset de�eri
    [SerializeField] private float transitionDuration = 1f; // Ge�i� s�resi

    [SerializeField] private GameObject healthBar; // Canvas �zerindeki can bar� objesi
    [SerializeField] private TMP_Text messageText; // TextMeshPro objesi
    [SerializeField] private float messageDisplayDuration = 3f; // Mesaj�n ekranda kalma s�resi

    private float originalOrthoSize; // Eski kamera boyutu
    private Vector3 originalCameraOffset; // Eski kamera offset de�eri
    private CinemachineTransposer transposer; // Kameran�n Transposer bile�eni
    private Coroutine currentCoroutine; // Mevcut �al��an Coroutine referans�
    private Coroutine messageCoroutine; // Mesaj i�in �al��an Coroutine referans�

    [SerializeField] private AudioSource areaEnterSound; // Alana girildi�inde �alacak ses

    void Start()
    {
        // Ba�lang�� de�erlerini kaydet
        if (virtualCamera != null)
        {
            originalOrthoSize = virtualCamera.m_Lens.OrthographicSize;

            transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
            if (transposer != null)
            {
                originalCameraOffset = transposer.m_FollowOffset;
            }
        }

        // Can bar�n� ba�lang��ta gizle
        if (healthBar != null)
        {
            healthBar.SetActive(false);
        }

        // Mesaj� ba�lang��ta gizle
        if (messageText != null)
        {
            messageText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && virtualCamera != null)
        {
            // Mevcut Coroutine'i durdur ve yeni ayarlar� uygula
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }
            currentCoroutine = StartCoroutine(ChangeCameraSettings(newOrthoSize, newCameraOffset));

            // Can bar�n� g�r�n�r yap
            if (healthBar != null)
            {
                healthBar.SetActive(true);
            }

            // Mesaj� ekrana getir
            if (messageText != null)
            {
                if (messageCoroutine != null)
                {
                    StopCoroutine(messageCoroutine);
                }
                messageCoroutine = StartCoroutine(DisplayMessage());
            }

            // Alana girildi�inde ses �al
            if (areaEnterSound != null)
            {
                areaEnterSound.Play();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && virtualCamera != null)
        {
            // Mevcut Coroutine'i durdur ve eski ayarlar� uygula
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }
            currentCoroutine = StartCoroutine(ChangeCameraSettings(originalOrthoSize, originalCameraOffset));

            // Can bar�n� gizle
            if (healthBar != null)
            {
                healthBar.SetActive(false);
            }
        }
    }

    private IEnumerator ChangeCameraSettings(float targetOrthoSize, Vector3 targetOffset)
    {
        float elapsedTime = 0f;
        float startOrthoSize = virtualCamera.m_Lens.OrthographicSize;
        Vector3 startOffset = transposer.m_FollowOffset;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;

            // Kamera boyutunu ve offset'i kademeli olarak de�i�tir
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(startOrthoSize, targetOrthoSize, elapsedTime / transitionDuration);
            transposer.m_FollowOffset = Vector3.Lerp(startOffset, targetOffset, elapsedTime / transitionDuration);

            yield return null;
        }

        // Nihai de�erleri ayarla
        virtualCamera.m_Lens.OrthographicSize = targetOrthoSize;
        transposer.m_FollowOffset = targetOffset;

        currentCoroutine = null; // Coroutine tamamland���nda s�f�rla
    }

    private IEnumerator DisplayMessage()
    {
        // Mesaj� aktif et
        messageText.gameObject.SetActive(true);

        // Yava��a belirme
        Color originalColor = messageText.color;
        originalColor.a = 0f;
        messageText.color = originalColor;

        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime;
            originalColor.a = Mathf.Lerp(0f, 1f, elapsedTime);
            messageText.color = originalColor;
            yield return null;
        }

        // Mesaj ekranda beklesin
        yield return new WaitForSeconds(messageDisplayDuration);

        // Yava��a kaybolma
        elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime;
            originalColor.a = Mathf.Lerp(1f, 0f, elapsedTime);
            messageText.color = originalColor;
            yield return null;
        }

        // Mesaj� gizle
        messageText.gameObject.SetActive(false);
    }
}
