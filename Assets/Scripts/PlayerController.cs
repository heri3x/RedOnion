using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Custom/PlayerController")]
public class PlayerController : MonoBehaviour
{
    public float Speed = 8;

    private Rigidbody2D m_rigidBody;

    [ContextMenu("Rotate Right")]
    void RotateRight()
    {
        gameObject.transform.Rotate(new Vector3(0, 0, 45));
    }

    [ContextMenu("Rotate Left")]
    void RotateLeft()
    {
        gameObject.transform.Rotate(new Vector3(0, 0, -45));
    }


    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 入力
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // 移動
        Vector2 direction = new Vector2(h, v);
        m_rigidBody.velocity = direction.normalized * Speed;
    }
}
