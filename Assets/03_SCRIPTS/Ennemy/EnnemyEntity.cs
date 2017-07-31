using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyEntity : Entity
{
    /// <summary>
    /// Champ de vision de l'ennemi
    /// </summary>
    public float LINEOFSIGHT = 60f;
    /// <summary>
    /// Hauteur des yeux des entités
    /// </summary>
    public float HEIGHTOFSIGHT = 0f;
    /// <summary>
    /// Portée à laquelle l'ennemi se met à frapper
    /// </summary>
    public float ATTACKRANGE = 8f;
    /// <summary>
    /// Vitesse de l'ennemi
    /// </summary>
    public float SPEED = 18f;
    /// <summary>
    /// Temps de cooldown restants d'une attaque
    /// </summary>
    private float attackCooldown = 0;
    /// <summary>
    /// Temps de cooldown fixe total pour une attaque
    /// </summary>
    private static float ATTACK_COOLDOWN = 1.4f;
    /// <summary>
    /// Délai avant la prise en compte de l'attaque
    /// </summary>
    private static float ATTACK_DELAY = 0.56f;


    /// <summary>
    /// Déplacement de l'ennemi en direction du joueur
    /// </summary>
    /// <param name="ennemy">GameObject de l'ennemi</param>
    /// <param name="playerPosition">Position du joueur vers lequel se déplacer</param>
    /// <param name="deltaTime">Temps écoulé depuis le dernier update</param>
    public void MoveToPlayer(GameObject ennemy, Vector3 playerPosition, float deltaTime)
    {
        float moveAmount = SPEED * deltaTime;
        base.MoveByTarget(ennemy, playerPosition, moveAmount);
        base.SetAngle(ennemy, playerPosition - ennemy.transform.position);
    }
    
    /// <summary>
    /// Vérifie si le Player peut être repéré
    /// </summary>
    /// <param name="playerPosition">Position du joueur</param>
    /// <param name="ennemyPosition">Position de l'ennemi</param>
    /// <returns>0 si non repéré, 1 si repéré, 2 si repéré et attaque</returns>
    public int CanDetectPlayer(Vector3 playerPosition, Vector3 ennemyPosition)
    {
        Vector3 playerEyePosition = new Vector3(playerPosition.x, HEIGHTOFSIGHT, playerPosition.z);
        Vector3 ennemyEyePosition = new Vector3(ennemyPosition.x, HEIGHTOFSIGHT, ennemyPosition.z);
        if (Physics.Linecast(playerEyePosition, ennemyEyePosition))
        {
            return 0;
        }
        else
        {
            float distanceX = Mathf.Abs(playerPosition.x - ennemyPosition.x);
            float distanceY = Mathf.Abs(playerPosition.y - ennemyPosition.y);
            float distance = Mathf.Sqrt(Mathf.Pow(distanceX, 2) + Mathf.Pow(distanceY, 2));
            return (distance < ATTACKRANGE) ? 2 : ((distance < LINEOFSIGHT) ? 1 : 0);
        }
    }

    public void Attack(GameObject ennemyObject, Animator animator)
    {
        if (animator && HasNoCooldown())
        {
            animator.SetTrigger("secondaryFire");
            EnnemyFistController fistController = ennemyObject.GetComponentInChildren<EnnemyFistController>();
            if (fistController)
            {
                fistController.DelayHit(ATTACK_DELAY);
            }
            attackCooldown = ATTACK_COOLDOWN;
        }
    }

    /// <summary>
    /// Réduit les cooldown des actions de l'ennemi
    /// </summary>
    /// <param name="deltaTime">Temps écoulé depuis la dernière update</param>
    public void DecreaseCooldowns(float deltaTime)
    {
        attackCooldown -= deltaTime;
    }
    /// <summary>
    /// Vérifie qu'aucun cooldown n'est actif
    /// </summary>
    /// <returns>Booléen vrai si aucun cooldown n'est en cours</returns>
    public bool HasNoCooldown()
    {
        return (attackCooldown <= 0);
    }
}