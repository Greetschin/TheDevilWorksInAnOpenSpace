using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity
{
    /// <summary>
    /// Instance Singleton de PlayerEntity
    /// </summary>
    private static PlayerEntity instance;
    /// <summary>
    /// Accesseur Singleton de PlayerEntity
    /// </summary>
    /// <returns>Instance de PlayerEntity</returns>
    static public PlayerEntity GetInstance()
    {
        if (instance == null)
        {
            instance = new PlayerEntity();
        }
        return instance;
    }



    /// <summary>
    /// Energie du Player (faisant office de vie/power/...)
    /// </summary>
    public int power;
    /// <summary>
    /// Vitesse du Player
    /// </summary>
    public float SPEED = 10f;
    /// <summary>
    /// Stock maximum d'énergie
    /// </summary>
    const int POWERMAX = 100;
    /// <summary>
    /// Temps avant la perte d'un point d'énergie
    /// </summary>
    public float POWERLOSSTIME = 1f;

    /// <summary>
    /// Déplacement du Player
    /// </summary>
    /// <param name="player">GameObject du joueur</param>
    /// <param name="horizontal">Déplacement horizontal (entre -1 et 1)</param>
    /// <param name="vertical">Déplacement vertical (entre -1 et 1)</param>
    /// <param name="deltaTime">Temps écoulé depuis la dernière update</param>
    public void Run(GameObject player, float horizontal, float vertical, float deltaTime)
    {
        float moveAmount = SPEED * deltaTime;
        Vector3 rawMoveVector = new Vector3(horizontal, 0, vertical);
        base.MoveByTranslate(player, rawMoveVector, moveAmount);
    }

    //
    public void Idle()
    {

    }

    //
    public void Hit()
    {

    }

    //
    public void Die()
    {

    }

    //
    public void Regen()
    {

    }


}
