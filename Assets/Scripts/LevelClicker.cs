using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//script to manage clickable events for in scene lvl select buttons
public class LevelClicker : MonoBehaviour, IPointerClickHandler
{
    public int lvl;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.lvlNum = lvl;
        GameManager.instance.StartExiting(false);
    }
}
