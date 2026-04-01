using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;
using static UnityEngine.GraphicsBuffer;

public class ShadowsManager : MonoBehaviour
{
    //0 = patrouille, 1 = pourchasse le joueur;
    bool state;

    [Header("Déplacement.")]

    [SerializeField] float walkspeed = 5f;
    [Tooltip("En secondes.")]
    [SerializeField] float waittime = 3f;
    public Vector2 direction;


    [Header("Attaque.")]

    [SerializeField] float speedmult = 5f;
    [Tooltip("En secondes.")]
    [SerializeField] float chasetime = 2f;
    [SerializeField] float chasecooldown = 5f;


    [Header("Detection.")]
    [SerializeField] float viewrange = 3f;
    [SerializeField] Transform playertransform;
    [SerializeField] LayerMask enviroMask;
    [SerializeField] LayerMask playerMask;
    bool canchase = true;

    //Liste des coordonées où il doit se rendre.
    public List<Transform> coordinates;
    int currentcoo = 1;
    int walkdirection = 1;
    Transform next;
    Animator myAnimator;

    void Start()
    {
        
        //Spawn à la première coordonnée et ira à la seconde.
        transform.position = coordinates[0].position;
        next = coordinates[currentcoo];
        myAnimator = GetComponent<Animator>();

        PatrolMode();
    }

    void PatrolMode()
    {
        StartCoroutine(Patrol(next));
        StartCoroutine(Detection());
    }

    void ChaseMode()
    {
         StopAllCoroutines();
        StartCoroutine(Chase());
    }
    
    
    IEnumerator Patrol(Transform target)
    {
        //Gère le déplacement : tant qu'il n'est pas à la position, il s'y dirige au pas de walkspeed * deltaTime.
        while (transform.position != target.position)
        {
            direction.x = target.position.x - transform.position.x;
            direction.y = target.position.y - transform.position.y;
            transform.position = Vector3.MoveTowards(transform.position, target.position, walkspeed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSecondsRealtime(waittime);


        //Détermine la prochaine coordonnée où se rendre.
        currentcoo+= walkdirection;
        //Sécurité si on est arrivé au bout, gère les allés retours.
        if (currentcoo > coordinates.Count-1)
        {
            walkdirection = -1 ;
            currentcoo += 2 * walkdirection;
        }

        if (currentcoo < 0)
        {
            walkdirection = +1;
            currentcoo += 2 * walkdirection;
        }

        next = coordinates[currentcoo];
        //Relance la coroutine avec la nouvelle destination.
        StartCoroutine(Patrol(next));
    }

    void Observe(Vector2 direction)
    {
        //Envoie un raycast. S'il touche 
        if (Physics2D.Raycast(transform.position, transform.TransformDirection(direction), viewrange, enviroMask) == false)
        {
            RaycastHit2D detect = Physics2D.Raycast(transform.position, transform.TransformDirection(direction), viewrange, playerMask);
            Debug.DrawRay(transform.position, transform.TransformDirection(direction) * viewrange, Color.red, 1f);
            {
                if (detect)
                {
                    {
                        if (canchase) ChaseMode();
                        Debug.Log("Vu");
                    }
                }

            }
        }
        
    }

    IEnumerator Chase()
    {
        for (int i = 0; i < 200; i++)
        {
            direction.x = playertransform.position.x - transform.position.x;
            direction.y = playertransform.position.y - transform.position.y;
            transform.position = Vector3.MoveTowards(transform.position, playertransform.position, walkspeed * speedmult * Time.deltaTime);
            yield return new WaitForSecondsRealtime(0.01f);
        }
        PatrolMode();
        canchase = false;
        StartCoroutine(Chaserecovery());
    }

    IEnumerator Chaserecovery()
    {
        yield return new WaitForSecondsRealtime(chasecooldown);
        canchase = true;
    }

    IEnumerator Detection()
    {
        Observe(Vector2.down);
        Observe(new Vector2(0.5f,-0.5f));
        Observe(Vector2.right);
        Observe(new Vector2(0.5f, 0.5f));
        Observe(Vector2.up);
        Observe(new Vector2(-0.5f, 0.5f));
        Observe(Vector2.left);
        Observe(new Vector2(-0.5f, -0.5f));
        for (int i = 0; i < 40; i++)
        {
            yield return null;
        }
        StartCoroutine(Detection());
    }


    //Gère la mort au contact
    private void OnCollisionEnter2D(Collision2D collided)
    {
            if (collided.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("DeathScene");
        }
    }

    private void Update()
    {
        if (direction.y > 0) myAnimator.SetInteger("Direction", 0);
        else if (direction.y < 0) myAnimator.SetInteger("Direction", 1);
        else if (direction.x < 0) myAnimator.SetInteger("Direction", 2);
        else if (direction.x > 0) myAnimator.SetInteger("Direction", 3);

        if (direction.x == 0 && direction.y == 0) myAnimator.SetInteger("Direction", 0);
    }
}
