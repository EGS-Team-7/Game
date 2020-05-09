using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomScale : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float x = Random.Range(1, 10);
        float y = Random.Range(1, 10);
        gameObject.transform.localScale = new Vector3(x, y, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
