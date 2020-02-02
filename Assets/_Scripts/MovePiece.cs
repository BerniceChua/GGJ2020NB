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
    [SerializeField] private KeyCode m_returnToInventory;

    [SerializeField] private bool m_isCorrectPlacement = false;

    [SerializeField] private float m_yDifference;

    [SerializeField] private Vector2 m_inventoryPosition;

    public Sprite m_Stage2Image;

    public static int TotalScore;

    /// <summary>
    /// can comment these out later.
    /// </summary>
    public static float timeBonus = 120.0f;
    ///  ///  ///

    // Start is called before the first frame update
    void Start()
    {
        if (LevelSelect.WhichLevel == 2)
        {
            GetComponent<SpriteRenderer>().sprite = m_Stage2Image;
        }
    }

    // Update is called once per frame
    void Update() {
        //timeBonus -= Time.deltaTime;

        InventoryControl();

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

            GetComponent<Renderer>().sortingOrder = 1;
            transform.position = collision.gameObject.transform.position;
            m_IsPieceLocked = true;
            Instantiate(m_edgeParticles, collision.gameObject.transform.position, m_edgeParticles.rotation);
            m_isCorrectPlacement = false;  /// to reset this value and get out of OnTriggerStay2D.
            TotalScore += 10;

            GameManager.remainingPieces -= 1;
            Debug.Log("GameManager.remainingPieces = " + GameManager.remainingPieces);
        }

        if ((collision.gameObject.name != this.gameObject.name) && (m_isCorrectPlacement == true)) {
            Debug.Log("Collided with " + collision.name + "; try again.");
            /// Play some sound???
            m_isCorrectPlacement = false;  /// to reset this value and get out of OnTriggerStay2D.
            TotalScore += 2;
        }
    }

    private void OnMouseDown() {
        m_IsPickedUp = true;
        m_isCorrectPlacement = false;
        GetComponent<Renderer>().sortingOrder = 10;

        m_inventoryPosition = transform.position;
    }

    private void InventoryControl()
    {
        if ( (Input.GetAxis("Mouse ScrollWheel") > 0) && (m_IsPieceLocked == false) )
        {
            transform.position = new Vector2(7.0f, transform.position.y - 2.4f);

            m_yDifference -= 2.4f;
        }

        if ( (Input.GetAxis("Mouse ScrollWheel") < 0) && (m_IsPieceLocked == false) )
        {
            transform.position = new Vector2(7.0f, transform.position.y + 2.4f);
            m_yDifference += 2.4f;
        }

        if ( (Input.GetKeyDown(m_returnToInventory) && m_IsPickedUp == true) )
        {
            transform.position = new Vector2(7.0f, m_inventoryPosition.y + m_yDifference);
        }
    }
}
