using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameLoopManager : MonoBehaviour {

    [SerializeField] public float m_StartDelay = 3.0f;
    [SerializeField] public float m_EndDelay = 3.0f;
    [SerializeField] public float m_DelayEndMessage = 3.0f;

    [SerializeField] private GameObject m_confirmationPanel { get { return m_confirmationPanel; } }

    [SerializeField] private Button n_nameItButton;

    public bool m_IsGameCompleted = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GameLoop());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator GameLoop() {
        Debug.Log("I'm inside IEnumerator GameLoop()!");

        yield return StartCoroutine(RepairState());

        yield return StartCoroutine(ConfirmationState());

        yield return StartCoroutine(EndState());

    }

    /// <summary>
    /// The jigsaw puzzle state.
    /// </summary>
    /// <returns></returns>
    private IEnumerator RepairState() {
        /// Put the contents of the tutorial here

        ShuffleTheJigsaw();



        Debug.Log("I'm inside IEnumerator RepairState()!");
        yield return m_StartDelay;
    }

    /// <summary>
    /// When the puzzle is solved, confirm this to the player by showing the m_confirmationPanel.
    /// </summary>
    /// <returns></returns>
    private IEnumerator ConfirmationState() {
        Debug.Log("I'm inside IEnumerator ConfirmationState()!");

        if (m_IsGameCompleted) {
            m_confirmationPanel.SetActive(true);
            yield return StartCoroutine(EndState());
        } else {
            yield return m_EndDelay;
        }
    }

    private IEnumerator EndState() {
        Debug.Log("I'm inside IEnumerator EndState()!");
        
        //if (n_nameItButton.onClick)
        yield return m_DelayEndMessage;
    }

    private void ShuffleTheJigsaw() {

    }

    /// <summary>
    /// Allow the player to move the jigsaw puzzle 
    /// </summary>
    private void EnablePlayerControl()
    {
        
    }


}
