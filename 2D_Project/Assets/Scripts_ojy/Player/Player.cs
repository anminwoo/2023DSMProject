using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Scripts_ojy
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Vector2 inputVector;
        public float speed;

        private Rigidbody2D _rigid;
        private SpriteRenderer _spriter;
        private Animator _anim;
        
        void Awake()
        {
            _rigid = GetComponent<Rigidbody2D>();
            _spriter = GetComponent<SpriteRenderer>();
            _anim = GetComponent<Animator>();
        }
        void Update()
        {
            Vector2 nextVector = inputVector.normalized * speed;
            _rigid.velocity = nextVector;
        }

        void OnMove(InputValue value)
        {
            inputVector = value.Get<Vector2>();
            onMove.Invoke();
        }

        void OnFire(InputValue value)
        {
            onFire.Invoke();
        }

        void LateUpdate()
        {
            _anim.SetFloat("Speed", inputVector.magnitude);
            if (inputVector.x != 0)
            {
                _spriter.flipX = inputVector.x < 0;
            }
            
        }
        public void OnDamage()
        {
            onDamage.Invoke();
        }

        public void OnParry()
        {
            onParry.Invoke();
        }
        
        
        
        public UnityEvent onMove;
        public UnityEvent onFire;
        public UnityEvent onDamage;
        public UnityEvent onParry;
    }
}
