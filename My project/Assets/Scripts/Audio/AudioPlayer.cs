using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip stepw;
    [SerializeField] AudioClip stepc;
    [SerializeField] AudioClip stepm;

    [SerializeField] float stepcooldown = 0.35f;
    float time;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    private void Update()
    {
        time += Time.deltaTime;

        if ((Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f) && time >= stepcooldown)
        {
            //source.pitch = 1f + Random.Range(-0.2f, 0.2f);
            source.PlayOneShot(stepw);
            time = 0;
        }
    }
}
