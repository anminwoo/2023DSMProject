using Scripts_Baek.Item.Core;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Scripts_ojy.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Vector2 inputVector;
        public CharacterStatusData statusData;

        public uint maxHp;
        public uint currentHp;
        public uint shield;
        public uint damage;
        public float speed;
        public uint defensive;
        
        private Rigidbody2D _rigid;
        private SpriteRenderer _sr;
        private Animator _anim;
        
        void Awake()
        {
            Init(statusData.status);
            _rigid = GetComponent<Rigidbody2D>();
            _sr = GetComponent<SpriteRenderer>();
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
            _anim.SetBool("IsWalk", inputVector != Vector2.zero);
            if (inputVector.x != 0)
            {
                _sr.flipX = inputVector.x < 0;
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

        private void Init(Status status)
        {
            maxHp = status.maxHp;
            currentHp = maxHp;
            shield = status.shield;
            damage = status.damage;
            speed = status.speed;
            defensive = status.defensive;
        }
    }
}
