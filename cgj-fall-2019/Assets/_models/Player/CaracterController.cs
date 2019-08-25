using Player.Guns;
using UnityEngine;

namespace Player
{
    public class CaracterController : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        private SpriteRenderer _sprite;
        private float _horizontal;
        private float _vertical;

        public bool LockMovement = false;
        public float walkSpeed = 5;
        public Gun currentGun;

        // Start is called before the first frame update
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _sprite = GetComponentInChildren<SpriteRenderer>();
        }
        public void LockMovment()
        {
            LockMovement = true;
        }

        public void UnlockMovment()
        {
            LockMovement = false;
        }
        // Update is called once per frame
        void Update()
        {
            if(!LockMovement)
            {
                _horizontal = Input.GetAxisRaw("Horizontal");
                _vertical = Input.GetAxisRaw("Vertical");
                _rigidbody.velocity = new Vector2(_horizontal, _vertical).normalized * walkSpeed;
                var velocity = _rigidbody.velocity;
                if (velocity.magnitude > 0)
                {
                    var angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
                    _sprite.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                }

                if (Input.GetButtonDown("Fire1"))
                {
                    currentGun.FirePressed(Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position));
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
}
