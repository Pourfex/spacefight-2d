using MLAPI;
using MLAPI.Messaging;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player {
    public class Player : NetworkedBehaviour {
        public Transform cannonTransform;
        public GameObject bulletPrefab;

        private Rigidbody2D rb;
        private float thrust;
        private float rotation;

        private readonly float maxSpeed = 30f;
        private readonly float rotationSpeed = 500f;

        public override void NetworkStart() {
            base.NetworkStart();
            rb = GetComponent<Rigidbody2D>();

            if (IsLocalPlayer) {
                SetupControls();
            }
        }

        private void FixedUpdate() {
            if (!IsLocalPlayer) {
                return;
            }

            rb.AddRelativeForce(Vector2.up * (thrust * (Time.fixedDeltaTime * 500f)), ForceMode2D.Force);
            rb.MoveRotation(rb.rotation + rotation * rotationSpeed * Time.fixedDeltaTime);

            if (rb.velocity.magnitude > maxSpeed) {
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
            }
        }

        public void ResetPlayer() {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            transform.rotation = Quaternion.identity;
            transform.position = Vector2.zero;
        }

        //On the standalone server, the networked transform are not updated so the current position of the player (and it's cannon) has not changed since the beginning, so bullets spawn at the spawnpoint of the player.
        // We can change the networked transform behaviour to change on the server side as well
        // or we can tell the client to spawn the object for others (because he know the actual position of transform)
        // or we send in the rpc the position, rotation and velocity and change this method
        
        [ServerRPC]
        public void CmdFire(Vector3 position, Quaternion rotation, Vector2 velocity) {
            var actualRotation = transform.rotation;
            Debug.Log(bulletPrefab.name); 
            var bullet = Instantiate(bulletPrefab, position, rotation);
            var bulletScript = bullet.GetComponent<Bullet>();
            var bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.velocity = velocity;
            bulletRb.AddRelativeForce(Vector2.up * bulletScript.speed);

            bullet.GetComponent<NetworkedObject>().Spawn();
            Destroy(bullet, 1.5f);
        }

        private void SetupControls() {
            var leftRight = new InputAction("LeftRight");
            leftRight.AddCompositeBinding("Axis")
                .With("Positive", "<Keyboard>/rightArrow")
                .With("Negative", "<Keyboard>/leftArrow");
            leftRight.Enable();

            var topBottom = new InputAction("LeftRight");
            topBottom.AddCompositeBinding("Axis")
                .With("Positive", "<Keyboard>/upArrow")
                .With("Negative", "<Keyboard>/downArrow");
            topBottom.Enable();

            var fireButton = new InputAction("Fire");
            fireButton.AddBinding("<Keyboard>/space");
            fireButton.Enable();

            leftRight.started += ctx => rotation = -ctx.ReadValue<float>();
            leftRight.canceled += ctx => rotation = 0f;
            topBottom.started += ctx => thrust = ctx.ReadValue<float>();
            topBottom.canceled += ctx => thrust = 0f;
            fireButton.performed += ctx => InvokeServerRpc(CmdFire, cannonTransform.position, transform.rotation, rb.velocity);
        }
    }
}
