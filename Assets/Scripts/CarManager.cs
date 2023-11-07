using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//manager script for transportation game
public class CarManager : MonoBehaviour
{
    public CarScript[] cars;
    public bool done;
    public Animator info;

    // Start is called before the first frame update
    void Start()
    {
        cars = FindObjectsOfType<CarScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckCollected() && !done)
        {
            done = true;
            GameManager.instance.StartExiting(true);
            GameManager.carsDone = true;
            info.SetTrigger("Go");
        }
    }

    bool CheckCollected()
    {
        foreach (var item in cars)
        {
            if (!item.done)
            {
                return false;
            }
        }

        return true;
    }
}
