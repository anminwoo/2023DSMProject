using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts_An
{
    public class PlayerController : MonoBehaviour, IDamagable
    {
        [SerializeField] private Vector2 inputVector;

        public int maxHp;
        public int currentHp;
        public int currentDamage;
        private bool isDead;

        [SerializeField] private float currentSpeed;
    
        private Rigidbody2D _rigid;
        private SpriteRenderer _sr;
        private Animator _anim;
        private CapsuleCollider2D _capCol;

        private void Start()
        {
            _rigid = GetComponent<Rigidbody2D>();
            _sr = GetComponent<SpriteRenderer>();
            _anim = GetComponent<Animator>();
            _capCol = GetComponent<CapsuleCollider2D>();
        }

        private void Update()
        {
            if (isDead)
            {
                _rigid.velocity = Vector2.zero;
                return;
            }
            Vector2 nextVector = inputVector.normalized * currentSpeed;
            _rigid.velocity = nextVector;
        }

        private void OnMove(InputValue value)
        {
            inputVector = value.Get<Vector2>();
        }

        private void LateUpdate()
        {
            _anim.SetBool("IsWalk", inputVector != Vector2.zero);
            if (inputVector.x != 0)
            {
                _sr.flipX = inputVector.x < 0;
            }
        }

        public void GetDamage(int damage)
        {
            currentHp -= damage;
            UIManager.instance.hpSlider.value = currentHp;
            AudioManager.instance.playSfx(AudioManager.Sfx.PlaDam);
            if (currentHp <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            _anim.SetTrigger("Die");
            GameObject sword = GetComponentInChildren<GameObject>();
            sword.gameObject.SetActive(false);
            _capCol.enabled = false;
            isDead = true;
            AudioManager.instance.playSfx(AudioManager.Sfx.Plath);
        }

        public void ChangeStatus(ItemData itemData)
        {
            currentDamage += itemData.damage;
            currentHp += itemData.hp;
            currentSpeed += itemData.speed;
        }
    
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                if (other.gameObject.TryGetComponent(out Enemy enemy))
                {
                    GetDamage(enemy.damage);    
                }
            }
        }
    }
}
