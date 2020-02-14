using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayArtName : MonoBehaviour
{

    public TextMeshProUGUI m_nameText;
    private string m_artName;
    private string m_dateTime;

    // Start is called before the first frame update
    void Start()
    {
        m_nameText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        m_nameText.text = m_artName + "\n" + m_dateTime;
    }

    public void SetArtName(TMP_InputField name)
    {
        m_artName = name.text;
        m_dateTime = System.DateTime.Now.ToString("HH:mm MMMM dd, yyyy");
    }
}
