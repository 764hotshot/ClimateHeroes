using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//manager script for both puzzle lvls that communicates with game manager
public class PuzzlePieceManager : MonoBehaviour
{
    public float GridSize;
    public PuzzlePiece[] puzzlePieces;
    public bool done;
    public GameObject windPieces;
    public GameObject solarPieces;
    public Animator info1;
    public Animator info2;

    // Start is called before the first frame update
    void Start()
    {
        PuzzlePiece.occupiedSpaces = new Dictionary<Vector2, GameObject>();
        (GameManager.lvlNum == 1 ? windPieces : solarPieces).SetActive(true);
        puzzlePieces = FindObjectsByType<PuzzlePiece>(FindObjectsSortMode.None);
        List<Vector2> possibleSpaces = new List<Vector2>();
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                possibleSpaces.Add(new Vector2(i, -j));
            }
        }
        List<Vector2> taken = new List<Vector2>();
        foreach (var item in puzzlePieces)
        {
            Vector2 target;
            target = possibleSpaces[Random.Range(0, possibleSpaces.Count)];
            possibleSpaces.Remove(target);
            item.transform.position = target*GridSize;
            taken.Add(target);
            item.currentLocation = target;
            PuzzlePiece.occupiedSpaces.Add(target, item.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckDone()&&!done)
        {
            done = true;
            GameManager.instance.StartExiting(true);
            if (GameManager.lvlNum == 1) { GameManager.windDone = true; } else { GameManager.solarDone = true; }
            (GameManager.lvlNum == 1 ? info1 : info2).SetTrigger("Go");
        }
    }

    bool CheckDone()
    {
        foreach (var item in puzzlePieces)
        {
            if (item.targetLocation != item.currentLocation)
            {
                return false;
            }
        }
        return true;
    }
}
