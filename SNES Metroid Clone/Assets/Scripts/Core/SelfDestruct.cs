using System.Collections;
using UnityEngine;

namespace Core
{
    public class SelfDestruct : MonoBehaviour
    {
        [SerializeField] private float _destructTime = 2.0f;
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(SelfDestructSequence());
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        IEnumerator SelfDestructSequence()
        {
            yield return new WaitForSeconds(_destructTime);
            Destroy(this.gameObject);
        }
    }
}
