using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using DG.Tweening;

public class Saw : MonoBehaviour
{
    [SerializeField] private float m_rotationSpeed = 1f;
    [SerializeField] private float m_speed = 5f;

    [SerializeField] private Vector3 m_endPoint;
    [SerializeField, HideInInspector] private Vector3 m_startPosition;

    [SerializeField] private Sprite m_spriteBorder;
    [SerializeField] private Sprite m_spriteFill;
    [SerializeField] private LineRenderer m_line;

    private float m_tileSize = 16f;

    void Start()
    {
        Vector3 endPosition = transform.position + (m_endPoint * m_tileSize);

        var distance = Vector3.Distance(m_startPosition, endPosition);
        var travelTime = distance / m_speed;
        transform.DOMove(endPosition, travelTime, true).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);

        // Create Sprites
        float steps;
        if (m_endPoint.x != 0 && m_endPoint.y != 0)
        {
            steps = Mathf.Abs(m_endPoint.x);
        }
        else if (m_endPoint.x != 0)
        {
            steps = Mathf.Abs(m_endPoint.x);
        }
        else
        {
            steps = Mathf.Abs(m_endPoint.y);
        }
        steps -= 2;

        var direction = GetDirection();

        // Draw start and end
        var startGo = new GameObject();
        startGo.transform.position = m_startPosition;
        startGo.AddComponent<SpriteRenderer>().sprite = m_spriteBorder;

        var endGo = new GameObject();
        endGo.transform.position = endPosition;
        endGo.AddComponent<SpriteRenderer>().sprite = m_spriteBorder;

        startGo.transform.eulerAngles = LookAt2D(startGo.transform, endGo.transform, 180f);
        endGo.transform.eulerAngles = LookAt2D(endGo.transform, startGo.transform, 0f);

        // Draw in between
        Vector3 position = m_startPosition;
        for (int i = 0; i <= steps; i++)
        {
            position += direction * m_tileSize;

            var go = new GameObject();
            go.transform.position = position;
            go.AddComponent<SpriteRenderer>().sprite = m_spriteFill;
            go.transform.eulerAngles = LookAt2D(go.transform, startGo.transform, 0f);
        }
    }

    private Vector3 LookAt2D(Transform p_from, Transform p_to, float p_offset)
    {
        Vector2 direction = new Vector2(p_to.transform.position.x - p_from.transform.position.x, p_to.transform.position.y - p_from.transform.position.y);
        float rotation = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) + p_offset;
        return new Vector3(0, 0, rotation);
    }

    private Vector3 GetDirection()
    {
        var direction = new Vector2Int();

        if (m_endPoint.x != 0)
        {
            direction += Vector2Int.right * (int) Mathf.Sign(m_endPoint.x);
        }

        if (m_endPoint.y != 0)
        {
            direction += Vector2Int.up * (int) Mathf.Sign(m_endPoint.y);
        }

        return new Vector3(direction.x, direction.y, 0f);
    }

    void Update()
    {
        transform.Rotate(0f, 0f, 45f * Time.deltaTime * m_rotationSpeed);
    }

    private void OnValidate()
    {
        m_startPosition = Vector3Int.CeilToInt(transform.position);
    }

    private void OnDrawGizmos()
    {
        var endPosition = m_startPosition + (m_endPoint * m_tileSize);

        Gizmos.color = Color.green;
        Gizmos.DrawCube(endPosition, Vector3.one * 4f);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(m_startPosition, endPosition);
    }
}