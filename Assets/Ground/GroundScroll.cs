using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScroll : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position += 5 * Time.deltaTime * Vector3.left;
        if (transform.position.x < -20)
        {
            SpawnFloor();
            DestroyFloor();
        }

    }

    private void SpawnFloor()
    {
        GameObject floor = Instantiate(gameObject);
        floor.transform.position = new Vector3(0, -4.6f, 0);
    }

    private void DestroyFloor()
    {
        Destroy(gameObject);
    }
}
