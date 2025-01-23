using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // UI i�in gerekli

public class NextLevelTrigger : MonoBehaviour
{
    public string targetSceneName; // Oynat�lacak sahnenin ad�
    public CanvasGroup fadeCanvasGroup; // Ekran kararmas� i�in bir Canvas Group
    public float fadeDuration = 1f; // Kararma s�resi

    private bool isFading = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isFading)
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
