using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
 
public class NetworkHandler : NetworkManager
{
    private List<MatchInfoSnapshot> m_Matches = new List<MatchInfoSnapshot>();
    private void Awake()
    {
        StartMatchMaker();
        SetMatchHost("mm.unet.unity3d.com", 443, true);
    }

    public void CreateMatch(string name)
    {
        matchMaker.CreateMatch(name, matchSize, true, "", "", "", 0, 0, OnMatchCreate);
    }

    public override void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        base.OnMatchCreate(success, extendedInfo, matchInfo);
        if (!success)
            Debug.Log("NetworkHandler: OnMatchCreate: Failed to create match" + extendedInfo);

    }

    public void JoinMatch(int index)
    {
        matchMaker.JoinMatch(matches[index].networkId, "", "", "", 0, 0, OnMatchJoined);
    }

    public override void OnMatchJoined(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        base.OnMatchJoined(success, extendedInfo, matchInfo);
        if (!success)
            Debug.Log("NetworkHandler: OnMatchJoined: Failed to join match " + extendedInfo);
        else
            Debug.Log("NetworkHandler: OnMatchJoined: Successfully joined match " + matchInfo.networkId);
    }

    public List<string> GetMatchList()
    {
        matchMaker.ListMatches(0, 20, "", true, 0, 0, OnMatchList);
        List<string> matchNames = new List<string>();
        foreach (MatchInfoSnapshot mis in m_Matches)
        {
            matchNames.Add(mis.name);
        }
        return matchNames;
    }
    public override void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList)
    {
        base.OnMatchList(success, extendedInfo, matchList);
        m_Matches.Clear();
        m_Matches.AddRange(matchList);
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
        Destroy(gameObject);
        //If the host leaves the scene will automatically change to the menu. Before this happens we need to clean up objects that don't destroy on load to stop conflicts.
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        base.OnServerDisconnect(conn);

        //Handle the host leaving
    }

}