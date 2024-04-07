using UnityEngine;

public class GroundScroll : MonoBehaviour
{
    [SerializeField] float m_scrollSpeed = 5;

    // Update is called once per frame
    private void Update()
    {
        transform.position += m_scrollSpeed * Time.deltaTime * Vector3.left;
        if (transform.position.x < -20)
        {
            transform.position = new Vector3(0, -4.6f, 0);
        }
    }
}
