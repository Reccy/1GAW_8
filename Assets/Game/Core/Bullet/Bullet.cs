using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int m_delayFrames = 8;
    private int m_currentDelayFrames;

    private GameObject m_overlappingTile = null;

    public Vector3Int CurrentPosition => new Vector3Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), Mathf.RoundToInt(transform.position.z));

    private void Awake()
    {
        m_currentDelayFrames = m_delayFrames;
    }

    private void FixedUpdate()
    {
        while (m_currentDelayFrames > 0)
        {
            m_currentDelayFrames--;
        }

        if (OverlapsTile((Vector2Int)CurrentPosition))
        {
            Destroy(m_overlappingTile);
            Destroy(gameObject);
            return;
        }

        Move();

        m_currentDelayFrames = m_delayFrames;
    }

    private void Move()
    {
        transform.position += transform.up;
    }

    private bool OverlapsTile(Vector2Int pos)
    {
        Collider2D[] cols = Physics2D.OverlapBoxAll(pos, Vector2.one * 0.5f, 0);

        for (int i = 0; i < cols.Length; ++i)
        {
            Collider2D col = cols[i];
            m_overlappingTile = col.gameObject;
        }

        return cols.Length > 0;
    }
}
