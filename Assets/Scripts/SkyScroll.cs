using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyScroll : MonoBehaviour
{
    [SerializeField] float m_ScrollSpeed = 1.0f;
    [SerializeField] float m_SpawnPoint = -19f;

    // Update is called once per frame
    private void Update()
    {
        transform.position += m_ScrollSpeed * Vector3.left * Time.deltaTime;
        if (transform.position.x < m_SpawnPoint)
        {
            gameObject.transform.position = new Vector3(0, 0, 1);
        }
    }
}
