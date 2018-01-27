using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NetworkUI : MonoBehaviour {

    [Header("Network Managers")]
    [SerializeField] private NetworkHandler m_NetworkHandle;
    [Header("Create Game")]
    [SerializeField] private InputField m_GameName; 
    [Header("List Games")]
    [SerializeField] private RectTransform m_GameList;
    [SerializeField] private Button m_JoinGameBtn;
	public void CreateGame()
    {
        m_NetworkHandle.CreateMatch(m_GameName.text);
    }

    public void GetMatches()
    {
        for(int index = 0; index < m_GameList.childCount; index++)
        {
            Destroy(m_GameList.GetChild(index).gameObject);
        }
        List<string> gameNames = m_NetworkHandle.GetMatchList();
        int matchIndex = 0;
        foreach(string name in gameNames)
        {
            Button btn = Instantiate(m_JoinGameBtn, m_GameList) as Button;
            btn.transform.GetChild(0).GetComponent<Text>().text = name;
            //btn.onClick.AddListener()
            matchIndex++;

        }
    }

    public void JoinGame()
    {
       // m_NetworkHandle.JoinMatch()
    }

}
