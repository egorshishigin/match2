using UnityEngine;

namespace Items.Config
{
    [RequireComponent(typeof(Rigidbody))]
    public class Item : MonoBehaviour
    {
        [SerializeField] private int _id;

        [SerializeField] private Rigidbody _rigidbody;

        [SerializeField] private LayerMask _dropLayer;

        public int ID => _id;

        public Rigidbody Rigidbody => _rigidbody;

        public bool CanDrag { get; set; }


        private void OnCollisionEnter(Collision collision)
        {
            if((_dropLayer & (1 << collision.gameObject.layer)) != 0)
            {
                CanDrag = false;
            }
        }
    }
}