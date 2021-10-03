using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level1Text : MonoBehaviour
{
    private TMP_Text m_text;
    private PlayerController m_pc;

    private Vector3Int m_initialPosition;

    private enum State { START, MOVED, PICKUP_AMMO, FIRED_GUN, DEAD }
    private State m_state = State.START;

    private void Awake()
    {
        m_text = GetComponentInChildren<TMP_Text>();
        m_pc = FindObjectOfType<PlayerController>();
    }

    private void Start()
    {
        m_initialPosition = m_pc.CurrentTilePos;
    }

    private void Update()
    {
        if (m_state == State.START)
        {
            if (m_pc.CurrentTilePos != m_initialPosition)
            {
                m_text.text = "Pickup the ammo";

                m_state = State.MOVED;
            }

        }

        if (m_state == State.MOVED)
        {
            if (m_pc.Ammo > 0)
            {
                m_text.text = "Use IJKL or ABXY to fire your gun";

                m_state = State.PICKUP_AMMO;
            }
        }

        if (m_state == State.PICKUP_AMMO)
        {
            if (m_pc.Ammo == 0)
            {
                m_text.text = "Go through the wall and to the goal";

                m_state = State.FIRED_GUN;
            }
        }

        if (m_state == State.FIRED_GUN)
        {
            if (m_pc.IsDead)
            {
                m_text.text = "Be careful!\n\nWalking over unstable floors will cause them to collapse.";

                m_state = State.DEAD;
            }
        }
    }
}
