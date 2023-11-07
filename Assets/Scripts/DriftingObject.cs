using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script for individual carbon in carbon capture game that allows them to be draggable
public class DriftingObject : MonoBehaviour
{
    private bool dragging;
    private Vector3 startDraggingPos;
    private Vector3 startDraggingMousePos;
    private Rigidbody2D rb;
    public float speed;
    public float smooth;
    public List<Vector3> oldPos;
    public List<float> oldDeltaTime;
    public int delay;
    public float minRot;
    public float rotScale;
    public float rotAccel;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        minRot = Random.value*rotScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.angularVelocity = Mathf.Clamp(rb.angularVelocity, -100, 100);
        if (minRot <= 0 ? rb.angularVelocity > minRot : rb.angularVelocity < minRot)
        {
           rb.angularVelocity = Mathf.Lerp(rb.angularVelocity, minRot, rotAccel);
        }
        if (dragging)
        {
            transform.position = Vector3.Lerp(transform.position, startDraggingPos + Camera.main.ScreenToWorldPoint(Input.mousePosition) - startDraggingMousePos - Camera.main.transform.position, smooth);
        }
        if (oldPos.Count > delay)
        {
            oldPos.RemoveAt(0);
            oldDeltaTime.RemoveAt(0);
        }
        oldPos.Add(transform.position);
        oldDeltaTime.Add(Time.deltaTime);
    }

    //private void OnDrawGizmos()
    //{

    //    float sum = 0f;
    //    foreach (float item in oldDeltaTime)
    //    {
    //        sum += item;
    //    }
    //    Gizmos.DrawRay(oldPos[0], (transform.position - oldPos[0]) * 10);
    //    foreach (Vector3 item in oldPos)
    //    {
    //        Gizmos.DrawSphere(item, 0.1f);
    //    }
    //}

    private void OnMouseDown()
    {
        dragging = true;
        startDraggingMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        startDraggingPos = transform.position;
    }
    private void OnMouseUp()
    {
        dragging = false;
        float sum = 0f;
        foreach (float item in oldDeltaTime)
        {
            sum += item;
        }
        rb.velocity = (transform.position - oldPos[0])/sum;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Basket"))
        {
            gameObject.SetActive(false);
        }
    }
}
