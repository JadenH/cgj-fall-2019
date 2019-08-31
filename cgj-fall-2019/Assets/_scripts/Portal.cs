using TMPro;
using UnityEngine;

public class Portal : GameBehaviour
{
    public GameObject Canvas;
    public TextMeshProUGUI TextField;

    private bool _onPortal = false;
    private Scenario _scenario;

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

    public void SetScenario(Scenario scenario)
    {
        _scenario = scenario;
        TextField.text = scenario.TruthDescription;
    }

    private void Update()
    {
        if (_onPortal && Input.GetKeyDown(KeyCode.Space))
        {
            Game.ChooseScenario(_scenario);
        }
    }

}
