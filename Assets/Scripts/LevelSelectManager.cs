using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//manages the start scene and communicates with game manager
public class LevelSelectManager : MonoBehaviour
{
    public Animator startUI;
    public bool moveCam;
    public float speed;
    public Animator finUI;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.lvlNum = 0;
        if (GameManager.startDone)
        {
            startUI.SetTrigger("Go");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.carsDone && GameManager.solarDone && GameManager.windDone && GameManager.captureDone)
        {
            finUI.SetTrigger("Go");
        }
        if (moveCam)
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 5, speed * Time.deltaTime);
        }
        if (GameManager.carsDone && GameManager.solarDone && GameManager.windDone && GameManager.captureDone)
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 26.22f, speed* Time.deltaTime);
        }
    }

    public void SetLvl(int lvl)
    {
        GameManager.lvlNum = lvl;
        GameManager.instance.StartExiting(false);
    }

    public void GoToLvlSelect()
    {
        startUI.SetTrigger("Go");
        moveCam = true;
        GameManager.startDone = true;
    }
}
