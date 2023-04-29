using Items.Points;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Point Saver", menuName = "ScriptableObjects/Point Saver")]
    public class PointSaver : ScriptableObject
    {
        [SerializeField] private Vector3 savedCheckPoint;

        public Vector3 SavedCheckPoint { get => savedCheckPoint; set => savedCheckPoint = value; }

        public void SaveCheckPoint(CheckPoint checkPoint)
        {
            savedCheckPoint = checkPoint.transform.position;
        }

        public void EraseCheckPoint()
        {
            savedCheckPoint = Vector3.zero;
        }
    }
}