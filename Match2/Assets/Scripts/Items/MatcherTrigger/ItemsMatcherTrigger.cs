using System.Linq;
using System.Collections.Generic;

using DG.Tweening;

using Items.Config;

using UnityEngine;
using UnityEngine.Events;

namespace Items.MatcherTrigger
{
    public class ItemsMatcherTrigger : MonoBehaviour
    {
        [SerializeField] private Transform _item1Position;

        [SerializeField] private Transform _item2Position;

        [SerializeField] private float _throwForce;

        [SerializeField] private ForceMode _forceMode;

        [SerializeField] private float _rotationDuration;

        [SerializeField] private AudioSource _audioSource;

        private List<Item> _items = new List<Item>();

        private Item _item;

        private Sequence _sequence;

        public UnityEvent ItemsMatch;

        public Sequence Sequence => _sequence;

        private void OnTriggerEnter(Collider other)
        {
            if (ItemCheck(other) && _items.Count < 2 && _item.CanDrag)
            {
                PlaceItem();

                _items.Add(_item);

                MatchItems();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (ItemCheck(other))
            {
                _items.Remove(_item);
            }
        }

        public void AnimateMatchItems()
        {
            _sequence = DOTween.Sequence();

            foreach (Item item in _items)
            {
                _sequence.Join(item.transform.DORotate(new Vector3(0f, -360f, 0f), _rotationDuration, RotateMode.LocalAxisAdd));
            }

            _sequence.OnComplete(() =>
            {
                _sequence.Kill();

                foreach (Item item in _items)
                {
                    item.gameObject.SetActive(false);
                }

                _items.Clear();
            });
        }

        public void ClearTriggerItems()
        {
            _items.Clear();
        }

        private void MatchItems()
        {
            if (ItemsMatchCheck(_items))
            {
                ItemsMatch?.Invoke();
            }
            else if (_items.Count > 1)
            {
                ThrowWrongItem(_item.Rigidbody);

                _audioSource.PlayOneShot(_audioSource.clip);
            }
        }

        private void PlaceItem()
        {
            if (_items.Count == 1)
            {
                _item.Rigidbody.transform.position = _item2Position.position;
            }
            else
            {
                _item.Rigidbody.transform.position = _item1Position.position;
            }

            ResetItemRigibody();
        }

        private void ResetItemRigibody()
        {
            _item.Rigidbody.transform.rotation = Quaternion.Euler(Vector3.zero);

            _item.Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }

        private bool ItemCheck(Collider collider)
        {
            if (collider.TryGetComponent<Item>(out _item))
            {
                return true;
            }
            else return false;
        }

        private bool ItemsMatchCheck(List<Item> items)
        {
            bool itemsMatch = items.Count == 2 && items.All(i => i.ID == items[0].ID);

            return itemsMatch;
        }

        private void ThrowWrongItem(Rigidbody rigidbody)
        {
            _item.CanDrag = false;

            rigidbody.constraints = RigidbodyConstraints.None;

            rigidbody.AddForce(Vector3.up + Vector3.forward * _throwForce, _forceMode);
        }
    }
}