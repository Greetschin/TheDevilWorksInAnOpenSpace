using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    /// Vitesse du Player
    /// </summary>
    public float current_speed_x = 0f;
	public float current_speed_z = 0f;
	/// <summary>
	/// vitesse maximal du joueur
	/// </summary>
	public float MAX_SPEED = 30f;
    /// <summary>
    /// Acceleration du joueur
    /// </summary>
    public float ACCELERATION = 0.3f;

    /// <summary>
    /// Stock maximum d'énergie
    /// </summary>
    const int POWERMAX = 100;
    /// <summary>
    /// Temps avant la perte d'un point d'énergie
    /// </summary>
    public static float POWERLOSSTIME = 1f;
    /// <summary>
    /// Energie du Player (faisant office de vie/power/...)
    /// </summary>
    private float power = POWERMAX / 2;

    /// <summary>
    /// Score du joueur
    /// </summary>
    private int score = 0;





    /// <summary>
    /// Déplacement du Player
    /// </summary>
    /// <param name="player">GameObject du joueur</param>
    /// <param name="horizontal">Déplacement horizontal (entre -1 et 1)</param>
    /// <param name="vertical">Déplacement vertical (entre -1 et 1)</param>
    /// <param name="deltaTime">Temps écoulé depuis la dernière update</param>
    public void Run(GameObject player, float horizontal, float vertical, float deltaTime)
    {
        current_speed_x = Mathf.Lerp (current_speed_x * deltaTime, MAX_SPEED * horizontal, ACCELERATION );
        current_speed_z = Mathf.Lerp (current_speed_z * deltaTime, MAX_SPEED * vertical, ACCELERATION);
		Debug.Log (current_speed_x);
		Vector3 rawMoveVector = new Vector3(current_speed_x, 0, current_speed_z);
		base.MoveByVector(player, rawMoveVector);
    }

    /// <summary>
    /// Modifie la valeur d'énergie et l'affiche, la régule si nécessaire et vérifie la défaite
    /// </summary>
    /// <param name="deltaPower">Delta du power à ajouter/retirer</param>
    public void EditPower(float deltaPower)
    {
        power += deltaPower;
        if (power <= 0)
        {
            power = 0;
            Debug.Log("GAME OVER");
        }
        else if (power > POWERMAX)
        {
            power = POWERMAX;
            Debug.Log("POWER OVERLOAD");
        }
        Slider powerSlider = GameObject.FindGameObjectWithTag("PowerCursor").GetComponent<Slider>();
        powerSlider.value = Mathf.Ceil(power);
        EditScore(-1);
    }

    /// <summary>
    /// Modifie la valeur du score, et l'affiche
    /// </summary>
    /// <param name="deltaScore">Valeur à ajouter au score</param>
    public void EditScore(int deltaScore)
    {
        score += deltaScore;
        Text scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        scoreText.text = ""+score;
    }
}
