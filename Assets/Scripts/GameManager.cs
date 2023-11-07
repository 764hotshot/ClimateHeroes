using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//manager for whole game and deals with transitions
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<Sprite> peopleIBSprites;
    public List<Sprite> backgroundIBSprites;
    public static int lvlNum; // NOT INDEXED START AT 1
    public DialougeManager dm;
    public bool exiting;
    public GameObject ExitArrow;
    public GameObject Confetti;
    public GameObject Transition;
    public GameObject p;
    public GameObject b;
    public static bool windDone;
    public static bool solarDone;
    public static bool carsDone;
    public static bool captureDone;
    public bool transitioning;
    public static bool startDone;
    private void Awake()
    {
        instance = this;
        //if (instance != this)
        //{
        //    if (instance == null)
        //    {
        //        instance = this;
        //    }
        //    else
        //    {
        //        Destroy(gameObject);
        //    }
        //}
        //DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        transitioning = false;
        //Transition = GameObject.FindGameObjectWithTag("Transition");
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            p.GetComponent<UnityEngine.UI.Image>().sprite = peopleIBSprites[lvlNum - 1];
            b.GetComponent<UnityEngine.UI.Image>().sprite = backgroundIBSprites[lvlNum - 1];
            //dm = FindObjectOfType<DialougeManager>();
            dm.conversationIndex = lvlNum-1;
            dm.StartConversation();
        }
        if (SceneManager.GetActiveScene().buildIndex == 0 && startDone)
        {
            Camera.main.orthographicSize = 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            windDone = true;
        }
        if (Input.GetKey(KeyCode.W))
        {
            solarDone = true;
        }
        if (Input.GetKey(KeyCode.E))
        {
            carsDone = true;
        }
        if (Input.GetKey(KeyCode.R))
        {
            captureDone = true;
        }
    }

    public void StartExiting(bool confettiActive)
    {
        if (confettiActive)
            Instantiate(Confetti).transform.position += Camera.main.transform.position - new Vector3(0, 0, -10);
        var r = Instantiate(ExitArrow, FindObjectOfType<Canvas>().transform);
        r.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate { StartCoroutine(Exit()); });
        r.transform.SetSiblingIndex(r.transform.parent.childCount - 2);
    }

    public IEnumerator Exit()
    {
        if (!transitioning)
        {

            transitioning = true;
            Transition.GetComponent<Animator>().SetTrigger("Go");
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex < 1 ? SceneManager.GetActiveScene().buildIndex + 1 : 1); //FIXXXXXXXXXX
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                SceneManager.LoadScene(1);
            }
            if (SceneManager.GetActiveScene().buildIndex > 1)
            {
                SceneManager.LoadScene(0);
            }
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                if (lvlNum == 1 || lvlNum == 2)
                {
                    SceneManager.LoadScene(2);
                }
                else
                {
                    SceneManager.LoadScene(lvlNum);
                }
            }
        }
    }
}
