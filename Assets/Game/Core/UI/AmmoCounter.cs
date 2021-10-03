using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoCounter : MonoBehaviour
{
    private TMP_Text m_text;
    private PlayerController m_pc;

    private void Awake()
    {
        m_text = GetComponentInChildren<TMP_Text>();
        m_pc = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        m_text.text = $"{m_pc.Ammo} Bullets Remaining";
    }
}
