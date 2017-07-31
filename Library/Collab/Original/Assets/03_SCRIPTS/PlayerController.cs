using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerEntity playerEntity;
    private GameObject playerObject;
    private GameObject cameraTrigger;
    private bool cameraMove;
	private bool inFrame;

    // Use this for initialization
    void Start()
    {
        playerEntity = PlayerEntity.GetInstance();
        playerObject = gameObject;
        cameraMove = false; 
        cameraTrigger = GameObject.Find("TriggerCamera");

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    /** Fonction récupérant l'utilisation des touches déplacement
     * 
     * Envoie au Player les directions de déplacement demandées (ainsi que le GameObject et le temps écoulé)
     **/
    void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        if (moveHorizontal != 0 || moveVertical != 0)
        {
			isLeavingCamera (moveHorizontal, moveVertical);
            float deltaTime = Time.deltaTime;
            playerEntity.Run(gameObject, moveHorizontal, moveVertical, deltaTime);
            if (cameraMove)
            {
                playerEntity.Run(cameraTrigger, moveHorizontal, moveVertical, deltaTime);
            }
        }
    }
	/// <summary>
	/// Ises the leaving camera.
	/// </summary>
	void isLeavingCamera(float horizontal, float vertical){

		Vector3 fwd = transform.TransformDirection(horizontal, 0 , vertical);
		if (!inFrame) {
			cameraMove = true;
		}
		if (Physics.Raycast (this.transform.position, fwd, 1)) {
			Debug.Log ("collide");
			cameraMove = false;
		}


	}
    //Fonction permetant de move la caméra lorsque le joueur sort du trigger
    void OnTriggerExit(Collider TriggerCamera)
    {
        inFrame = false;
        Debug.Log("ExitTrigger");
    }
    void OnTriggerEnter(Collider TriggerCamera)
    {
        inFrame = true;
        Debug.Log("EnterTrigger");

    }
        }
