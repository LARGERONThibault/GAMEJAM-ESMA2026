using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightTimerFade : MonoBehaviour
{
    [Header("Réglages du Timer")]
    public float timerDuration = 5f; // Durée avant extinction progressive
    private float timer;

    [Header("Réglages de la Lumière")]
    [Tooltip("Glisser ici une Light2D ou laisser vide pour utiliser la Light attachée.")]
    public Light2D targetLight2D;    // URP 2D light
    public Light targetLight3D;      // fallback pour Light classique

    public float fadeDuration = 2f;  // Durée de la transition d'extinction

    private bool isFading = false;
    private float initialIntensity;

    void Start()
    {
        // Récupère automatiquement la Light2D si possible
        if (targetLight2D == null)
            targetLight2D = GetComponent<Light2D>();

        // Si pas de Light2D, on essaye la Light 3D
        if (targetLight2D == null && targetLight3D == null)
            targetLight3D = GetComponent<Light>();

        if (targetLight2D == null && targetLight3D == null)
        {
            Debug.LogError("Aucune Light2D ni Light trouvée ! Assigne-en une dans l'inspecteur.");
            enabled = false;
            return;
        }

        // Initialiser l'intensité de départ correctement
        initialIntensity = (targetLight2D != null) ? targetLight2D.intensity : targetLight3D.intensity;
        timer = timerDuration;
    }

    void Update()
    {
        if (isFading) return;

        timer -= Time.deltaTime;
        if (timer <= 0f)
            StartCoroutine(FadeOutLight());
    }

    private IEnumerator FadeOutLight()
    {
        isFading = true;
        float elapsed = 0f;
        float startIntensity = initialIntensity;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / fadeDuration);
            float current = Mathf.Lerp(startIntensity, 0f, t);

            if (targetLight2D != null)
                targetLight2D.intensity = current;
            if (targetLight3D != null)
                targetLight3D.intensity = current;

            yield return null;
        }

        // S'assurer que l'intensité est bien à 0 à la fin
        if (targetLight2D != null)
            targetLight2D.intensity = 0f;
        if (targetLight3D != null)
            targetLight3D.intensity = 0f;

        // Optionnel : désactiver la composante lumière après le fade (garde le fade visible)
        if (targetLight2D != null)
            targetLight2D.enabled = false;
        if (targetLight3D != null)
            targetLight3D.enabled = false;
    }
}
