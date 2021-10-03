using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    private PlayerController m_pc;
    [SerializeField] private float m_speed = 2.0f;

    private void Awake()
    {
        m_pc = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, m_pc.transform.position, Time.deltaTime * m_speed);
    }
}
