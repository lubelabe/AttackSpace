using System.Collections;
using UnityEngine;

namespace Miscellaneous
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private float _timeOfLife = 20;
        [SerializeField] private bool _isBulletOfPlayer;
        [SerializeField] private bool _isBulletOfEnemy;
    
        private void Start()
        {
            StartCoroutine(DisableBullet());
        }

        private IEnumerator DisableBullet()
        {
            yield return new WaitForSeconds(_timeOfLife);
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.CompareTag("Player") && _isBulletOfPlayer || other.transform.CompareTag("Enemy") && _isBulletOfEnemy)
            {
                return;
            }
            gameObject.SetActive(false);
        }
    }
}
