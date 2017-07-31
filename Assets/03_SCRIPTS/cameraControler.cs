using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControler : MonoBehaviour {
	private GameObject playerCamera;
	private GameObject player;
    private Vector3 CAMERA_RELATIVE_POSITION = new Vector3(0f, 75f, -35f);

    // Use this for initialization
    void Start () {
        playerCamera = GameObject.Find("MainCharacter/Main Camera");
		player = GameObject.Find ("MainCharacter/MainCharacter");
	}

	// Update is called once per frame
	void Update () {
        if (playerCamera && player)
        {
            playerCamera.transform.position = player.transform.position + CAMERA_RELATIVE_POSITION;
        }
	}
}
