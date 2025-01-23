using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    // Bu fonksiyon butona t�kland���nda �a�r�lacak
    public void RestartScene()
    {
        Time.timeScale = 1f;

        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
