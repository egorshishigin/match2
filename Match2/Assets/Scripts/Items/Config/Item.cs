using UnityEngine;

namespace Items.Config
{
    [RequireComponent(typeof(Rigidbody))]
    public class Item : MonoBehaviour
    {
        [SerializeField] private int _id;

        [SerializeField] private Rigidbody _rigidbody;

        public int ID => _id;

        public Rigidbody Rigidbody => _rigidbody;

        public bool CanDrag { get; set; }
    }
}