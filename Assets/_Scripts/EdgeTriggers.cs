using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeTriggers : MonoBehaviour
{
    public Tiler tile;
    public string Void;

    bool spawn = true;
    public bool next = false;

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 12)
        {
            next = true;
            spawn = true;
        }
    }

    private void Update()
    {
        if(next == true)
        {
            //if (Void == "Center")
            //tile.Center();
            if (tile.enabled == true)
            {
                if (spawn == true)
                {
                    if (Void == "Up")
                        tile.Up();
                    if (Void == "Down")
                        tile.Down();
                    if (Void == "Left")
                        tile.Left();
                    if (Void == "Right")
                        tile.Right();
                    if (Void == "UpL")
                        tile.UpL();
                    if (Void == "UpR")
                        tile.UpR();
                    if (Void == "DownL")
                        tile.DownL();
                    if (Void == "DownR")
                        tile.DownR();
                    spawn = false;
                    next = false;
                }
            }
        }
    }

    /*
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 11)
        {
            if (Void == "Center")
            {
                tile.enabled = false;
            }
        }
    }
    */
}
