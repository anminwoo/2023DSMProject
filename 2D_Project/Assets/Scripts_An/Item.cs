using UnityEngine;

namespace Scripts_An
{
    public class Item : MonoBehaviour
    {
        public ItemData itemData;
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                var player = other.gameObject.GetComponent<PlayerController>();
                player.ChangeStatus(itemData);
            }
        }
    }
}
