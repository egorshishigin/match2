using Items.Config;

using Pause;

using UnityEngine;

namespace PlayerInput
{
    public class RigibodyMouseDrag : MonoBehaviour, IPuaseHandler
    {
        [SerializeField] private float _forceAmount = 500;

        [SerializeField] private float _heightOffset;

        private Rigidbody _selectedRigidbody;

        private Camera _targetCamera;

        private Vector3 _originalScreenTargetPosition;

        private Vector3 _originalRigidbodyPos;

        private float _selectionDistance;

        private Item _selectedItem;

        private void Awake()
        {
            Game.Instance.PauseManager.Register(this);
        }

        private void Start()
        {
            _targetCamera = GetComponent<Camera>();
        }

        private void Update()
        {
            if (!_targetCamera)
                return;

            if (Input.GetMouseButtonDown(0))
            {
                _selectedRigidbody = GetRigidbodyFromMouseClick();

                if (_selectedRigidbody != null)
                {
                    GetItemComponent();

                    SetUpRigibody(_selectedRigidbody);
                }
            }

            if (Input.GetMouseButtonUp(0) && _selectedRigidbody)
            {
                _selectedRigidbody = null;
            }
        }

        private void FixedUpdate()
        {
            if (_selectedRigidbody && _selectedItem.CanDrag)
            {
                DragItem();
            }
        }

        public void SetPause(bool paused)
        {
            if (this == null)
                return;

            enabled = paused ? false : true;
        }

        private void DragItem()
        {
            Vector3 yOffset = new Vector3(0, _heightOffset, 0);

            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _selectionDistance);

            Vector3 mousePositionOffset = _targetCamera.ScreenToWorldPoint(mousePosition) - _originalScreenTargetPosition;

            Vector3 VelocityDirection = _originalRigidbodyPos + mousePositionOffset + yOffset - _selectedRigidbody.transform.position;

            _selectedRigidbody.velocity = _forceAmount * Time.fixedDeltaTime * VelocityDirection;
        }

        private void SetUpRigibody(Rigidbody rigidbody)
        {
            rigidbody.isKinematic = false;

            rigidbody.constraints = RigidbodyConstraints.None;
        }

        private void GetItemComponent()
        {
            _selectedItem = _selectedRigidbody.GetComponent<Item>();

            _selectedItem.CanDrag = true;
        }

        private Rigidbody GetRigidbodyFromMouseClick()
        {
            RaycastHit hitInfo = new RaycastHit();

            Ray ray = _targetCamera.ScreenPointToRay(Input.mousePosition);

            bool hit = Physics.Raycast(ray, out hitInfo);

            if (hit)
            {
                if (hitInfo.collider.gameObject.GetComponent<Rigidbody>())
                {
                    _selectionDistance = Vector3.Distance(ray.origin, hitInfo.point);

                    _originalScreenTargetPosition = _targetCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _selectionDistance));

                    _originalRigidbodyPos = hitInfo.collider.transform.position;

                    return hitInfo.collider.gameObject.GetComponent<Rigidbody>();
                }
            }

            return null;
        }
    }
}