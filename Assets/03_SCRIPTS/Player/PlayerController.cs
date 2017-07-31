using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerEntity playerEntity;
    private GameObject playerObject;
    private Animator playerAnimator;
	private Light playerLigth;



    /// <summary>
    /// Initialisation de l'objet
    /// </summary>
    void Start()
    {
        playerEntity = PlayerEntity.GetInstance();
        playerObject = gameObject;
        playerAnimator = playerObject.GetComponent<Animator>();
        GameObject light = GameObject.Find("Player light");
        if (light)
        {
            playerLigth = light.GetComponent<Light>();
        }

	}

    /// <summary>
    /// Update appelée une fois par frame
    /// </summary>
    void Update()
    {
        Move();
        Attack();
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
            playerEntity.EnableAnimationState(playerAnimator, "isRunning", true);
        } else
        {
            playerEntity.EnableAnimationState(playerAnimator, "isRunning", false);
        }
    }

    /// <summary>
    /// Récupère les saisies d'attaque et les envoie à l'objet du Player
    /// </summary>
    void Attack()
    {
        bool primaryFire = Input.GetButtonDown("Fire1");
        bool secondaryFire = Input.GetButtonDown("Fire2");
        float deltaTime = Time.deltaTime;
        playerEntity.DecreaseCooldowns(deltaTime);
        if (primaryFire)
        {
            playerEntity.PrimaryFire(playerObject, playerAnimator);
        }
        if (secondaryFire)
        {
            playerEntity.SecondaryFire(playerObject, playerAnimator);
        }
    }

    /// <summary>
    /// Demande la diminution de l'énergie à l'aide du temps écoulé
    /// </summary>
    void DecreasePower()
    {
        float deltaTime = Time.deltaTime;
        playerEntity.EditPower(-deltaTime * PlayerEntity.POWERLOSSTIME);
		playerLigth.intensity = playerEntity.power * 0.2f;
    }



    /// <summary>
    /// Gestion des collisions provoquées par le joueur
    /// </summary>
    /// <param name="collision">Informations de collision</param>
    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "AttackHitbox")
        {
        }
    }
}