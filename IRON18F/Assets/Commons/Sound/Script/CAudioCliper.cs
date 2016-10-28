using UnityEngine;
using System.Collections;

public class CAudioCliper : MonoBehaviour 
{
    protected AudioSource m_audio =null;
    
    protected virtual void CreatSource()
    {
        if (m_audio == null)
        {
            gameObject.AddComponent<AudioSource>();
            m_audio = GetComponent<AudioSource>();
        }
    }
	// Use this for initialization
    void Start()
    {
        CreatSource();   
    }

    public void PlayAudio(AudioClip clip)
    {
        CreatSource();
        
        if (m_audio.isPlaying)
            return;

        m_audio.loop = false;
        m_audio.clip = clip;
        m_audio.Play();
    }
    
}
