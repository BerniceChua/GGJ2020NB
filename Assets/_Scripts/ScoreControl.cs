using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreControl : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI m_scoreText;

    // Start is called before the first frame update
    void Start()
    {
        //m_scoreText = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        m_scoreText.text = "Score: " + Mathf.RoundToInt(MovePiece.TotalScore + MovePiece.timeBonus).ToString();
    }
}
