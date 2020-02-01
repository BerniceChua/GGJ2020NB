using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndStateLoader : MonoBehaviour {

    [SerializeField] private GameObject m_ConfirmationPanel { get { return m_ConfirmationPanel; } }

    public bool m_IsGameCompleted = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_IsGameCompleted)
        {
            m_ConfirmationPanel.SetActive(true);
        }
    }
}
