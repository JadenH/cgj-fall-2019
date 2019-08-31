using System;
using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private float _horizontal;
    private float _vertical;
    private Gun _nearbyGun;
    private Animator _animator;
    private SpriteRenderer _renderer;
    private bool _audioPlaying;

    public bool LockMovement;
    public float WalkSpeed = 5;
    public Gun CurrentGun;
    public GameObject PickupInterface;
    public AudioSource AudioSource;
    public Health Health;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _renderer = _animator.GetComponent<SpriteRenderer>();
        if (CurrentGun)
        {
            CurrentGun.GetComponent<CircleCollider2D>().enabled = false;
        }

        Health.HealthChanged.AddListener(HandleHealthChanged);
    }

    private void HandleHealthChanged(float current, float delta, DamageType damageType)
    {
        if (damageType != DamageType.Heal)
        {
            StartCoroutine(Damaged());
        }
    }

    private IEnumerator Damaged()
    {
        _renderer.color = Color.red;
        yield return new WaitForSeconds(.2f);
        _renderer.color = Color.white;
    }

    private void Update()
    {
        if (_nearbyGun && Input.GetKeyDown(KeyCode.E))
        {
            EquipWeapon(_nearbyGun);
        }
        if (!LockMovement)
        {
            _horizontal = Input.GetAxisRaw("Horizontal");
            _vertical = Input.GetAxisRaw("Vertical");
            _rigidbody.velocity = new Vector2(_horizontal, _vertical).normalized * WalkSpeed;

            var gunDir = Input.mousePosition - Camera.main.WorldToScreenPoint(CurrentGun.transform.position);
            if (CurrentGun.CurrentCooldown <= Time.time)
            {
                if (Input.GetButtonDown("Fire"))
                {
                    CurrentGun.FirePressed(gunDir);
                }

                if (Input.GetButton("Fire"))
                {
                    CurrentGun.FireHold(gunDir);
                }
            }
        }
        else
        {
            _horizontal = 0;
            _vertical = 0;
            _rigidbody.velocity = new Vector2(0, 0);
        }

        if (!_audioPlaying && (Math.Abs(_vertical) > 0.001 || Math.Abs(_horizontal) > 0.001))
        {
            _audioPlaying = true;
            AudioSource.Play();
        }
        else if (_audioPlaying && (Math.Abs(_vertical) < 0.001 && Math.Abs(_horizontal) < 0.001))
        {
            AudioSource.Stop();
            _audioPlaying = false;
        }

        var lookDir = (Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position)).normalized;
        var angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        if (Math.Abs(Mathf.Sign(lookDir.x) - 1) > .01)
        {
            CurrentGun.transform.rotation = Quaternion.AngleAxis(angle + 180, Vector3.forward);
        }
        else
        {
            CurrentGun.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        if (Mathf.Round(lookDir.y) <= 0)
        {
            CurrentGun.GetComponent<SpriteRenderer>().sortingOrder = _renderer.sortingOrder + 1;
        }
        else
        {
            CurrentGun.GetComponent<SpriteRenderer>().sortingOrder = _renderer.sortingOrder - 1;
        }
        _renderer.flipX = Mathf.Round(lookDir.x) > 0;
        if (Vector3.Dot(lookDir, _rigidbody.velocity.normalized) > 0)
        {
            _animator.SetFloat("Multiplier", 1);
        }
        else
        {
            _animator.SetFloat("Multiplier", -1);
        }

        CurrentGun.GetComponent<SpriteRenderer>().flipX = Math.Abs(Mathf.Sign(lookDir.x) - 1) > .01;

        CurrentGun.transform.localPosition = new Vector3(Mathf.Round(lookDir.x) * .25f, -.4f);
        _animator.SetFloat("Horizontal", -Mathf.Abs(lookDir.x));
        _animator.SetFloat("Vertical", lookDir.y);
        _animator.SetFloat("Magnitude", _rigidbody.velocity.magnitude);
    }

    public void EquipWeapon(Gun gun)
    {
        var gunTransform = gun.transform;
        CurrentGun.transform.position = gunTransform.position;
        CurrentGun.transform.parent = gunTransform.parent;
        CurrentGun.GetComponent<Collider2D>().enabled = true;
        gun.transform.parent = transform;
        gun.GetComponent<Collider2D>().enabled = false;
        gun.transform.localPosition = new Vector3(-0.3f, 0.2f, 0);
        CurrentGun = gun;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        _nearbyGun = other.GetComponent<Gun>();
        if (_nearbyGun)
        {
            PickupInterface.SetActive(true);
            PickupInterface.transform.position = Camera.main.WorldToScreenPoint(other.transform.position) + Vector3.up*20;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (_nearbyGun && other.transform == _nearbyGun.transform)
        {
            PickupInterface.SetActive(false);
            _nearbyGun = null;
        }
    }
}