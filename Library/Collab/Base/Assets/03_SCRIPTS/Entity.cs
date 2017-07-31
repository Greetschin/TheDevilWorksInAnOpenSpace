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
	protected void MoveByVector(GameObject entityObject, Vector3 translation){
		entityObject.transform.Translate(translation);
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

    /*
    /// <summary>
    /// Knockback de l'entité par rapport à une origine
    /// </summary>
    /// <param name="entityObject">GameObject à pousser</param>
    /// <param name="origin">Vecteur de la position de l'origine du knockback</param>
    /// <param name="force">Force du knockback</param>
    /*public void Push(GameObject entityObject, Vector3 origin, float force)
    {
        Vector3 pushDirection = origin - entityObject.transform.position;
        pushDirection = -pushDirection.normalized;
        entityObject.GetComponent<Rigidbody>().AddForce(pushDirection * force);
    }*/
}