using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelSelect : MonoBehaviour
{
    public static int WhichLevel;

    [SerializeField] private string scenePath;
    //[SerializeField] private Scene m_level2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (gameObject.name == "Level1_Text (TMP)")
        {
            WhichLevel = 1;
        }

        if (gameObject.name == "Level2_Text (TMP)")
        {
            WhichLevel = 2;
        }

        //SceneManager.LoadScene(scenePath);
        SceneManager.LoadScene("SampleScene");
    }

    public void ChooseLevel()
    {
        if (gameObject.name == "Level1_Text (TMP)")
        {
            WhichLevel = 1;
        }

        if (gameObject.name == "Level2_Text (TMP)")
        {
            WhichLevel = 2;
        }

        //SceneManager.LoadScene(scenePath);
        SceneManager.LoadScene("SampleScene");
    }
}
