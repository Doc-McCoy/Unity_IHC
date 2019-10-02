using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    [Header("HUD")]
    public Text m_NameText;
    public Image m_AvatarImage;
    public Text m_SentenceText;

    private AudioSource m_AudioSource;
    private Animator m_Animator;
    private Queue<DialogSentence> m_Sentences = new Queue<DialogSentence>();
    private bool m_IsOpen;

    public void OpenDialogAnimation(bool open)
    {
        m_IsOpen = open;
        if (m_Animator) m_Animator.SetBool("Open", open);
    }

    public void BeginDialog(Dialog dialog)
    {
        if (m_IsOpen) return;

        OpenDialogAnimation(true);
        m_Sentences.Clear();

        if (m_NameText) m_NameText.text = dialog.m_Name;
        if (m_AvatarImage) m_AvatarImage.sprite = dialog.m_Avatar;

        foreach (var sentence in dialog.m_Sentences)
            m_Sentences.Enqueue(sentence);

        NextSentence();
    }

    public void NextSentence()
    {
        if (m_AudioSource) m_AudioSource.Stop();
        if (m_Sentences.Count == 0)
        {
            OpenDialogAnimation(false);
            return;
        }

        var sentence = m_Sentences.Dequeue();

        StopAllCoroutines();
        StartCoroutine(WriteSentence(sentence));
    }

    private IEnumerator WriteSentence(DialogSentence sentence)
    {
        if (m_AudioSource)
        {
            m_AudioSource.clip = sentence.m_Voice;
            m_AudioSource.Play();
        }

        m_SentenceText.text = string.Empty;
        foreach (char letter in sentence.m_Text.ToCharArray())
        {
            while (Time.timeScale == 0) yield return null;
            m_SentenceText.text += letter;
            yield return null;
        }
    }
}

[Serializable]
public class Dialog
{
    public string m_Name;
    public Sprite m_Avatar;
    public List<DialogSentence> m_Sentences;
}

[Serializable]
public class DialogSentence
{
    [TextArea(1,10)]
    public string m_Text;
    public AudioClip m_Voice;
}
