using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    [SerializeField] float      m_jumpForce = 7.5f;
    [SerializeField] float      m_attackSpeed = 1.0f;
    [SerializeField] int        m_Lives = 4;
    [SerializeField] Sprite[]   m_array_sprite;
    [SerializeField] Image      m_canvas_image;

    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private bool                m_grounded = false;
    private int                 m_currentAttack = 0;
    private float               m_timeSinceAttack = 0.0f;
    private int                 m_ColCount = 0;
    private float               m_DisableTimer;
    private List<GameObject>    m_enemiesInRange = new List<GameObject>();

    // Use this for initialization
    void Start ()
    {
        m_ColCount = 0;
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_animator.SetInteger("AnimState", 1);
    }

    // Update is called once per frame
    void Update ()
    {
        // Increase timer that controls attack combo
        m_timeSinceAttack += Time.deltaTime;

        //Jump timer
        m_DisableTimer -= Time.deltaTime;

        //Check if character just landed on the ground
        if (!m_grounded && State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if (m_grounded && !State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

        // -- Handle Animations --

        //Attack
        if(Input.GetButtonDown("Fire1") && m_timeSinceAttack > m_attackSpeed)
            Attack();
        else if (Input.GetButtonUp("Fire1"))
            m_animator.SetBool("IdleBlock", false);
            
        //Jump
        else if (Input.GetButtonDown("Jump") && m_grounded)
        {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            DisableJump(0.2f);
        }
    }

    /// <summary>
    /// Attack
    /// </summary>
    private void Attack()
    {
        m_currentAttack++;

        // Loop back to one after third attack
        if (m_currentAttack > 3)
            m_currentAttack = 1;

        // Reset Attack combo if time since last attack is too large
        if (m_timeSinceAttack > 1.0f)
            m_currentAttack = 1;

        // Call one of three attack animations "Attack1", "Attack2", "Attack3"
        m_animator.SetTrigger("Attack" + m_currentAttack);

        // Reset timer
        m_timeSinceAttack = 0.0f;

        foreach (GameObject enemy in m_enemiesInRange)
        {
            enemy.GetComponent<EnemyController>().Damage();
        }
    }

    /// <summary>
    /// Check if the player is in a state where he can jump
    /// </summary>
    /// <returns></returns>
    private bool State()
    {
        if (m_DisableTimer > 0)
            return false;
        return m_ColCount > 0;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        m_ColCount++;
        if (other.gameObject.tag == "Enemy")
        {
            m_enemiesInRange.Add(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        m_ColCount--;
        if (other.gameObject.tag == "Enemy")
        {
            m_enemiesInRange.Remove(other.gameObject);
        }
    }

    /// <summary>
    /// Disable jump for a certain duration
    /// </summary>
    /// <param name="duration">The duration for which the player can't jump</param>
    public void DisableJump(float duration)
    {
        m_DisableTimer = duration;
    }

    private void OnDestroy()
    {
        GameObject.Find("GameController").GetComponent<PauseManager>().TogglePause();
    }

    /// <summary>
    /// Lose a life when attacked
    /// </summary>
    public void Damage()
    {
        m_Lives--;
        m_animator.SetTrigger("Hurt");
        switch (m_Lives)
        {
            case 3:
                m_canvas_image.sprite = m_array_sprite[m_array_sprite.Length - 1];
                break;
            case 2:
                m_canvas_image.sprite = m_array_sprite[m_array_sprite.Length - 2];
                break;
            case 1:
                m_canvas_image.sprite = m_array_sprite[m_array_sprite.Length - 3];
                break;
            default:
                m_canvas_image.sprite = m_array_sprite[m_array_sprite.Length - 4];
                m_animator.SetTrigger("Death");
                Destroy(gameObject, 2.0f);
                break;
        }
    }

    public int GetLives()
    {
        return m_Lives;
    }
}
