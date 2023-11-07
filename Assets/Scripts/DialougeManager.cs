using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//manages dialouge for transition scene
public class DialougeManager : MonoBehaviour
{
    public List<Conversation> conversations;
    public int conversationIndex;
    public int lineIndex;
    public float textSpeedDelay;
    public GameObject dialougeObject;
    public TextMeshProUGUI textBox;
    public bool inConversation;
    public bool speaking;
    public bool skipSpeaking;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (speaking)
            {
                skipSpeaking = true;
            }
            else
            {
                if (inConversation)
                {
                    NextLine();
                }
            }
        }
    }

    public void StartConversation()
    {
        if (inConversation)
        {
            return;
        }
        inConversation = true;
        dialougeObject.SetActive(true); // maybe animation
        lineIndex = 0;
        NextLine();
    }

    public void NextLine()
    {
        if (lineIndex == conversations[conversationIndex].conversationLines.Count)
        {
            EndConversation();
            return;
        }
        if (speaking == false)
        {
            StartCoroutine(SayLine(conversations[conversationIndex].conversationLines[lineIndex].text));
        }
    }

    public IEnumerator SayLine(string text)
    {
        speaking = true;
        int characterIndex = 0;
        textBox.text = "";
        while (characterIndex < text.Length)
        {
            textBox.text += text.Substring(characterIndex, 1);
            characterIndex++;
            yield return new WaitForSeconds(textSpeedDelay);
            if (skipSpeaking)
            {
                skipSpeaking = false;
                break;
            }
        }
        skipSpeaking = false;
        textBox.text = text;
        speaking = false;
        lineIndex++;
    }

    public void EndConversation()
    {
        inConversation = false;
        dialougeObject.SetActive(false); // maybe animation
        conversationIndex++;
        GameManager.instance.StartExiting(false);
    }
}
