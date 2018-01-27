using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class NetworkTest : NetworkBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    [Command]
    void CmdSendMessage()
    {
        RpcMessage();
    }
    [ClientRpc]
    void RpcMessage()
    {
        Debug.Log("Space pressed");
    }
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdSendMessage();
        }
    }
	
}
