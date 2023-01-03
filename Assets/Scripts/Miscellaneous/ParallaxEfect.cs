using UnityEngine;

namespace Miscellaneous
{
    public class ParallaxEfect : MonoBehaviour
    {
        [SerializeField] private float _speedToMoveParallax;
        [SerializeField] private float _limitToDecrease;
        [SerializeField] private Transform _positionInitialToDecrease;

        private void Update()
        {
            if (transform.position.y > _limitToDecrease)
            {
                transform.position += Vector3.down * _speedToMoveParallax * Time.deltaTime;
            }
            else
            {
                transform.position = _positionInitialToDecrease.position;
            }
        }
    }
}
