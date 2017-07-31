using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe abstraite héritée par les différentes entités
/// </summary>
abstract public class Entity
{
    /// <summary>
    /// Vitesse de rotation moyenne des entités
    /// </summary>
    protected const float ROTATE_SPEED = 0.2f;

    /// <summary>
    /// Déplacement par translation
    /// </summary>
    /// <param name="entityObject">GameObject à déplacer</param>
    /// <param name="translation">Vecteur contenant la translation à effectuer</param>
    /// <param name="amount">Quantité de mouvement (produit de la vitesse de l'entité avec le temps écoulé depuis update)</param>
    protected void MoveByTranslate(GameObject entityObject, Vector3 translation, float amount)
    {
        Vector3 moveVector = Vector3.ClampMagnitude(translation, amount);
        entityObject.transform.Translate(moveVector, Space.World);
    }

    /// <summary>
    /// Déplacement en direction d'une cible
    /// </summary>
    /// <param name="entityObject">GameObject à déplacer</param>
    /// <param name="targetPosition">Position vers laquelle diriger l'objet</param>
    /// <param name="amount">Quantité de mouvement (produit de la vitesse de l'entité avec le temps écoulé depuis update)</param>
    protected void MoveByTarget(GameObject entityObject, Vector3 targetPosition, float amount)
    {
        Vector3 rawMoveVector = targetPosition - entityObject.transform.position;
        Vector3 moveVector = Vector3.ClampMagnitude(rawMoveVector, amount);
        entityObject.transform.Translate(moveVector, Space.World);
    }

    /// <summary>
    /// Modifie la direction à laquelle l'entité fait face
    /// </summary>
    /// <param name="entityObject">GameObject de l'entité</param>
    /// <param name="angleVector">Vecteur pointant vers la direction souhaitée</param>
    protected void SetAngle(GameObject entityObject, Vector3 angleVector)
    {
        if (angleVector != Vector3.zero)
        {
            entityObject.transform.rotation = Quaternion.Slerp(entityObject.transform.rotation, Quaternion.LookRotation(angleVector), ROTATE_SPEED);
        }
    }

    /// <summary>
    /// Modifie l'état d'une animation de l'entité
    /// </summary>
    /// <param name="animator">Animateur lié au joueur</param>
    /// <param name="stateName">Nom de la variable d'état de l'animation concernée</param>
    /// <param name="active">Valeur à lui affecter</param>
    public void EnableAnimationState(Animator animator, string stateName, bool active)
    {
        if (animator)
        {
            animator.SetBool(stateName, active);
        }
    }
}