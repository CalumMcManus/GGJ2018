using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class DoorManager : NetworkBehaviour {

    List<bool> Doors = new List<bool>();
    int first = -1;
    int second = -1;
    NetworkHandler handler;
    void Awake()
    {
        Doors.Add(false);
        Doors.Add(false);
        Doors.Add(false);
        Doors.Add(false);
        DontDestroyOnLoad(transform.gameObject);
        handler = FindObjectOfType<NetworkHandler>();
    }
    [Command]
    void CmdSendDoorIndex(int index)
    {
  
        RpcDoorIndex(index);
    }
    [ClientRpc]
    void RpcDoorIndex(int index)
    {
        Cool test = FindObjectOfType<Cool>();
        test.MoveCube();
        Debug.Log(handler.playerID);
        if (handler.playerID == 1)
        { return; }
        Debug.Log("Capacity: " + Doors.Capacity);
        Debug.Log("Doors boolean: " + Doors[index]);
        Debug.Log("Doors index: " + index);

        if (index >= Doors.Capacity)
        { return; }
        Debug.Log("Check One");
        if (Doors[index] == true)
        { return; }
        Debug.Log("Check Two");
        if (first < 0)
        {
            first = index;
            Doors[index] = true;
            return;
        }
        Debug.Log("Check Three");
        if (second < 0)
        {
            second = first;
            first = index;
            Doors[index] = true;
            return;
        }
        Debug.Log("Check Four");
        Doors[second] = false;
        second = first;
        first = index;
        Doors[index] = true;
        Debug.Log("End");

    }
    void Update()
    {
        if (!isLocalPlayer || handler.playerID == 2)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CmdSendDoorIndex(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CmdSendDoorIndex(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CmdSendDoorIndex(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            CmdSendDoorIndex(3);
        }
    }
}
