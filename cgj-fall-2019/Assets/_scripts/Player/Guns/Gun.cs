using System.Collections;
using UnityEngine;

public abstract class Gun : GameBehaviour
{
    private GameObject _pickupInterface;
    private bool _pickup;

    [Tooltip("Seconds")]
    public float BulletLife = 2;
    public GameObject BulletPrefab;

    public virtual void FirePressed(Vector2 direction)
    {
    }

    public virtual void FireHold(Vector2 direction)
    {
    }

    public void Start()
    {
        _pickupInterface = Player.Character.transform.Find("PlayerInterface/Pickup").gameObject;
    }

    public void Update()
    {
        if (_pickup && Input.GetKeyDown(KeyCode.E))
        {
            var charController = Player.Character.GetComponent<CharacterController>();
            charController.EquipWeapon(this);
        }
    }

    protected IEnumerator Shoot(GameObject bullet, Vector2 direction)
    {
        while (bullet.gameObject != null)
        {
            var velocity = direction.normalized/4;

            bullet.transform.position += (Vector3) velocity;
            var hit = Physics2D.Raycast(bullet.transform.position, velocity.normalized, velocity.magnitude);
            Debug.DrawRay(bullet.transform.position, velocity.normalized);
            if (hit.collider && hit.transform.gameObject.tag != "Player")
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    if (hit.transform.GetComponent<Health>())
                    {
                        hit.transform.GetComponent<Health>().TakeDamage(10);
                    }
                }
                Destroy(bullet.gameObject);
            }
            yield return null;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        var charController = other.GetComponent<CharacterController>();
        if (charController)
        {
            _pickupInterface.SetActive(true);
            _pickup = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        _pickupInterface.SetActive(false);
        _pickup = false;
    }
}