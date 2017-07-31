using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerEntity playerEntity;
    private GameObject playerObject;




    /// <summary>
    /// Initialisation de l'objet
    /// </summary>
    void Start()
    {
        playerEntity = PlayerEntity.GetInstance();
        playerObject = gameObject;
    }

    /// <summary>
    /// Update appelée une fois par frame
    /// </summary>
    void Update()
    {
        Move();
        DecreasePower();
    }

    /// <summary>
    /// Récupère les saisies de déplacement et les envoie à l'objet du Player
    /// </summary>
    void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        if (moveHorizontal != 0 || moveVertical != 0)
        {
            float deltaTime = Time.deltaTime;
            playerEntity.Run(gameObject, moveHorizontal, moveVertical, deltaTime);
        }
    }

    /// <summary>
    /// Demande la diminution de l'énergie à l'aide du temps écoulé
    /// </summary>
    void DecreasePower()
    {
        float deltaTime = Time.deltaTime;
        playerEntity.EditPower(-deltaTime * PlayerEntity.POWERLOSSTIME);
    }
}