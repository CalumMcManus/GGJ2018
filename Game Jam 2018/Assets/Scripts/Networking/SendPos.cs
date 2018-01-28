using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SendPos : NetworkBehaviour {

    private Transform m_PlayerTrans;
    private void Start()
    {
        m_PlayerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        if(isLocalPlayer && FindObjectOfType<NetworkHandler>().playerID == 2)
        {
            StartCoroutine(Send());
        }
    }

    [Command]
    void CmdSendPos(float X, float Z)
    {
        RpcPos(X, Z);
    }
    [ClientRpc]
    void RpcPos(float X, float Z)
    {
        Debug.Log(X + " " + Z);
    }


    private IEnumerator Send()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            CmdSendPos(m_PlayerTrans.position.x, m_PlayerTrans.position.z);
        }
    }
}
