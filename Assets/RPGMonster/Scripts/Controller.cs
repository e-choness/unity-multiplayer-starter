using kart.RPGMonster.Scripts.Inputs;
using UnityEngine;
using Mirror;

namespace kart.RPGMonster.Scripts
{
    public class Controller : NetworkBehaviour
    {
        [SerializeField] private float speed = 0.01f;
        [SerializeField] private ControlReader controlReader;
        
        private Animator _animator;
        private static readonly int Moving = Animator.StringToHash("Moving");

        private void OnEnable()
        {
            controlReader.Enable();
        }

        private void OnDisable()
        {
            controlReader.Disable();
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        private void Update()
        {
            Move();
        }

        private void Move()
        {
            if (!authority) return;
            
            var movement = new Vector3(controlReader.Move.x, 0, controlReader.Move.y) * speed;
            transform.position += movement;

            if (movement.x != 0 || movement.z != 0)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(movement),
                    10000 * Time.deltaTime);
                _animator.SetBool(Moving, true);
            }
            else
            {
                _animator.SetBool(Moving, false);
            }
        }
    }
}
