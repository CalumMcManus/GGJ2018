using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class HostGame : MonoBehaviour
{
    List<MatchInfoSnapshot> matchList = new List<MatchInfoSnapshot>();
    NetworkMatch networkMatch;
    NetworkClient myClient;

    void Awake()
    {
        networkMatch = gameObject.AddComponent<NetworkMatch>();
        DontDestroyOnLoad(gameObject);
    }

    public void CreateSession(string name, string password)
    {
        string matchName = name;
        uint matchSize = 4;
        bool matchAdvertise = true;
        string matchPassword = password;

        networkMatch.CreateMatch(matchName, matchSize, matchAdvertise, matchPassword, "", "", 0, 0, OnMatchCreate);
    }

    public void JoinSession(int index)
    {
        networkMatch.JoinMatch(matchList[index].networkId, "", "", "", 0, 0, OnMatchJoined);
    }

    public List<string> ListMatches()
    {
        networkMatch.ListMatches(0, 20, "", true, 0, 0, OnMatchList);
        List<string> SessionNames = new List<string>();
        foreach (MatchInfoSnapshot mis in matchList)
        {
            SessionNames.Add(mis.name);
        }
        return SessionNames;
    }

    void OnGUI()
    {
        // You would normally not join a match you created yourself but this is possible here for demonstration purposes.
        if (GUILayout.Button("Create Room"))
        {
            string matchName = "room";
            uint matchSize = 4;
            bool matchAdvertise = true;
            string matchPassword = "";

            networkMatch.CreateMatch(matchName, matchSize, matchAdvertise, matchPassword, "", "", 0, 0, OnMatchCreate);
        }

        if (GUILayout.Button("List rooms"))
        {
            networkMatch.ListMatches(0, 20, "", true, 0, 0, OnMatchList);
        }

        if (matchList.Count > 0)
        {
            GUILayout.Label("Current rooms");
        }
        foreach (var match in matchList)
        {
            if (GUILayout.Button(match.name))
            {
                networkMatch.JoinMatch(match.networkId, "", "", "", 0, 0, OnMatchJoined);
            }
        }
    }

    public void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        if (success)
        {
            Debug.Log("Create match succeeded");
            NetworkServer.Listen(matchInfo, 9000);
            Utility.SetAccessTokenForNetwork(matchInfo.networkId, matchInfo.accessToken);
            networkMatch.JoinMatch(matchInfo.networkId, "", "", "", 0, 0, OnMatchJoined);
        }
        else
        {
            Debug.LogError("Create match failed: " + extendedInfo);
        }
    }

    public void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
    {
        if (success && matches != null && matches.Count > 0)
        {
            //Refresh list 
            matchList.Clear();
            matchList.AddRange(matches);
        }
        else if (!success)
        {
            Debug.LogError("List match failed: " + extendedInfo);
        }
    }

    public void OnMatchJoined(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        if (success)
        {
            Debug.Log("Join match succeeded");
            Utility.SetAccessTokenForNetwork(matchInfo.networkId, matchInfo.accessToken);
            myClient = new NetworkClient();
            myClient.RegisterHandler(MsgType.Connect, OnConnected);

            myClient.Connect(matchInfo);
        }
        else
        {
            Debug.LogError("Join match failed " + extendedInfo);
        }
    }

    public void OnConnected(NetworkMessage msg)
    {
        Debug.Log("Connected!");


    }
}