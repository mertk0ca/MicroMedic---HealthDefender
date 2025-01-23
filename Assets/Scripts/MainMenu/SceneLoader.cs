using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class SceneLoader : MonoBehaviour
{
    public string sceneName; // Y�klenecek sahnenin ad�
    public Image fadeImage; // Fade efekti i�in kullan�lan Image
    public float fadeDuration = 1f; // Fade s�resi
    public string saveFileName = "saveData.json"; // Kaydedilen dosyan�n ad� (�rnek: playerSave.json)

    // Bu metodu butonun OnClick() event'ine ba�layabilirsiniz
    public void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            // Eski kayd� sil
            DeleteSaveFile();

            // Fade efekti ve sahne y�kleme
            StartCoroutine(FadeOutAndLoadScene());
        }
        else
        {
            Debug.LogError("Scene name is not set!");
        }
    }

    // Fade Out i�lemi ve sahne y�kleme
    private IEnumerator FadeOutAndLoadScene()
    {
        Time.timeScale = 1f;
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        // Fade Out i�lemi
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        // Yeni sahneyi y�kle
        SceneManager.LoadScene(sceneName);
    }

    // Eski kayd� silme fonksiyonu
    private void DeleteSaveFile()
    {
        string filePath = Path.Combine(Application.persistentDataPath, saveFileName);

        // Dosya varsa sil
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log("Save file deleted.");
        }
        else
        {
            Debug.Log("No save file found to delete.");
        }
    }
}
