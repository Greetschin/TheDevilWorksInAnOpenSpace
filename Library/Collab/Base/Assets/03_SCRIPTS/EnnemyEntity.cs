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
    /// Vitesse de l'ennemi
    /// </summary>
    public float SPEED = 30f;
    /// <summary>
    /// Puissance de knockback de l'ennemi
    /// </summary>
    public float KNOCKBACKFORCE = 50f;
    /// <summary>
    /// Perte d'énergie entraînée par un coup de l'ennemi
    /// </summary>
    public static float POWERDAMAGE = 5;

    
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

    /*
    /// <summary>
    /// Knockback de l'entité par rapport à une origine
    /// </summary>
    /// <param name="entityObject">GameObject à pousser</param>
    /// <param name="origin">Vecteur de la position de l'origine du knockback</param>
    /// <param name="force">Force du knockback</param>
    /*public void Push(GameObject entityObject, Vector3 origin, float strengthFactor)
    {
        base.Push(entityObject, origin, KNOCKBACKFORCE*strengthFactor);
    }*/
}