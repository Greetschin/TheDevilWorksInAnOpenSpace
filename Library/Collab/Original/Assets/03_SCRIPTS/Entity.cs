using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe abstraite héritée par les différentes entités
/// </summary>
abstract public class Entity
{
    /// <summary>
    /// Déplacement par translation
    /// </summary>
    /// <param name="entityObject">GameObject à déplacer</param>
    /// <param name="translation">Vecteur contenant la translation à effectuer</param>
    /// <param name="amount">Quantité de mouvement (produit de la vitesse de l'entité avec le temps écoulé depuis update)</param>
    protected void MoveByTranslate(GameObject entityObject, Vector3 translation, float amount)
    {
        Vector3 rawMoveVector = translation * amount;
        Vector3 moveVector = Vector3.ClampMagnitude(rawMoveVector, amount);
        entityObject.transform.Translate(moveVector);
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
        entityObject.transform.Translate(moveVector);
    }
}