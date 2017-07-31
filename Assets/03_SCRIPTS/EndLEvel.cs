using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLEvel : MonoBehaviour {

    void start()
    {
    }
    
	void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene("DevEndLevel");
    }
}
