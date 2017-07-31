using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyController : MonoBehaviour
{
    private EnnemyEntity ennemyEntity;
    private GameObject ennemyObject;
    private Animator ennemyAnimator;
    private GameObject playerObject;
    private Vector3 rememberedPosition = Vector3.zero;



    /// <summary>
    /// Initialisation de l'objet
    /// </summary>
    void Start()
    {
        ennemyEntity = new EnnemyEntity();
        ennemyObject = gameObject;
        ennemyAnimator = ennemyObject.GetComponent<Animator>();
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    /// <summary>
    /// Update appelée une fois par frame
    /// </summary>
    void Update()
    {
        int detection = ennemyEntity.CanDetectPlayer(playerObject.transform.position, ennemyObject.transform.position);
        TrackPlayer(detection);
        Attack(detection);
    }

    /// <summary>
    /// Tente de traquer le joueur s'il peut le détecter
    /// </summary>
    private void TrackPlayer(int detectionState)
    {
        Vector3 playerPosition = playerObject.transform.position;
        float deltaTime = Time.deltaTime;
        if (detectionState > 0)
        {
            if (detectionState >= 1)
            {
                ennemyEntity.MoveToPlayer(ennemyObject, playerPosition, deltaTime);
                ennemyEntity.EnableAnimationState(ennemyAnimator, "isRunning", true);
            }
            rememberedPosition = playerPosition;
        } else if (rememberedPosition != Vector3.zero)
        {
            ennemyEntity.MoveToPlayer(ennemyObject, rememberedPosition, deltaTime);
            ennemyEntity.EnableAnimationState(ennemyAnimator, "isRunning", true);
            if (ennemyObject.transform.position == rememberedPosition)
            {
                rememberedPosition = Vector3.zero;
            }
        } else
        {
            ennemyEntity.EnableAnimationState(ennemyAnimator, "isRunning", false);
        }
    }

    /// <summary>
    /// Prépare ou demande une attaque si possible
    /// </summary>
    /// <param name="trackStatut">Statut de la détection du joueur par l'ennemi</param>
    private void Attack(int detectionState)
    {
        float deltaTime = Time.deltaTime;
        ennemyEntity.DecreaseCooldowns(deltaTime);
        if (detectionState >= 2)
        {
            ennemyEntity.Attack(ennemyObject, ennemyAnimator);
        }
    }

    /// <summary>
    /// Gère les collisions avec le joueur
    /// </summary>
    /// <param name="collision">Objet de collision</param>
    /*void OnCollisionEnter(Collision collision)
    {
        GameObject collisionObject = collision.gameObject;
        if (collisionObject.name == "Player")
        {
            playerEntity.EditPower(-EnnemyEntity.POWERDAMAGE);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ennemy contact : " + other.gameObject.name);
    }*/
}
