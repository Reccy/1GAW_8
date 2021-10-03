using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    private PlayerController m_pc;
    [SerializeField] private float m_speed = 2.0f;
    [SerializeField] private Sprite m_sadPlayerSprite;

    private SpriteRenderer m_renderer;

    private void Awake()
    {
        m_pc = FindObjectOfType<PlayerController>();
        m_renderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, m_pc.transform.position, Time.deltaTime * m_speed);

        if (m_pc.IsDead)
        {
            m_renderer.sprite = m_sadPlayerSprite;
        }
    }
}
