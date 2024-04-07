using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int    m_Lives = 4;
    [SerializeField] private float  m_speed = 1.0f;
    [SerializeField] private float  m_attackDelay = 1.0f;
    [SerializeField] private float  m_attackSpeed = 1.0f;
    [SerializeField] private float  m_attackRange = 1.5f;

    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private GameObject          m_player;
    private bool                m_isAttacking = false;
    private float               m_currentAttackDelay = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.Find("Player");
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_animator.SetInteger("AnimState", 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -50)
            Destroy(gameObject);
        if (Vector3.Distance(transform.position, m_player.transform.position) > m_attackRange)
        {
            m_body2d.AddForce(Vector3.left * m_speed, ForceMode2D.Force);
        }
        else
            m_animator.SetInteger("AnimState", 0);
        if (m_isAttacking)
        {
            m_currentAttackDelay -= Time.deltaTime;
            if (m_currentAttackDelay <= 0)
            {
                m_currentAttackDelay = m_attackDelay;
                m_isAttacking = false;
            }
        }
        if (CanAttack())
        {
            m_currentAttackDelay -= Time.deltaTime;
            if (m_currentAttackDelay <= 0)
            {
                m_currentAttackDelay = m_attackDelay;
                if (m_player.GetComponent<PlayerController>().GetLives() > 0)
                {
                    m_animator.SetTrigger("Attack");
                    m_isAttacking = true;
                    m_player.GetComponent<PlayerController>().Damage();
                }
            }
        }
        if (m_Lives <= 0)
        {
            m_animator.SetTrigger("Death");
            Destroy(gameObject, 2f);
        }
    }

    public void Damage()
    {
        m_Lives--;
        m_animator.SetTrigger("Hurt");
    }

    private bool CanAttack()
    {
        if (Vector3.Distance(transform.position, m_player.transform.position) < m_attackRange && !m_isAttacking && m_Lives > 0)
            return true;
        return false;
    }
}
