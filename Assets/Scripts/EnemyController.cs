using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] int        m_Lives = 4;

    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private int                 m_currentAttack = 0;
    private float               m_timeSinceAttack = 0.0f;
    private GameObject          m_player;

    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.FindWithTag("Player");
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_animator.SetInteger("AnimState", 1);
        gameObject.GetComponent<SpriteRenderer>().flipX = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, m_player.transform.position) < 1.5f)
        {
            m_animator.SetTrigger("Attack");
        }

        if (m_Lives <= 0)
        {
            m_animator.SetTrigger("Death");
            Destroy(gameObject, 1f);
        }
    }
}
