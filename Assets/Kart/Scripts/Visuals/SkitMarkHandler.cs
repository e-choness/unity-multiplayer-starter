using kart.Kart.Scripts.Controls;
using UnityEngine;

namespace kart.Kart.Scripts.Visuals
{
    [DisallowMultipleComponent]
    public class SkitMarkHandler : MonoBehaviour
    {
        [Header("Slip Attribute")]
        [SerializeField] private float slipThreshold = 0.4f;
        
        [Header("Skid Mark Attribute")]
        [SerializeField] private Transform skidMarkPrefab;
        [SerializeField] private float skidMarkAngle = 90.0f;
        
        // Controller references
        private DriftController _driftController;
        
        // Component storage
        private WheelCollider[] _wheelColliders;
        private Transform[] _skidMarks;
        
        void Start()
        {
            _driftController = GetComponent<DriftController>();
            _wheelColliders = GetComponentsInChildren<WheelCollider>();
            _skidMarks = new Transform[_wheelColliders.Length];
        }

        private void FixedUpdate()
        {
            for (var i = 0; i != _wheelColliders.Length; i++)
            {
                HandleSkitMarks(i);
            }
        }

        private void HandleSkitMarks(int index)
        {
            WheelHit hit;
            if (!IsHittingGround(index, out hit))
            {
                EndSkitMark(index);
                return;
            }

            if (IsSlipping(hit))
            {
                DrawSkitMark(index);
            }
            else
            {
                EndSkitMark(index);
            }
        }

        private bool IsHittingGround(int index, out WheelHit hit)
        {
            return _wheelColliders[index].GetGroundHit(out hit) && _driftController.IsGrounded();
        }

        private bool IsSlipping(WheelHit hit)
        {
            return Mathf.Abs(hit.sidewaysSlip) > slipThreshold
                   || Mathf.Abs(hit.forwardSlip) > slipThreshold;
        }

        private void DrawSkitMark(int index)
        {
            if (_skidMarks[index] is not null) return;

            _skidMarks[index] = Instantiate(skidMarkPrefab, _wheelColliders[index].transform);
            _skidMarks[index].localPosition = -Vector3.up * (_wheelColliders[index].radius * 0.9f);
            _skidMarks[index].localRotation = Quaternion.Euler(skidMarkAngle, 0.0f, 0.0f);
        }

        private void EndSkitMark(int index)
        {
            if (_skidMarks[index] is null) return;

            var holder = _skidMarks[index];
            _skidMarks[index] = null;
            holder.SetParent(null);
            holder.rotation = Quaternion.Euler(skidMarkAngle, 0.0f, 0.0f);
            Destroy(holder.gameObject, 5.0f);
        }
    }
}
