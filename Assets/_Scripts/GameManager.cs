using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int remainingPieces = 3;

    [SerializeField] GameObject m_uIConfirmationPanel;

    // Start is called before the first frame update
    void Start()
    {
        
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
