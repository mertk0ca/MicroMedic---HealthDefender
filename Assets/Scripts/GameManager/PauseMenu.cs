using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    // Duraklatma men�s�n� tutacak de�i�ken
    public GameObject duraklatMenu;

    // Butona t�kland���nda �a�r�lacak fonksiyon
    public void PauseGame()
    {
        Debug.Log("Duraklatma Butonuna T�kland�"); // Duraklatma butonuna t�klan�p t�klanmad���n� g�rmek i�in.

        // Duraklatma men�s�n� aktif hale getiriyoruz
        duraklatMenu.SetActive(true);

        // Oyunu duraklat�yoruz
        Time.timeScale = 0f;
    }


    // Oyunu devam ettirme fonksiyonu
    public void ResumeGame()
    {
        // Duraklatma men�s�n� devre d��� b�rak�yoruz
        duraklatMenu.SetActive(false);

        // Oyunu devam ettiriyoruz (timeScale 1 oldu�unda oyun devam eder)
        Time.timeScale = 1f;
    }
}
