using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class LightFade : MonoBehaviour
{
    [Header("Timer.")]
    [SerializeField] float timerDuration = 5f; // Durée avant extinction progressive
    float timer;
    [SerializeField] float fadeDuration = 5f;  // Durée de la transition d'extinction

    //Utile pour le fading et reset aux bonnes valeurs.
    float maxIntensity;
    float maxRange;
    float maxInner;

    bool isTimer = true;
    bool isFading = false;
    Light2D mylight;

    [Header("Debug")]
    [SerializeField]bool canDie = true;

    void Start()
    {
        mylight = GetComponent<Light2D>();
        timer = timerDuration;
        //Récupère la valeur donnée dans l'editeur.
        maxIntensity = mylight.intensity;
        maxRange = mylight.pointLightOuterRadius;
        maxInner = mylight.pointLightInnerRadius;
    }

    public void ResetLight()
    {
        //Reset toutes les valeurs à leur état initial.
        StopAllCoroutines();
        timer = timerDuration;
        mylight.intensity = maxIntensity;
        mylight.pointLightOuterRadius = maxRange;
        mylight.pointLightInnerRadius = maxInner;
        isTimer = true;
        isFading = false;
    }

    void Update()
    {
        if (isTimer) timer -= Time.deltaTime;


        if (timer <= 0f && isFading == false)
        {
            StartCoroutine(FadeOut());
            isFading = true;
        } 
    }

    IEnumerator FadeOut()
    {
        Debug.Log("appelée");
        float time = 0f;
        float intensity = maxIntensity;
        float range = maxRange;
        float inner = maxInner;

        //On ajoute le temps entre chaque frame jusqu'à la fin.
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            //Calcule le % de temps restant.
            float t = Mathf.Clamp01(time / fadeDuration);
            //Calcule l'intensité actuelle en fonction de l'intensité maximale et du temps restant avant d'arriver à 0.
            float currentIntensity = Mathf.Lerp(intensity, 0f, t);
            float currentRange = Mathf.Lerp(range, 0f, t);
            float currentInner = Mathf.Lerp(inner, 0f, t);
            mylight.intensity = currentIntensity;
            mylight.pointLightOuterRadius = currentRange;
            mylight.pointLightInnerRadius = currentInner;

            yield return null;
        }
            mylight.intensity = 0f;

        StartCoroutine(Death());
    }

    IEnumerator Death()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        SceneManager.LoadScene("DeathScene");
    }
}
