using UnityEngine;

namespace kart.Kart.Scripts.Controls
{
    [DisallowMultipleComponent]
    public class BankController : MonoBehaviour
    {
        [Header("Banking Attributes")] 
        [SerializeField] private float maxBankAngle = 5.0f;
        [SerializeField] private float bankSpeed = 2.0f;

        // Component references
        private KartController _kartController;
        
        private void Awake()
        {
            _kartController = GetComponent<KartController>();
        }

        private void FixedUpdate()
        {
            UpdateBanking();
        }
        
        private void UpdateBanking()
        {
            var horizontalInput = _kartController.GetMoveInput().x;
            var targetBankAngle = -maxBankAngle * horizontalInput;
            var currentEuler = transform.localEulerAngles;
            currentEuler.z = Mathf.LerpAngle(currentEuler.z, targetBankAngle, Time.deltaTime * bankSpeed);
            transform.localEulerAngles = currentEuler;
        }
    }
}