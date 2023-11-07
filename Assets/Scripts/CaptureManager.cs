using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//manager script for carbon capture game
public class CaptureManager : MonoBehaviour
{
    public List<GameObject> objs;
    public bool done;
    public Animator info;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckCollected() && !done)
        {
            done = true;
            GameManager.instance.StartExiting(true);
            GameManager.captureDone = true;
            info.SetTrigger("Go");
        }
    }

    bool CheckCollected()
    {
        foreach (var item in objs)
        {
            if (item.activeSelf)
            {
                return false;
            }
        }

        return true;
    }
}
