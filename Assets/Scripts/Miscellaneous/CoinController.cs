using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utils
{
    public class CoinController : MonoBehaviour
    {
        public static Action<int> IsCatch;

        [Header("Settings Coin")]
        [SerializeField] private int _valueToScore = 5;
        [SerializeField] private float _valueMinXToAppear = 5;
        [SerializeField] private float _valueMaxXToAppear = 5;
        [SerializeField] private float _timeForLife = 10;

        private Vector3 _initialPosition;

        private void Awake()
        {
            _initialPosition = transform.position;
        }

        private void Start()
        {
            StartCoroutine(DisableCoin());
        }

        private void OnEnable()
        {
            SelectRandomPositionToAppear();
        }

        private void OnDisable()
        {
            transform.position = _initialPosition;
        }

        private void SelectRandomPositionToAppear()
        {
            var randomPositionX = Random.Range(_valueMinXToAppear, _valueMaxXToAppear);
            var transformAux = transform;
            transformAux.position = new Vector3(randomPositionX, transformAux.position.y, 0);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.transform.CompareTag("Player"))
            {
                return;
            }
            IsCatch?.Invoke(_valueToScore);
            gameObject.SetActive(false);
        }

        private IEnumerator DisableCoin()
        {
            yield return new WaitForSeconds(_timeForLife);
            gameObject.SetActive(false);
        }
    }
}
