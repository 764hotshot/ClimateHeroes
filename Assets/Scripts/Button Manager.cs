using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//unused
public class ButtonManager : MonoBehaviour
{
    public static void LoadLevel(string lvl)
    {
        SceneManager.LoadScene(lvl);
    }
}
