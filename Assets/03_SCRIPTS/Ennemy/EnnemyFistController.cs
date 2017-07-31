using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyFistController : MonoBehaviour {
    /// <summary>
    /// Booléen indiquant que le player est touché si l'ennemi tape
    /// </summary>
    private bool playerReached;
    /// <summary>
    /// Atteste qu'un coup est en attente
    /// </summary>
    private bool queueingHit;
    /// <summary>
    /// Temps avant le coup en attente
    /// </summary>
    private float timeBeforeHit = 0;

    // Use this for initialization
    void Start()
    {
        playerReached = false;
        queueingHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckHitting();
    }



    /// <summary>
    /// Demande un coup dans un temps donné
    /// </summary>
    /// <param name="delay">Délai avant coup effectif</param>
    public void DelayHit(float delay)
    {
        queueingHit = true;
        timeBeforeHit = delay;
    }
    /// <summary>
    /// Vérifie s'il est possible de donner un coup
    /// </summary>
    private void CheckHitting()
    {
        if (queueingHit)
        {
            float deltaTime = Time.deltaTime;
            timeBeforeHit -= deltaTime;
            if (timeBeforeHit <= 0)
            {
                TryToHitSomething();
                queueingHit = false;
            }
        }
    }
    private void TryToHitSomething()
    {
        if (playerReached)
        {
            PlayerEntity.GetInstance().EditPower(-10);
        }
    }

    /// <summary>
    /// Enregistre que le joueur est vulnérable à un coup de poing
    /// </summary>
    /// <param name="collision">Informations de collision</param>
    private void OnTriggerStay(Collider other)
    {
        string otherTag = other.gameObject.tag;
        if (otherTag == "Player")
        {
            playerReached = true;
        }
    }
    /// <summary>
    /// Enregistre que le joueur n'est plus vulnérable à un coup de poing
    /// </summary>
    /// <param name="collision">Informations de collision</param>
    private void OnTriggerExit(Collider other)
    {
        string otherTag = other.gameObject.tag;
        if (otherTag == "Player")
        {
            playerReached = false;
        }
    }
}
