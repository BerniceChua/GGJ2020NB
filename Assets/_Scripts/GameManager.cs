using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int remainingPieces;

    [SerializeField] GameObject m_uIConfirmationPanel;

    [SerializeField] private GameObject m_jigsawContainer;

    [SerializeField] private Transform[] m_jigsawPieces;

    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_jigsawPieces = m_jigsawContainer.GetComponentsInChildren<Transform>();

        for (int i = 0; i < m_jigsawContainer.transform.childCount; i++)
        {
            m_jigsawPieces[i] = m_jigsawContainer.transform.GetChild(i);
        }

        remainingPieces = m_jigsawContainer.transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (remainingPieces > 0)
        {
            MovePiece.timeBonus -= Time.deltaTime;
        }

        if (remainingPieces == 0)
        {
            m_uIConfirmationPanel.SetActive(true);
        }
    }
}
