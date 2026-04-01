using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class LightFade : MonoBehaviour
{
    [Header("Timer.")]
    [SerializeField] float timerDuration = 5f; // Durée avant extinction progressive
    float timer;
    public float fadeDuration = 5f;  // Durée de la transition d'extinction

    /*
    [SerializeField] float jittertime = 0.3f;
    [SerializeField] float jitterRange = 2f;
    bool bJitterOn = false;
    bool jittering = false;
    float jiterCooldown = 0f;
    */

    //Utile pour le fading et reset aux bonnes valeurs.
    float maxIntensity;
    float maxRange;
    float maxInner;

    bool isTimer = true;
    bool isFading = false;
    public float currentTime = 0;
    [SerializeField] Light2D mylight;

    [Header("Debug")]
    [SerializeField] bool canDie = true;

    AudioSource source;
    [SerializeField] AudioClip a_pickup;


    void Start()
    {
        mylight = GetComponent<Light2D>();
        source = GetComponent<AudioSource>();
        timer = timerDuration;

        //Récupère la valeur donnée dans l'editeur.
        maxIntensity = mylight.intensity;
        maxRange = mylight.pointLightOuterRadius;
        maxInner = mylight.pointLightInnerRadius;
    }

    public void ResetLight()
    {
        //Reset toutes les valeurs à leur état initial.
        Debug.Log("ResetLight()");
        StopAllCoroutines();
        if (source) 
        {
            source.volume = 1;
            source.PlayOneShot(a_pickup);
        }
        timer = timerDuration;
        mylight.intensity = maxIntensity;
        mylight.pointLightOuterRadius = maxRange;
        mylight.pointLightInnerRadius = maxInner;
        isTimer = true;
        isFading = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) ResetLight();

        if (isTimer) timer -= Time.deltaTime;


        if (timer <= 0f && isFading == false)
        {
            StartCoroutine(FadeOut());
            isFading = true;
        }

        /*
        if (!bJitterOn)
        {
            float nRandom = Random.Range(0f, 1f);
            bJitterOn = true;
            jittering = true;
            if (nRandom < 0.01f)
            {
                Debug.Log("Called");
                StartCoroutine(Jitter());
            }
        }
        else
        {
            jiterCooldown += Time.fixedDeltaTime;

            if (jiterCooldown > 3f)
            {
                bJitterOn = false;
                jiterCooldown = 0f;
            }
        }
    }
        */

        IEnumerator FadeOut()
        {
            currentTime = 0f;
            float intensity = maxIntensity;
            float range = maxRange;
            float inner = maxInner;

            //On ajoute le temps entre chaque frame jusqu'à la fin.
            while (currentTime < fadeDuration)
            {
                /*
                    if (!jittering)
                {

                }
                */
                currentTime += Time.deltaTime;
                //Calcule le % de temps restant.
                float ratio = currentTime / fadeDuration;
                float t = Mathf.Clamp01(ratio);
                if (source) source.volume = 1-ratio;
                //Calcule l'intensité actuelle en fonction de l'intensité maximale et du temps restant avant d'arriver à 0.
                float currentIntensity = Mathf.Lerp(intensity, 0f, t);
                float currentRange = Mathf.Lerp(range, 2f, t);
                float currentInner = Mathf.Lerp(inner, 0f, t);
                mylight.intensity = currentIntensity;
                mylight.pointLightOuterRadius = currentRange;
                mylight.pointLightInnerRadius = currentInner;

                yield return null;
            }
            mylight.intensity = 0f;

            StartCoroutine(Death());

        }

        /*
    IEnumerator Jitter()
    {
        float currentRange = mylight.pointLightOuterRadius;
        jittering = true;
        for (int i = 0; i < Random.Range(1, 3); i++)
        {
            mylight.pointLightOuterRadius = mylight.pointLightOuterRadius + jitterRange;
            yield return new WaitForSecondsRealtime(jittertime);
            mylight.pointLightOuterRadius = currentRange;
            yield return new WaitForSecondsRealtime(jittertime);
        }
        jittering = false;
        mylight.pointLightOuterRadius = currentRange;
    }
        */

        IEnumerator Death()
        {
            yield return new WaitForSecondsRealtime(1.5f);
            if (canDie) SceneManager.LoadScene("DeathScene");
        }
    }
}
