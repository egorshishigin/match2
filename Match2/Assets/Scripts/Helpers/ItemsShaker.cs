using UnityEngine;

using Helpers.Shop.Model;

namespace Helpers
{
    public class ItemsShaker : HelperBase
    {
        private float _force;

        private Transform _forcePoint;

        private float _forceRadius;

        public ItemsShaker(GameData gameData, HelperView helperView, ShopModel shopModel, float force, Transform forcePosition, float forceRadius)
            : base(gameData, helperView, shopModel)
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
                    rigidbody.AddForce(rigidbody.transform.up * _force, ForceMode.Impulse);
                }
            }
        }
    }
}