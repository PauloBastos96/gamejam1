using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyScroll : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position += Vector3.left * Time.deltaTime;
        if (transform.position.x < -19.2f)
        {
            SpawnSky();
            DestroySky();
        }

    }

    private void SpawnSky()
    {
        GameObject sky = Instantiate(gameObject);
        sky.transform.position = new Vector3(0, 0, 0);
    }

    private void DestroySky()
    {
        Destroy(gameObject);
    }
}
