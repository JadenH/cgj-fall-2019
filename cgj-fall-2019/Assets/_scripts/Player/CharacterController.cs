using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _sprite;
    private float _horizontal;
    private float _vertical;

    public bool LockMovement;
    public float WalkSpeed = 5;
    public Gun CurrentGun;

    // Start is called before the first frame update
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!LockMovement)
        {
            _horizontal = Input.GetAxisRaw("Horizontal");
            _vertical = Input.GetAxisRaw("Vertical");
            _rigidbody.velocity = new Vector2(_horizontal, _vertical).normalized * WalkSpeed;
            _rigidbody.velocity = new Vector2(_horizontal, _vertical).normalized * WalkSpeed;
            var velocity = _rigidbody.velocity;
            if (velocity.magnitude > 0)
            {
                var angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
                _sprite.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            }

            if (Input.GetButtonDown("Fire1"))
            {
<<<<<<< HEAD
                CurrentGun.FirePressed(Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position));
=======
               CurrentGun.FirePressed(Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position));
>>>>>>> 3012a6d237468539510eb1430da6761c5077a458
            }
        }
        else
        {
            _horizontal = 0;
            _vertical = 0;
            _rigidbody.velocity = new Vector2(0, 0);
        }

    }

    public void EquipWeapon(Gun gun)
    {

    }
}