using System;
using Enemy;
using Player;
using TMPro;
using UnityEngine;
using Utils;

namespace Miscellaneous
{
    public class GameManager : MonoBehaviour
    {
        public static bool isGameOver { get; set; }
    
        [Header("Objects to coin")]
        [SerializeField] private TextMeshProUGUI _textScore;

        [Header("Objects to game over")] 
        [SerializeField] private GameObject _panelGameOver;

        [Header("Player")] 
        [SerializeField] private GameObject _player;

        private int _countScore;
    
        private void OnEnable()
        {
            CoinController.IsCatch += PlusScore;
            EnemyShipController.DeathEnemy += PlusScore;
            PlayerController.PlayerDeath += GameOver;
        }

        private void OnDisable()
        {
            CoinController.IsCatch -= PlusScore;
            EnemyShipController.DeathEnemy -= PlusScore;
            PlayerController.PlayerDeath -= GameOver;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                _panelGameOver.SetActive(false);
                isGameOver = false;
                _player.SetActive(true);
            }
        }

        private void PlusScore(int score)
        {
            _countScore += score;
            _textScore.text = "Score: " + _countScore;
        }

        private void GameOver()
        {
            _panelGameOver.SetActive(true);
            isGameOver = true;
        }
    }
}
