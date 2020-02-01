using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attach to each jigsaw puzzle piece.
/// </summary>
public class MovePiece : MonoBehaviour {

    public bool m_IsPickedUp = false;
    public bool m_IsPieceLocked = false;

    [SerializeField] private Transform m_edgeParticles;

    [SerializeField] private KeyCode m_placePiece;

    [SerializeField] private bool m_isCorrectPlacement = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        if (m_IsPickedUp == true && m_IsPieceLocked == false) {
            /// Transfers the mouse motion to the jigsaw puzzle piece.
            Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 objPos = Camera.main.ScreenToWorldPoint(mousePos);
            transform.position = objPos;
        }

        if (Input.GetKeyDown(m_placePiece) && m_IsPickedUp == true) {
            m_isCorrectPlacement = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        //if (collision.gameObject.name == gameObject.name + "_Socket") { 
        //if (collision.gameObject.name + "_Socket" == this.gameObject.name) { 
        /// using this as a string these 2 ways didn't work when I tried it.
        if ( (collision.gameObject.name == this.gameObject.name) && (m_isCorrectPlacement == true) ) {
            Debug.Log("(collision.gameObject.name == this.gameObject.name) && (m_isCorrectPlacement == true)");
            
            /// These 2 lines stop OnTriggerStay2D from persisting the collision even when it's not visible onscreen.
            //collision.GetComponent<BoxCollider2D>().enabled = false;
            //GetComponent<BoxCollider2D>().enabled = false;
            ///

            GetComponent<Renderer>().sortingOrder = 0;
            transform.position = collision.gameObject.transform.position;
            m_IsPieceLocked = true;
            Instantiate(m_edgeParticles, collision.gameObject.transform.position, m_edgeParticles.rotation);
            m_isCorrectPlacement = false;  /// to reset this value and get out of OnTriggerStay2D.
        }
        else {
            Debug.Log("Collided with " + collision.name + "; try again.");
            /// Play some sound???
            m_isCorrectPlacement = false;  /// to reset this value and get out of OnTriggerStay2D.
        }
    }

    private void OnMouseDown() {
        m_IsPickedUp = true;
        m_isCorrectPlacement = false;
        GetComponent<Renderer>().sortingOrder = 10;
    }
}
