using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    /// Supresseur du Singleton de PlayerEntity
    /// </summary>
    /// <returns>Instance de PlayerEntity</returns>
    static public PlayerEntity DeleteInstance()
    {

        instance = null;
        
        return instance;
    }


    /// <summary>
    /// Vitesses actuelles du joueur
    /// </summary>
    public float current_speed_x = 0f;
	public float current_speed_z = 0f;
	/// <summary>
	/// vitesse maximal du joueur
	/// </summary>
	public float MAX_SPEED = 33f;
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
    public static float POWERLOSSTIME = 3f;
    /// <summary>
    /// Energie du Player (faisant office de vie/power/...)
    /// </summary>
    public float power = POWERMAX / 2;

    /// <summary>
    /// Score du joueur
    /// </summary>
    private int score = 0;

    /// <summary>
    /// Temps de cooldown restants des attaques principales et secondaires
    /// </summary>
    private float primaryFireCooldown = 0;
    private float secondaryFireCooldown = 0;
    /// <summary>
    /// Temps de cooldown fixe total pour les attaques principales et secondaires
    /// </summary>
    private static float PRIMARY_FIRE_COOLDOWN = 0.5f;
    private static float SECONDARY_FIRE_COOLDOWN = 1f;
    /// <summary>
    /// Délai avant la prise en compte de l'attaque
    /// </summary>
    private static float PRIMARY_FIRE_DELAY = 0.22f;
    private static float SECONDARY_FIRE_DELAY = 0.56f;





    /// <summary>
    /// Déplacement du Player
    /// </summary>
    /// <param name="player">GameObject du joueur</param>
    /// <param name="horizontal">Déplacement horizontal (entre -1 et 1)</param>
    /// <param name="vertical">Déplacement vertical (entre -1 et 1)</param>
    /// <param name="deltaTime">Temps écoulé depuis la dernière update</param>
    public void Run(GameObject player, float horizontal, float vertical, float deltaTime)
    {
        current_speed_x = Mathf.Lerp (current_speed_x * deltaTime, getMaxSpeed() * horizontal, ACCELERATION);
        current_speed_z = Mathf.Lerp (current_speed_z * deltaTime, getMaxSpeed() * vertical, ACCELERATION);
		Vector3 rawMoveVector = new Vector3(current_speed_x, 0, current_speed_z);
        float maxTranslate = deltaTime * getMaxSpeed();
		base.MoveByTranslate(player, rawMoveVector, maxTranslate);
        base.SetAngle(player, rawMoveVector);
    }

    /// <summary>
    /// Attaque principale
    /// </summary>
    /// <param name="playerobject">GameObject du joueur</param>
    /// <param name="animator">Animator du joueur</param>
    /// <param name="deltaTime">Temps écoulé depuis la dernière update</param>
    public void PrimaryFire(GameObject playerObject, Animator animator)
    {
        if (animator && HasNoCooldown())
        {
            animator.SetTrigger("primaryFire");
            PlayerFistController.DelayHit(1, PRIMARY_FIRE_DELAY, 1);
            primaryFireCooldown = PRIMARY_FIRE_COOLDOWN;
        }
    }
    /// <summary>
    /// Attaque secondaire
    /// </summary>
    /// <param name="playerobject">GameObject du joueur</param>
    /// <param name="animator">Animator du joueur</param>
    /// <param name="deltaTime">Temps écoulé depuis la dernière update</param>
    public void SecondaryFire(GameObject playerObject, Animator animator)
    {
        if (animator && HasNoCooldown())
        {
            animator.SetTrigger("secondaryFire");
            PlayerFistController.DelayHit(2, SECONDARY_FIRE_DELAY, 5);
            secondaryFireCooldown = SECONDARY_FIRE_COOLDOWN;
        }
    }

    /// <summary>
    /// Réduit les cooldown des actions du joueur
    /// </summary>
    /// <param name="deltaTime">Temps écoulé depuis la dernière update</param>
    public void DecreaseCooldowns(float deltaTime)
    {
        float decreaseAmount = (power >= 85) ? deltaTime / 1.5f : deltaTime;
        primaryFireCooldown -= decreaseAmount;
        secondaryFireCooldown -= decreaseAmount;
    }
    /// <summary>
    /// Vérifie qu'aucun cooldown n'est actif
    /// </summary>
    /// <returns>Booléen vrai si aucun cooldown n'est en cours</returns>
    public bool HasNoCooldown()
    {
        return (primaryFireCooldown <= 0 && secondaryFireCooldown <= 0);
    }

    private float getMaxSpeed()
    {
        float maxSpeed = (power >= 85) ? MAX_SPEED * 1.5f : MAX_SPEED;
        return maxSpeed;
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
            DeleteInstance();
            SceneManager.LoadScene("DevGameOver");
        }
        else if (power > POWERMAX)
        {
            power = POWERMAX;
            Debug.Log("POWER OVERLOAD");
        }
        GameObject slideObject = GameObject.FindGameObjectWithTag("PowerCursor");
        if (slideObject)
        {
            Slider powerSlider = slideObject.GetComponent<Slider>();
            if (powerSlider)
            {
                powerSlider.value = Mathf.Ceil(power);
            }
        }
    }

    /// <summary>
    /// Modifie la valeur du score, et l'affiche
    /// </summary>
    /// <param name="deltaScore">Valeur à ajouter au score</param>
    public void EditScore(int deltaScore)
    {
        score += deltaScore;
        GameObject scoreObject = GameObject.FindGameObjectWithTag("Score");
        if (scoreObject)
        {
            Text scoreText = scoreObject.GetComponent<Text>();
            if (scoreText)
            {
                scoreText.text = "" + score;
            }
        }
    }
}
