using UnityEngine;
using ECM2;
using UnityEditor;
using UnityEngine.Serialization;

namespace UnityMC
{
    /// <summary>
    /// This example shows how to implement a Cinemachine-based first person controller,
    /// using Cinemachine Virtual Camera’s 3rd Person Follow.
    ///
    /// Additionally, shows how to implement a Crouch / UnCrouch animation.
    ///
    /// Must be added to a Character.
    /// </summary>
    
    public class PlayerController : MonoBehaviour
    {
        private bool canMove_ = true;

        public bool CanMove
        { 
            set { canMove_ = value; }
        }
        [FormerlySerializedAs("inventory_")]
        [Header("Inventory")]
        [SerializeField]
        protected InventoryLogic inventoryLogic_;
        public InventoryLogic InventoryLogic
        {
            get { return inventoryLogic_; }
        }
        [Header("Cinemachine")]
        [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow.")]
        public GameObject cameraTarget;
        [Tooltip("How far in degrees can you move the camera up.")]
        public float maxPitch = 80.0f;
        [Tooltip("How far in degrees can you move the camera down.")]
        public float minPitch = -80.0f;
        
        [Space(15.0f)]
        [Tooltip("Cinemachine Virtual Camera positioned at desired crouched height.")]
        public GameObject crouchedCamera;
        [Tooltip("Cinemachine Virtual Camera positioned at desired un-crouched height.")]
        public GameObject unCrouchedCamera;
        
        [Space(15.0f)]
        [Tooltip("Mouse look sensitivity")]
        public Vector2 lookSensitivity = new Vector2(1.5f, 1.25f);
        
        // Cached Character
        
        private Character character_;
        
        // Current camera target pitch
        
        private float _cameraTargetPitch;

        private BaseBlock clickedBlock_ = null;
        RaycastHit targetRaycastHit;

        public float range;
        
        /// <summary>
        /// Add input (affecting Yaw).
        /// This is applied to the Character's rotation.
        /// </summary>
        
        public void AddControlYawInput(float value)
        {
            character_.AddYawInput(value);
        }
        
        /// <summary>
        /// Add input (affecting Pitch).
        /// This is applied to the cameraTarget's local rotation.
        /// </summary>
        
        public void AddControlPitchInput(float value, float minValue = -80.0f, float maxValue = 80.0f)
        {
            if (value == 0.0f)
                return;
            
            _cameraTargetPitch = MathLib.ClampAngle(_cameraTargetPitch + value, minValue, maxValue);
            cameraTarget.transform.localRotation = Quaternion.Euler(-_cameraTargetPitch, 0.0f, 0.0f);
        }
        
        /// <summary>
        /// When character crouches, toggle Crouched / UnCrouched cameras.
        /// </summary>
        
        private void OnCrouched()
        {
            crouchedCamera.SetActive(true);
            unCrouchedCamera.SetActive(false);
        }
        
        /// <summary>
        /// When character un-crouches, toggle Crouched / UnCrouched cameras.
        /// </summary>
        
        private void OnUnCrouched()
        {
            crouchedCamera.SetActive(false);
            unCrouchedCamera.SetActive(true);
        }

        private void Awake()
        {
            character_ = GetComponent<Character>();
        }
        
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            
            // Disable Character's rotation mode, we'll handle it here
            
            character_.SetRotationMode(Character.RotationMode.None);
        }
        
        private void OnEnable()
        {
            // Subscribe to Character events
            
            character_.Crouched += OnCrouched;
            character_.UnCrouched += OnUnCrouched;
        }
        
        private void OnDisable()
        {
            // Unsubscribe to Character events
            
            character_.Crouched -= OnCrouched;
            character_.UnCrouched -= OnUnCrouched;
        }

        private void Update()
        {
            if (!canMove_)
            {
                return;
            }
            checkTargetBlock();
            // Movement input
            
            Vector2 moveInput = new Vector2
            {
                x = Input.GetAxisRaw("Horizontal"),
                y = Input.GetAxisRaw("Vertical")
            };
            
            // Movement direction relative to Character's forward

            Vector3 movementDirection = Vector3.zero;

            movementDirection += character_.GetRightVector() * moveInput.x;
            movementDirection += character_.GetForwardVector() * moveInput.y;
            
            // Set Character movement direction

            character_.SetMovementDirection(movementDirection);
            
            // Look input

            Vector2 lookInput = new Vector2
            {
                x = Input.GetAxisRaw("Mouse X"),
                y = Input.GetAxisRaw("Mouse Y")
            };
            
            // Add yaw input, this update character's yaw rotation

            AddControlYawInput(lookInput.x * lookSensitivity.x);
            
            // Add pitch input (look up / look down), this update cameraTarget's local rotation
            
            AddControlPitchInput(lookInput.y * lookSensitivity.y, minPitch, maxPitch);
            
            // Crouch input

            if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.C))
                character_.Crouch();
            else if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.C))
                character_.UnCrouch();
            
            // Jump input

            if (Input.GetButtonDown("Jump"))
                character_.Jump();
            else if (Input.GetButtonUp("Jump"))
                character_.StopJumping();
            if (Input.GetButton("Fire1"))
            {
                tryBreakBlock();
            }

            if (Input.GetButtonDown("Fire2"))
            {
                tryToggleItem();
            }
        }

        void tryBreakBlock()
        {
            if(clickedBlock_!=null){clickedBlock_.tryBreak();}
        }
        
        void tryToggleItem()
        {
            if (clickedBlock_)
            {
                Vector3 hitDir = targetRaycastHit.point - clickedBlock_.transform.position;
                Vector3 offect = newBlockOffect(hitDir);

                Instantiate(clickedBlock_.drop_, clickedBlock_.transform.position + offect,
                    Quaternion.identity);//Test
            }
        }

        Vector3 newBlockOffect(Vector3 vec3)
        {
            print("Dir:"+vec3);
            float[] vals = new float[3];
            vals[0] = vec3.x;
            vals[1] = vec3.y;
            vals[2] = vec3.z;
            float biggestVal = Mathf.Abs(vals[0]);
            float biggestValDir = 0;
            for (int i = 0; i < 3; i++)
            {
                if (Mathf.Abs(vals[i]) > biggestVal)
                {
                    biggestVal = Mathf.Abs(vals[i]);
                    biggestValDir = i;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if (i == biggestValDir)
                {
                    if (vals[i] > 0)
                    {
                        vals[i] = 1;
                    }
                    else
                    {
                        vals[i] = -1;
                    }
                }
                else
                {
                    vals[i] = 0;
                }
            }
            return new Vector3(vals[0], vals[1], vals[2]);
        }
        
        void checkTargetBlock() {

            RaycastHit hit;
            BaseBlock targetBlock = null;
            Ray ray = character_.camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)) {

                Transform objectHit = hit.transform;

                targetRaycastHit = hit;

                if (!objectHit.GetComponent<BaseBlock>()) { return; }

                if (Vector3.Distance(transform.position, objectHit.position) > range) { return; }

                targetBlock = objectHit.GetComponent<BaseBlock>();
            }

            if (clickedBlock_ != targetBlock)
            {
                if (clickedBlock_ != null)
                {
                    clickedBlock_.OnBlockDisSelected();
                }

                if (targetBlock != null)
                {
                    targetBlock.OnBlockSelected();
                }
                clickedBlock_ = targetBlock;
            }

        }
    }
}