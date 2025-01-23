using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // UI i�in gerekli

public class NextLevelScene2 : MonoBehaviour
{
    public string targetSceneName; // Oynat�lacak sahnenin ad�
    public CanvasGroup fadeCanvasGroup; // Ekran kararmas� i�in bir Canvas Group
    public float fadeDuration = 1f; // Kararma s�resi

    private bool isFading = false;

    // FatCount scriptine referans
    public FatCount fatCountScript;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // E�er oyuncu etkile�imdeyse ve fatCount 0 ise sahne ge�i�ini ba�lat
        if (other.CompareTag("Player") && fatCountScript.fatCount == 0 && !isFading)
        {
            StartCoroutine(FadeAndLoadScene());
        }
    }

    private IEnumerator FadeAndLoadScene()
    {
        isFading = true;

        // Ekran kararmas�
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Clamp01(timer / fadeDuration);
            yield return null;
        }

        // Sahne y�kle
        SceneManager.LoadScene(targetSceneName);
    }
}