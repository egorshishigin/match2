using UnityEngine;

public class RigibodyMouseDrag : MonoBehaviour
{
    [SerializeField] private float _forceAmount = 500;

    private Rigidbody _selectedRigidbody;

    private Camera _targetCamera;

    private Vector3 _originalScreenTargetPosition;

    private Vector3 _originalRigidbodyPos;

    private float _selectionDistance;

    private Item _selectedItem;

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
                _selectedItem = _selectedRigidbody.GetComponent<Item>();

                _selectedItem.CanDrag = true;

                _selectedRigidbody.isKinematic = false;

                _selectedRigidbody.constraints = RigidbodyConstraints.None;
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
            Vector3 mousePositionOffset = _targetCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _selectionDistance)) - _originalScreenTargetPosition;
            _selectedRigidbody.velocity = _forceAmount * Time.deltaTime * (_originalRigidbodyPos + mousePositionOffset - _selectedRigidbody.transform.position);
        }
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
