using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{

    public GameObject Canvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Canvas.SetActive(true);
        print("SADfas");
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Canvas.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
