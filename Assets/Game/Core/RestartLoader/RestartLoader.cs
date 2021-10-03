using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartLoader : MonoBehaviour
{
    private PlayerController m_pc;

    private Transform[] m_children;

    private void Awake()
    {
        m_pc = FindObjectOfType<PlayerController>();

        m_children = GetComponentsInChildren<Transform>();
        
        foreach (Transform t in m_children)
        {
            if (t == transform)
                continue;

            t.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (m_pc.IsDead)
        {
            foreach (Transform t in m_children)
            {
                t.gameObject.SetActive(true);
            }

            enabled = false;
        }
    }
}
