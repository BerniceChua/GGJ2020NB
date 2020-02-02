using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicLayerManager : MonoBehaviour
{
    public AudioSource[] m_MusicLayers;     // 6 audio mixer layers
    private int m_Progress;                      // Goes from 0-5
    private int m_PreviousProgress;             // m_Progress from the previous update frame

    // Start is called before the first frame update
    void Start()
    {
        m_Progress = 1;
        m_PreviousProgress = 5;
    }

    // Update is called once per frame
    void Update()
    {
        RefreshLayers();
    }

    void RefreshLayers()
    {
        if(m_Progress != m_PreviousProgress)
        {
            for(int i = 0; i <= m_Progress; i++)
            {
                m_MusicLayers[i].volume = 1.0f;
            }
            for(int j = m_Progress; j <= 5; j++)
            {
                m_MusicLayers[j].volume = 0.0f;
            }
            m_PreviousProgress = m_Progress;
        }
    }

    public void IncreaseProgress()
    {
        if(m_Progress < 6) m_Progress++;
    }

    public void DecreaseProgress()
    {
        if(m_Progress > 1) m_Progress--;
    }
}
