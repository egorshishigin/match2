using UnityEngine;

using Helpers.Shop.Model;
using Helpers.Inventory;

namespace Helpers
{
    public class ItemsShaker : HelperBase
    {
        private float _force;

        private Transform _forcePoint;

        private float _forceRadius;

        public ItemsShaker(InventoryData inventoryData, InventoryDataIO inventoryDataIO, HelperView helperView, ShopModel shopModel, float force, Transform forcePosition, float forceRadius)
            : base(inventoryData, inventoryDataIO, helperView, shopModel)
        {
            _force = force;

            _forcePoint = forcePosition;

            _forceRadius = forceRadius;
        }

        protected override void UseHelper()
        {
            Collider[] colliders = Physics.OverlapSphere(_forcePoint.position, _forceRadius);

            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
                {
                    Vector3 direction = (collider.transform.position - _forcePoint.position).normalized;

                    rigidbody.AddForce(direction * _force, ForceMode.Impulse);
                }
            }

            Debug.Log("Items shaker used");
        }
    }
}