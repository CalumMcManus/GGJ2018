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

    private List<int> m_MatchIndexs = new List<int>();
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
        m_MatchIndexs.Clear();
        foreach(string name in gameNames)
        {
            Button btn = Instantiate(m_JoinGameBtn, m_GameList) as Button;
            btn.transform.GetChild(0).GetComponent<Text>().text = name;
            m_MatchIndexs.Add(matchIndex);
            Debug.Log(m_MatchIndexs[matchIndex]);
            btn.onClick.AddListener(delegate { JoinGame(0); });
            matchIndex++;

        }
    }

    public void JoinGame(int index)
    {
        Debug.Log(index);
        m_NetworkHandle.JoinMatch(index);
    }

}
