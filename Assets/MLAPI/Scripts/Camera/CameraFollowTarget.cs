using UnityEngine;

namespace Camera {
    public class CameraFollowTarget : MonoBehaviour {
        
        private Vector2 offset;
        private float zOffset;
        public Transform target;

        void Awake() {
            zOffset = transform.position.z;
        }

        private void LateUpdate() {
            if (target != null) {
                var position = target.position;
                transform.position = new Vector3(position.x + offset.x, position.y + offset.y, zOffset);
            }
        }

        private void TrackPosition(GameObject target) {
            this.target = target.transform;
        }
    }
}
