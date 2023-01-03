using System;
using ScriptableObject.SOConstructor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemyShipController : MonoBehaviour
    {
        public static Action<int> DeathEnemy;

        public bool CanShot { get; set; }
        public bool CanMove { get; set; }

        [Header("Settings of shot")]
        [SerializeField] private int _valueToScore = 8;
        [SerializeField] private float _forceToShot = 250;
        [SerializeField] private Transform _originShot;

        [Header("Setting to random move")]
        [SerializeField] private float _speedToMove;

        [SerializeField] private SOSettingToMove _soValuesToMoveRandomPosition;
        
            
        private Transform _containerBullets;
        private float _timeForShot;
        private float _currentTime;
        private Vector2 positionToMove;

        private void Awake()
        {
            _containerBullets = GameObject.Find("BulletsToEnemy").transform;
        }

        private void OnEnable()
        {
            _timeForShot = Random.Range(8, 15);
            SetPositionRandomToMove();
            if (!CanMove)
            {
                CanMove = true;
                return;
            }
        }
        
        private void Update()
        {
            _currentTime += Time.deltaTime;
            if (CanShot)
            {
                if (Math.Abs(_currentTime - _timeForShot) < 1)
                {
                    Shot();
                    _currentTime = 0;
                }
            }

            if (!CanMove)
            {
                return;
            }
            var distance = Vector2.Distance(transform.position, positionToMove);
            if (distance < 0.5f)
            {
                SetPositionRandomToMove();
            }

            transform.position = Vector2.MoveTowards(transform.position, positionToMove,
                _speedToMove * Time.deltaTime);
        }

        private void OnDisable()
        {
            CanMove = false;
            CanShot = false;
        }

        private void Shot()
        {
            var currentBullet = FindBulletForUse();
            currentBullet.transform.position = transform.position;
            currentBullet.SetActive(true);
            currentBullet.GetComponent<Rigidbody2D>().AddForce(Vector2.down * _forceToShot);
        }

        private GameObject FindBulletForUse()
        {
            GameObject bulletToUSe = null;
            if (_containerBullets.transform.childCount <= 0)
            {
                return null;
            }
            
            for (var i = 0; i < _containerBullets.transform.childCount; i++)
            {
                if (_containerBullets.transform.GetChild(i).gameObject.activeSelf)
                {
                    continue;
                }

                bulletToUSe = _containerBullets.transform.GetChild(i).gameObject;
                break;
            }

            return bulletToUSe;
        }

        private void SetPositionRandomToMove()
        {
            var positionX = Random.Range(_soValuesToMoveRandomPosition.ValueMinXToMove, _soValuesToMoveRandomPosition.ValueMaxXToMove);
            var positionY = Random.Range(_soValuesToMoveRandomPosition.ValueMinYToMove, _soValuesToMoveRandomPosition.ValueMaxYToMove);

            positionToMove = new Vector2(positionX, positionY);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.transform.CompareTag("Bullet"))
            {
                return;
            }
            DeathEnemy?.Invoke(_valueToScore);
            gameObject.SetActive(false);
        }
    }
}
