using UnityEngine;

public class WayPoint : MonoBehaviour
    {
        private Transform _waypoint;
        private MeshRenderer _visibleRenderer;

        private void Awake()
        {
            _waypoint = GetComponent<Transform>();
            _visibleRenderer = GetComponent<MeshRenderer>();

            HideWayPoint();
        }

        private void HideWayPoint()
        {
            _visibleRenderer.enabled = false;
        }

        public Vector3 GetVector3()
        {
            return _waypoint.position;
        }
    }
