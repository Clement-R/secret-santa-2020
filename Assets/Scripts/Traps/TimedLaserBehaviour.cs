using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class TimedLaserBehaviour : ActivationTrap
{
    [SerializeField] private float m_delayDuration;
    [SerializeField] private float m_offsetDuration;
    [SerializeField] private float m_activeDuration;
    [SerializeField] private float m_previewDuration;
    [SerializeField] private Sprite m_laserSprite;
    [SerializeField] private Sprite m_laserPreviewSprite;
    [SerializeField] private SFX m_laserPreviewSFX;
    [SerializeField] private SFX m_laserActivateSFX;

    [SerializeField] private Vector3 m_endPoint;
    [SerializeField] private Transform m_end;

    private float m_lastActivation = 0f;
    private float m_nextActivation = 0f;
    private List<SpriteRenderer> m_lasers = new List<SpriteRenderer>();
    private List<Collider2D> m_lasersColliders = new List<Collider2D>();

    protected override void Start()
    {
        base.Start();
        Vector3 endPosition = transform.position + (m_endPoint * 16f);

        // Set End position
        m_end.transform.position = endPosition;

        // Create Laser in between
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

        var direction = GetDirection();

        // Draw in between
        Vector3 position = transform.position;
        Transform previous = m_end;
        for (int i = 0; i <= steps; i++)
        {
            var go = new GameObject();
            go.transform.parent = transform;
            go.transform.position = position;
            go.layer = LayerMask.NameToLayer("Trap");
            var sr = go.AddComponent<SpriteRenderer>();
            sr.sortingLayerName = "Environnement";
            sr.sprite = m_laserSprite;
            go.transform.eulerAngles = LookAt2D(go.transform, previous, 90f);

            var col = go.AddComponent<BoxCollider2D>();
            col.size = new Vector2(4f, 22f);
            col.isTrigger = true;
            m_lasersColliders.Add(col);

            m_lasers.Add(sr);

            position += direction * 16f;
            previous = go.transform;
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

    public override void TrapReset()
    {
        base.TrapReset();

        m_lastActivation = 0f;
        m_nextActivation = Time.time + m_offsetDuration;
    }

    void Update()
    {
        if (Time.time >= m_lastActivation + m_activeDuration)
        {
            Deactivate();
        }

        if (Time.time >= m_nextActivation - m_previewDuration)
        {
            PreActivate();
        }

        if (Time.time >= m_nextActivation)
        {
            Activate();
        }
    }

    private void PreActivate()
    {
        m_lasers.ForEach(s => s.sprite = m_laserPreviewSprite);
    }

    protected override void Activate(bool p_silent = false)
    {
        m_lasersColliders.ForEach(c => c.enabled = true);

        m_lasers.ForEach(s => s.sprite = m_laserSprite);

        m_lastActivation = Time.time;
        m_nextActivation = m_lastActivation + m_delayDuration;
    }

    protected override void Deactivate(bool p_silent = false)
    {
        m_lasersColliders.ForEach(c => c.enabled = false);

        m_lasers.ForEach(s => s.sprite = null);
    }

    [SerializeField, HideInInspector] private Vector3 m_startPosition;
    private void OnValidate()
    {
        m_startPosition = Vector3Int.CeilToInt(transform.position);

        var endPosition = m_startPosition + (m_endPoint * 16f);
        m_end.transform.position = endPosition;
    }

    private void OnDrawGizmos()
    {
        var endPosition = m_startPosition + (m_endPoint * 16f);

        Gizmos.color = Color.green;
        Gizmos.DrawCube(endPosition, Vector3.one * 4f);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(m_startPosition, endPosition);
    }
}