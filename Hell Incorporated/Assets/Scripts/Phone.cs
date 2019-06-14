using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour
{
    [SerializeField]
    private AudioSource m_phoneRingSound;

    public void Ring()
    {
        m_phoneRingSound.Play();
        GetComponent<SequentialDialogue>().CloseDialogue();
    }

    public void OpenDialogue()
    {
        GetComponent<SequentialDialogue>().OpenNextDialogue();
        m_phoneRingSound.Stop();
    }
}
