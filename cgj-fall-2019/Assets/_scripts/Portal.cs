using TMPro;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject Canvas;
    public bool Truth = false;
    public TextMeshProUGUI TextField;

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
