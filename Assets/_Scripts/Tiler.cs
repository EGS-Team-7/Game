using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiler : MonoBehaviour
{
    public GameObject backGround;
    public bool tiling = false;

    public GameObject current;

    private void Start()
    {
        tiling = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 12)
        {
            current = other.gameObject;
            tiling = true;
        }
    }

    public void Up()
    {
        Instantiate(backGround, new Vector3(current.transform.position.x, current.transform.position.y + 100, 100), Quaternion.identity);
    }

    public void Down()
    {
        Instantiate(backGround, new Vector3(current.transform.position.x, current.transform.position.y - 100, 100), Quaternion.identity);
    }

    public void Left()
    {
        Instantiate(backGround, new Vector3(current.transform.position.x - 100, current.transform.position.y, 100), Quaternion.identity);
    }

    public void Right()
    {
        Instantiate(backGround, new Vector3(current.transform.position.x + 100, current.transform.position.y, 100), Quaternion.identity);
    }

    public void UpL()
    {
        Instantiate(backGround, new Vector3(current.transform.position.x - 100, current.transform.position.y + 100, 100), Quaternion.identity);
    }

    public void UpR()
    {
        Instantiate(backGround, new Vector3(current.transform.position.x + 100, current.transform.position.y + 100, 100), Quaternion.identity);
    }

    public void DownL()
    {
        Instantiate(backGround, new Vector3(current.transform.position.x - 100, current.transform.position.y - 100, 100), Quaternion.identity);
    }

    public void DownR()
    {
        Instantiate(backGround, new Vector3(current.transform.position.x + 100, current.transform.position.y - 100, 100), Quaternion.identity);
    }

}
