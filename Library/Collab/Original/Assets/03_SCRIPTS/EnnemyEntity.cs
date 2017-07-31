using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyEntity : Entity
{
    /// <summary>
    /// Champ de vision de l'ennemi
    /// </summary>
    public float LINEOFSIGHT = 10f;
    /// <summary>
    /// Vitesse de l'ennemi
    /// </summary>
    public float SPEED = 8f;

    
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
    }
    
    /// <summary>
    /// Vérifie si le Player peut être repéré
    /// </summary>
    /// <param name="playerPosition">Position du joueur</param>
    /// <param name="ennemyPosition">Position de l'ennemi</param>
    /// <returns>Booléen vrai si le joueur est suffisament proche de l'ennemi</returns>
    public bool CanDetectPlayer(Vector3 playerPosition, Vector3 ennemyPosition)
    {
        float distanceX = Mathf.Abs(playerPosition.x - ennemyPosition.x);
        float distanceY = Mathf.Abs(playerPosition.y - ennemyPosition.y);
        float distance = Mathf.Sqrt(Mathf.Pow(distanceX, 2) + Mathf.Pow(distanceY, 2));
        return distance < LINEOFSIGHT;
    }
}