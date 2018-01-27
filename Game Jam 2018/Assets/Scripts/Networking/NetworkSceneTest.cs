using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NetworkSceneTest : MonoBehaviour {

    // called third
    void Start()
    {
        Debug.Log("Start");
        NetworkHandler handler = FindObjectOfType<NetworkHandler>();
        if (handler)
        {
            Debug.Log(handler.playerID);
            if(handler.playerID == 1)
            {
                SceneManager.LoadScene("ControlRoom", LoadSceneMode.Single);
            }
            else if(handler.playerID == 2)
            {
                SceneManager.LoadScene("Maze", LoadSceneMode.Single);
            }
        }

        
    }
}
