using UnityEngine;

namespace RigidbodyHelpers
{
    [RequireComponent(typeof(Rigidbody))]
    public class RigidbodyCustomGravity : MonoBehaviour
    {
        [SerializeField] private float _gravity = 9.8f;
        private Rigidbody _rigidbody;
        
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.useGravity = false;
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity -= Vector3.up * _gravity * Time.fixedDeltaTime;
        }
    }
}
