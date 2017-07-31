using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyController : MonoBehaviour
{
    private EnnemyEntity ennemyEntity;
    private PlayerEntity playerEntity;
    private GameObject ennemyObject;
    private GameObject playerObject;

    // Use this for initialization
    void Start()
    {
        ennemyEntity = new EnnemyEntity();
        playerEntity = PlayerEntity.GetInstance();
        ennemyObject = gameObject;
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        TrackPlayer();
    }

    /// <summary>
    /// Tente de traquer le joueur s'il peut le détecter
    /// </summary>
    void TrackPlayer()
    {
        Vector3 ennemyPosition = ennemyObject.transform.position;
        Vector3 playerPosition = playerObject.transform.position;
        if (ennemyEntity.CanDetectPlayer(playerPosition, ennemyPosition))
        {
            float deltaTime = Time.deltaTime;
            ennemyEntity.MoveToPlayer(ennemyObject, playerPosition, deltaTime);
        }
    }

    /// <summary>
    /// Gère les collisions avec le joueur
    /// </summary>
    /// <param name="collision">Objet de collision</param>
    void OnCollisionEnter(Collision collision)
    {
        GameObject collisionObject = collision.gameObject;
        if (collisionObject.name == "Player")
        {
            playerEntity.EditPower(-EnnemyEntity.POWERDAMAGE);
            //ennemyEntity.Push(playerObject, collision.contacts[0].point, 1.0f);
            //ennemyEntity.Push(ennemyObject, collision.contacts[0].point, 0.2f);
        }
    }
}
