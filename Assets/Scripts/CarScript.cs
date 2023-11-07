using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script for each car in transportation game
public class CarScript : MonoBehaviour
{
    public float speed;
    public float offsetXReset;
    public bool done;
    public Sprite bike;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime);
    }

    private void OnMouseDown()
    {
        done = true;
        GetComponent<SpriteRenderer>().sprite = bike;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Finish"))
        {
            transform.position = new Vector3(offsetXReset, transform.position.y);
        }
    }
}
