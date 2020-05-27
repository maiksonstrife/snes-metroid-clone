using Control;
using JetBrains.Annotations;
using Player;
using UnityEngine;

namespace Camera
{
    public class CameraController : MonoBehaviour
    {
        public GameObject player;

        #region Tunable Fields

        [SerializeField] private float cameraXOffset = 5.0f;
        [SerializeField] private float cameraYOffset = 1.0f;
        [SerializeField] private float horizSpeed = 2.0f;
        [SerializeField] private float vertSpeed = 4.0f;
        [SerializeField] private float cameraZ = -10.0f;
        #endregion

        #region Private Fields
        
        
        private Transform _camera;
        private PlayerController _playerController;

        #endregion


        #region Monobehaviour

        // Start is called before the first frame update
        void Start()
        {
            if (!player)
                player = GameObject.FindGameObjectWithTag("Player");
            if(player == null)
                Debug.LogError("No Player in scene for camera to reference.");
            _playerController = player.GetComponent<PlayerController>();
            _camera = UnityEngine.Camera.main.transform;

            _camera.position = player.transform.position + new Vector3(cameraXOffset, cameraYOffset, cameraZ);

        }

        // Update is called once per frame
        void Update()
        {
            if (_playerController.isFacingRight)
            {
                _camera.position = new Vector3( Mathf.Lerp(_camera.position.x, player.transform.position.x + cameraXOffset, horizSpeed * Time.deltaTime),
                                                Mathf.Lerp(_camera.position.y, player.transform.position.y + cameraYOffset, vertSpeed * Time.deltaTime),
                                                _camera.position.z);
                
            }
            if (!_playerController.isFacingRight)
            {
                _camera.position = new Vector3( Mathf.Lerp(_camera.position.x, player.transform.position.x - cameraXOffset, horizSpeed * Time.deltaTime),
                                                Mathf.Lerp(_camera.position.y, player.transform.position.y + cameraYOffset, vertSpeed * Time.deltaTime),
                                               _camera.position.z);
                
            }
            
        }

        #endregion
        
    }
}
