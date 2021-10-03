using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Range(1, 16)] [SerializeField] private int m_delayFrames = 8;
    private int m_currentDelayFrames;

    private GameObject m_overlappingWall = null;

    [SerializeField] private GameObject m_unstableTilePrefab = null;

    public Vector2Int CurrentPosition => new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
    public Vector2Int UpPosition => CurrentPosition + Vector2Int.up;
    public Vector2Int DownPosition => CurrentPosition + Vector2Int.down;
    public Vector2Int LeftPosition => CurrentPosition + Vector2Int.left;
    public Vector2Int RightPosition => CurrentPosition + Vector2Int.right;

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

        if (OverlapsWall(CurrentPosition))
        {
            Destroy(m_overlappingWall);
            Destroy(gameObject);

            CreateUnstableTile(CurrentPosition);
            CreateUnstableTile(UpPosition);
            CreateUnstableTile(DownPosition);
            CreateUnstableTile(LeftPosition);
            CreateUnstableTile(RightPosition);

            return;
        }

        Move();

        m_currentDelayFrames = m_delayFrames;
    }

    private void Move()
    {
        transform.position += transform.up;
    }

    private bool OverlapsWall(Vector2Int pos)
    {
        Collider2D[] cols = Physics2D.OverlapBoxAll(pos, Vector2.one * 0.5f, 0);

        for (int i = 0; i < cols.Length; ++i)
        {
            Collider2D col = cols[i];

            if (col.GetComponentInChildren<Wall>())
            {
                m_overlappingWall = col.gameObject;
                return true;
            }
        }

        return false;
    }

    private bool OverlapsUnstableTile(Vector2Int pos)
    {
        Collider2D[] cols = Physics2D.OverlapBoxAll(pos, Vector2.one * 0.5f, 0);

        for (int i = 0; i < cols.Length; ++i)
        {
            Collider2D col = cols[i];

            if (col.GetComponentInChildren<UnstableTile>())
            {
                m_overlappingWall = col.gameObject;
                return true;
            }
        }

        return false;
    }

    private void CreateUnstableTile(Vector2Int pos)
    {
        if (OverlapsWall(pos) || OverlapsUnstableTile(pos))
            return;

        Instantiate(m_unstableTilePrefab, new Vector3(pos.x, pos.y, 0), Quaternion.identity);
    }
}
