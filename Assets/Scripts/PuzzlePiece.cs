using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//script for individual puzzle pieces that create their functionality
public class PuzzlePiece : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public int index;
    public static int activePiece;
    public static Dictionary<Vector2, GameObject> occupiedSpaces = new Dictionary<Vector2, GameObject>();
    public bool inSpace;
    public Vector2 targetLocation; //grid
    public Vector2 currentLocation; //grid
    private float gridSize;
    private bool dragging;
    private Vector3 startDraggingPos;
    private Vector3 startDraggingMousePos;
    public Transform shadow;
    private SpriteRenderer sr;
    private SpriteRenderer shadowSr;

    // Start is called before the first frame update
    void Start()
    {
        gridSize = FindAnyObjectByType<PuzzlePieceManager>().GridSize;
        sr = GetComponent<SpriteRenderer>();
        shadowSr = shadow.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dragging)
        {
            transform.position = (startDraggingPos + Camera.main.ScreenToWorldPoint(Input.mousePosition) - startDraggingMousePos) + new Vector3(0, 0, -1);
            shadow.position = new Vector3(gridSize * Mathf.Round(transform.position.x / gridSize), gridSize * Mathf.Round(transform.position.y / gridSize));
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (activePiece == 0)
        {
            if (inSpace)
            {
                occupiedSpaces.Remove(new Vector2(Mathf.Round(transform.position.x / gridSize), Mathf.Round(transform.position.y / gridSize)));
            }
            activePiece = index;
            dragging = true;
            startDraggingMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            startDraggingPos = transform.position;
            sr.sortingOrder = 5;
            shadowSr.sortingOrder = 4;
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (occupiedSpaces.ContainsKey(new Vector2(Mathf.Round(transform.position.x / gridSize), Mathf.Round(transform.position.y / gridSize))))
        {
            occupiedSpaces[new Vector2(Mathf.Round(transform.position.x / gridSize), Mathf.Round(transform.position.y / gridSize))].GetComponent<PuzzlePiece>().KnockedOut();
        }
        occupiedSpaces.Add(new Vector2(Mathf.Round(transform.position.x / gridSize), Mathf.Round(transform.position.y / gridSize)), gameObject);
        activePiece = 0;
        dragging = false;
        inSpace = true;
        transform.position = new Vector3(gridSize * Mathf.Round(transform.position.x / gridSize), gridSize * Mathf.Round(transform.position.y / gridSize));
        shadow.position = transform.position;
        shadowSr.sortingOrder = -5;
        sr.sortingOrder = 0;
        currentLocation = new Vector2(Mathf.Round(transform.position.x / gridSize), Mathf.Round(transform.position.y / gridSize));
    }

    //private void OnMouseDown()
    //{
    //    if (activePiece == 0)
    //    {
    //        if (inSpace)
    //        {
    //            occupiedSpaces.Remove(new Vector2(Mathf.Round(transform.position.x / gridSize), Mathf.Round(transform.position.y / gridSize)));
    //        }
    //        activePiece = index;
    //        dragging = true;
    //        startDraggingMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        startDraggingPos = transform.position;
    //        sr.sortingOrder = 5;
    //        shadowSr.sortingOrder = 4;
    //    }
    //}
    //private void OnMouseUp()
    //{
    //    if (occupiedSpaces.ContainsKey(new Vector2(Mathf.Round(transform.position.x / gridSize), Mathf.Round(transform.position.y / gridSize))))
    //    {
    //        occupiedSpaces[new Vector2(Mathf.Round(transform.position.x / gridSize), Mathf.Round(transform.position.y / gridSize))].GetComponent<PuzzlePiece>().KnockedOut();
    //    }
    //    occupiedSpaces.Add(new Vector2(Mathf.Round(transform.position.x / gridSize), Mathf.Round(transform.position.y / gridSize)), gameObject);
    //    activePiece = 0;
    //    dragging = false;
    //    inSpace = true;
    //    transform.position = new Vector3(gridSize * Mathf.Round(transform.position.x / gridSize), gridSize * Mathf.Round(transform.position.y / gridSize));
    //    shadow.position = transform.position;
    //    shadowSr.sortingOrder = -5;
    //    sr.sortingOrder = 0;
    //    currentLocation = new Vector2(Mathf.Round(transform.position.x / gridSize), Mathf.Round(transform.position.y / gridSize));
    //}

    public void KnockedOut()
    {
        inSpace = false;
        occupiedSpaces.Remove(new Vector2(Mathf.Round(transform.position.x / gridSize), Mathf.Round(transform.position.y / gridSize)));
        transform.Translate(Random.Range(0.3f, 0.7f) * gridSize * (2 * Random.Range(1, 3) - 3), Random.Range(0.3f, 0.7f) * gridSize * (2 * Random.Range(1, 3) - 3), 0);
        sr.sortingOrder = 1;
    }
}
