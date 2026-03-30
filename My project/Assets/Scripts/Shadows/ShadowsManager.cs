using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class ShadowsManager : MonoBehaviour
{
    //0 = patrouille, 1 = pourchasse le joueur;
    bool state;

    //Variables liées au temps.
    [SerializeField] float walkspeed = 5f;
    [SerializeField] float waittime = 3f;
    

    //Liste des coordonées où il doit se rendre.
    public List<Transform> coordinates;
    int currentcoo = 1;
    int walkdirection = 1;
    Transform next;

    void Start()
    {
        //Spawn à la première coordonnée et ira à la seconde.
        transform.position = coordinates[0].position;
        next = coordinates[currentcoo];

        StartCoroutine(Patrol(next));
    }
    
    
    IEnumerator Patrol(Transform target)
    {
        while (transform.position != target.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, walkspeed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSecondsRealtime(waittime);


        //Détermine la prochaine coordonnée où se rendre.
        currentcoo+= walkdirection;
        //Sécurité si on est arrivé au bout.
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

        Debug.Log(currentcoo);
        next = coordinates[currentcoo];

        StartCoroutine(Patrol(next));
    }


    //Gère la mort au contact
    private void OnCollisionEnter2D(Collision2D collision)
    {
        SceneManager.LoadScene("DeathScene");
    }

}
