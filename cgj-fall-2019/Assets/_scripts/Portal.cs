using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Portal : MonoBehaviour
{

    public GameObject Canvas;
    public bool Truth = false;
    public Text TextField;


    private bool _onPortal = false;    
   
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _onPortal = true;
        Canvas.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _onPortal = false;
        Canvas.SetActive(false);
    }

    public void SetText(string lie)
    {
        TextField.text = lie;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

        }
    }

}
