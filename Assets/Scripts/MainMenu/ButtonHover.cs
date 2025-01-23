using UnityEngine;
using TMPro; // TextMeshPro k�t�phanesi
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI buttonText; // TextMeshPro bile�eni
    public VertexGradient normalGradient; // Varsay�lan gradient
    public VertexGradient hoverGradient;  // Hover s�ras�nda gradient
    public float fadeDuration = 0.5f;     // Ge�i� s�resi
    public float normalCharacterSpacing = 0f; // Varsay�lan karakter bo�lu�u
    public float hoverCharacterSpacing = 10f; // Hover s�ras�nda karakter bo�lu�u
    public AudioSource hoverSound;       // Hover sesi i�in AudioSource
    public AudioSource clickSound;       // T�klama sesi i�in AudioSource

    private Coroutine currentGradientCoroutine;   // Aktif gradient Coroutine
    private Coroutine currentSpacingCoroutine;    // Aktif karakter bo�lu�u Coroutine

    private void Awake()
    {
        // Vertex gradient �zelli�ini aktif et
        buttonText.enableVertexGradient = true;
    }

    // Fare butonun �zerine geldi�inde �al���r
    public void OnPointerEnter(PointerEventData eventData)
    {
        StartGradientTransition(hoverGradient);
        StartCharacterSpacingTransition(hoverCharacterSpacing);

        // Hover sesi �al
        if (hoverSound != null)
        {
            hoverSound.Play();
        }
    }

    // Fare butondan ayr�ld���nda �al���r
    public void OnPointerExit(PointerEventData eventData)
    {
        StartGradientTransition(normalGradient);
        StartCharacterSpacingTransition(normalCharacterSpacing);
    }

    // Butona t�kland���nda ses �al�nmas�n� sa�layan fonksiyon
    public void OnButtonClick()
    {
        if (clickSound != null)
        {
            clickSound.Play();
        }
    }

    private void StartGradientTransition(VertexGradient targetGradient)
    {
        if (currentGradientCoroutine != null)
        {
            StopCoroutine(currentGradientCoroutine);
        }
        currentGradientCoroutine = StartCoroutine(GradientTransition(targetGradient));
    }

    private void StartCharacterSpacingTransition(float targetSpacing)
    {
        if (currentSpacingCoroutine != null)
        {
            StopCoroutine(currentSpacingCoroutine);
        }
        currentSpacingCoroutine = StartCoroutine(CharacterSpacingTransition(targetSpacing));
    }

    private IEnumerator GradientTransition(VertexGradient targetGradient)
    {
        VertexGradient initialGradient = buttonText.colorGradient; // Mevcut gradient
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            buttonText.colorGradient = new VertexGradient(
                Color.Lerp(initialGradient.topLeft, targetGradient.topLeft, elapsedTime / fadeDuration),
                Color.Lerp(initialGradient.topRight, targetGradient.topRight, elapsedTime / fadeDuration),
                Color.Lerp(initialGradient.bottomLeft, targetGradient.bottomLeft, elapsedTime / fadeDuration),
                Color.Lerp(initialGradient.bottomRight, targetGradient.bottomRight, elapsedTime / fadeDuration)
            );
            yield return null;
        }

        buttonText.colorGradient = targetGradient;
    }

    private IEnumerator CharacterSpacingTransition(float targetSpacing)
    {
        float initialSpacing = buttonText.characterSpacing; // Mevcut karakter bo�lu�u
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            buttonText.characterSpacing = Mathf.Lerp(initialSpacing, targetSpacing, elapsedTime / fadeDuration);
            yield return null;
        }

        buttonText.characterSpacing = targetSpacing;
    }
}
