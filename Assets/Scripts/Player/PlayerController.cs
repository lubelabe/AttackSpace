using System;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public static Action PlayerDeath;
        
        [Header("Settings to player")]
        [SerializeField] private float _speedMove;
        
        [Header("Settings to shot")]
        [SerializeField] private Transform _bulletContainer;
        [SerializeField] private float _forceToShot;

        private Rigidbody2D _rb;
        private GameObject _currentBullet;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                UseBullet();
            }
        }

        private void FixedUpdate()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            var posToMove = new Vector3(horizontal, vertical, 0);
            _rb.MovePosition(transform.position + posToMove * Time.deltaTime * _speedMove);
        }

        private void UseBullet()
        {
            if (_bulletContainer.childCount <= 0)
            {
                return;
            }
            for (var i = 0; i < _bulletContainer.childCount; i++)
            {
                if (_bulletContainer.transform.GetChild(i).gameObject.activeSelf)
                {
                    continue;
                }

                _currentBullet = _bulletContainer.transform.GetChild(i).gameObject;
                _currentBullet.transform.position = transform.position;
                _currentBullet.SetActive(true);
                _currentBullet.GetComponent<Rigidbody2D>().AddForce(Vector2.up * _forceToShot);
                break;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.CompareTag("BulletEnemy") || other.transform.CompareTag("Enemy"))
            {
                PlayerDeath?.Invoke();
                gameObject.SetActive(false);
            }
        }
    }
}