using Scripts_Baek;
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

        public Status currentStatus;
        public int currentHp;
        public Active currentActive;
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
            Vector2 nextVector = inputVector.normalized * currentStatus.speed;
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

        public int finalDamage = 0;
        public void OnDamage(int damage, Enemy e)
        {
            finalDamage = damage;
            finalDamage -= currentStatus.defensive;
            currentHp -= finalDamage;
            onDamage.Invoke(e);
        }

        public void OnParry(Enemy e)
        {
            onParry.Invoke(e);
        }

        public void OnDeath()
        {
            onDeath.Invoke();
        }
        
        public UnityEvent onMove;
        public UnityEvent onFire;
        public UnityEvent<Enemy> onDamage;
        public UnityEvent<Enemy> onParry;
        public UnityEvent onDeath;

        public void Init(Status status)
        {
            currentStatus.maxHp = status.maxHp;
            currentHp = currentStatus.maxHp;
            currentStatus.shield = status.shield;
            currentStatus.damage = status.damage;
            currentStatus.speed = status.speed;
            currentStatus.defensive = status.defensive;
            currentStatus.invincibleTime = status.invincibleTime;
        }
    }
}
