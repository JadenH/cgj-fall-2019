using TMPro;
using UnityEngine;

public class Portal : GameBehaviour
{
    public GameObject Canvas;
    public TextMeshProUGUI TextField;
    public Animator Ani;
    public ParticleSystem Parts;

    private bool _onPortal = false;
    private Scenario _scenario;


    void Start()
    {
        var emission = Parts.emission;
        emission.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _onPortal = true;
        Canvas.SetActive(true);
        Ani.SetBool("PortalOn", true);
        var emission = Parts.emission;
        emission.enabled = true;
        Parts.time = 0;
        Parts.Play();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _onPortal = false;
        Canvas.SetActive(false);
        Ani.SetBool("PortalOn", false);
        var emission = Parts.emission;
        emission.enabled = false;
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
