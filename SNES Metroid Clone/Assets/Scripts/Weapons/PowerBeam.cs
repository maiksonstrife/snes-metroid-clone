using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Weapons
{
    
    [RequireComponent(typeof(CircleCollider2D))]
    public class PowerBeam : MonoBehaviour, IWeapon
    {
        [SerializeField] private int _damage = 10;
        [SerializeField] private float _speed = 10.0f;
        public GameObject destructAnim;
        
        private Vector3 _direction = Vector3.right;
        private CircleCollider2D _collider;
        
        // Start is called before the first frame update
        public void Start()
        {
            _collider = GetComponent<CircleCollider2D>();
            StartCoroutine(SelfDestruct());
        }

        // Update is called once per frame
        public void Update()
        {
            transform.Translate(_direction * (_speed * Time.deltaTime));
        }

        public void SetDirection(Vector3 direction)
        {
            _direction = direction;
        }

        public void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log("Beam Hit Object in layer " + other.gameObject.layer.ToString());
            if (other.gameObject.layer == 9) //Layer 9 is platforms
            {
                GameObject.Instantiate(destructAnim, transform.position, quaternion.identity);
                Destroy(this.gameObject);
            }
        }

        private IEnumerator SelfDestruct()
        {
            yield return new WaitForSeconds(5.0f);
            Destroy(this.gameObject);
        }
    }
}
