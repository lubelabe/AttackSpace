using System.Collections;
using System.Collections.Generic;
using Miscellaneous;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class SpawnEnemyController : MonoBehaviour
    {
        [Header("Settings spawn enemies")]
        [SerializeField] private GameObject[] _containersEnemies;
        [SerializeField] private Transform[] _initialPosition;

        [Header("Settings to wave enemies")]
        [SerializeField] private int _sizeWave;

        private GameObject _enemyTypeSelected;
        private readonly List<GameObject> _listFinalEnemiesToSpawn = new List<GameObject>();
        private readonly int[] _enemiesCanShot = new int[2];
        private int _countEnemiesDestroy;

        private void Start()
        {
            StartCoroutine(StartActiveEnemies());
        }

        private void OnEnable()
        {
            EnemyShipController.DeathEnemy += CountEnemiesDeath;
        }

        private void OnDisable()
        {
            EnemyShipController.DeathEnemy -= CountEnemiesDeath;
        }

        private void LateUpdate()
        {
            if (GameManager.isGameOver)
            {
                return;
            }
            if (_countEnemiesDestroy >= _sizeWave)
            {
                StartCoroutine(StartActiveEnemies());
            }
        }

        private void CountEnemiesDeath(int obj)
        {
            _countEnemiesDestroy++;
        }
        
        private IEnumerator StartActiveEnemies()
        {
            _countEnemiesDestroy = 0;
            yield return new WaitForSeconds(2);
            SelectEnemyType();
        }

        private void SelectEnemyType()
        {
            var randomEnemies = Random.Range(0, _containersEnemies.Length);
            _enemyTypeSelected = _containersEnemies[randomEnemies];
            CheckEnemiesCanBeUse();
        }

        private void CheckEnemiesCanBeUse()
        {
            _listFinalEnemiesToSpawn.Clear();
            if (_enemyTypeSelected.transform.childCount <= 0)
            {
                return;
            }

            for (var i = 0; i < _enemyTypeSelected.transform.childCount; i++)
            {
                if (_enemyTypeSelected.transform.GetChild(i).gameObject.activeSelf)
                {
                    continue;
                }

                if (_listFinalEnemiesToSpawn.Count < _sizeWave)
                {
                    _listFinalEnemiesToSpawn.Add(_enemyTypeSelected.transform.GetChild(i).gameObject);
                }
            }
            ActiveEnemy();
        }
        
        private void ActiveEnemy()
        {
            var randomPositionInitial = Random.Range(0, _initialPosition.Length);
            _enemiesCanShot[0] = Random.Range(0, _sizeWave);
            _enemiesCanShot[1] = Random.Range(0, _sizeWave);
            
            for (var i = 0; i < _sizeWave; i++)
            {
                var currentEnemy = _listFinalEnemiesToSpawn[i];
                currentEnemy.transform.position = _initialPosition[randomPositionInitial].position;
                currentEnemy.SetActive(true);
                if (!_enemiesCanShot[0].Equals(i) && !_enemiesCanShot[1].Equals(i))
                {
                    continue;
                }
                currentEnemy.GetComponent<EnemyShipController>().CanShot = true;
            }
        }
    }
}