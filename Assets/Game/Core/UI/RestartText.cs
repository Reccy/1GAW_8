using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RestartText : MonoBehaviour
{
    private TMP_Text m_text;
    private PlayerController m_pc;

    private void Awake()
    {
        m_pc = FindObjectOfType<PlayerController>();
        m_text = GetComponentInChildren<TMP_Text>();

        m_text.enabled = false;
    }

    private void Update()
    {
        if (m_pc.IsDead)
        {
            m_text.enabled = true;
        }
    }
}
