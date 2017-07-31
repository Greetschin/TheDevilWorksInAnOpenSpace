using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFistController : MonoBehaviour {
    /// <summary>
    /// Tableau associatif des objets en collision avec la position de hitbox du poing
    /// </summary>
    private static Dictionary<int, GameObject> collidingObjects = new Dictionary<int, GameObject>();
    /// <summary>
    /// Atteste qu'un coup est en attente ; 1 pour petit coup et 2 pour grand coup
    /// </summary>
    private static int queueingHit = 0;
    /// <summary>
    /// Temps avant le coup en attente
    /// </summary>
    private static float timeBeforeHit = 0;
    /// <summary>
    /// Puissance du coup (nombre d'objets détruits)
    /// </summary>
    private static int hitPower = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        CheckHitting();
    }


    
    /// <summary>
    /// Demande un coup dans un temps donné
    /// </summary>
    /// <param name="delay">Délai avant coup effectif</param>
    public static void DelayHit(int hitType, float delay, int power = 1)
    {
        queueingHit = hitType;
        timeBeforeHit = delay;
        hitPower = power;
    }
    /// <summary>
    /// Vérifie s'il est possible de donner un coup
    /// </summary>
    private void CheckHitting()
    {
        if (queueingHit > 0)
        {
            float deltaTime = Time.deltaTime;
            timeBeforeHit -= deltaTime;
            if (timeBeforeHit <= 0)
            {
                TryToHitSomething();
                queueingHit = 0;
            }
        }
    }
    private void TryToHitSomething()
    {
        int hittedObjectCount = 0;
        foreach (GameObject objectToHit in collidingObjects.Values)
        {
            if (objectToHit && objectToHit.tag != "unbreakable" && hittedObjectCount < hitPower)
            {
                GameObject.Destroy(objectToHit);
                hittedObjectCount++;
                PlayerEntity.GetInstance().EditPower(5);
                PlayerEntity.GetInstance().EditScore(1);
            }
        }
    }

    /// <summary>
    /// Enregistre les objets en collision lorsqu'ils entrent dans la hitbox du poing
    /// </summary>
    /// <param name="collision">Informations de collision</param>
    private void OnTriggerStay(Collider other)
    {
        int objectID = other.gameObject.GetInstanceID();
        if (!collidingObjects.ContainsKey(objectID))
        {
            collidingObjects.Add(objectID, other.gameObject);
        }
    }
    /// <summary>
    /// Supprime les objets en collision lorsqu'ils sortent de la hitbox du poing
    /// </summary>
    /// <param name="collision">Informations de collision</param>
    private void OnTriggerExit(Collider other)
    {
        int objectID = other.gameObject.GetInstanceID();
        if (collidingObjects.ContainsKey(objectID))
        {
            collidingObjects.Remove(objectID);
        }
    }
}
