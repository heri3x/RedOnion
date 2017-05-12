using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyData Data;

    private Rigidbody2D m_rigidBody;
    private float m_count = 0;
    private bool m_moveLeft = false;

    void Start()
    {
        gameObject.name = Data.Name;
        m_rigidBody = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().color = Data.Color;

        Debug.Log("level: " + GameManager.Instance.Level);
    }

    void Update()
    {
        // 入力
        float h = 1.0f;
        float v = 0.0f;
        if (m_moveLeft)
        {
            h = -h;
        }

        // 移動
        Vector2 direction = new Vector2(h, v);
        m_rigidBody.velocity = direction.normalized * Data.Speed;

        //
        m_count += Mathf.Abs(h);
        if (m_count >= Data.Limit)
        {
            m_moveLeft = !m_moveLeft;
            m_count = 0;
        }
    }
}
