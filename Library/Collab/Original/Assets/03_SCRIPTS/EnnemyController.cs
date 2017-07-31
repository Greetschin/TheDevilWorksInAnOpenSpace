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

    /** Tente de traquer le joueur s'il peut le détecter
     * 
     * Vérifie tout d'abord que le Player est à une distance suffisament faible, et le prend en chasse si nécessaire
     **/
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

    /** Gère les collisions avec le joueur
     * 
     **/
    void OnCollisionEnter(Collision collision)
    {
        GameObject collisionObject = collision.gameObject;
        if (collisionObject.name == "Player")
        {
            Debug.Log("collision");
        }
    }
}
