using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightGradient : MonoBehaviour {

    [Range(0,1)] private float steepnessfactor;
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 m_Start;
    [SerializeField] private Vector3 End;
    private Color AmbientLight;
    // Use this for initialization
    void Start ()
    {
        AmbientLight = RenderSettings.ambientLight;
        Vector3 dist = (End - m_Start).normalized;
    }
	
	// Update is called once per frame
	void Update ()
    {
        float dist = Vector3.Distance(player.transform.position, m_Start);
        float b = dist / 100;
        float r = (100 - dist) / 100;
        Color color = new Color(r, 0, b);
        RenderSettings.ambientLight = color;
	}
}
