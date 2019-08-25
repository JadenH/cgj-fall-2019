// Smooth towards the target

using UnityEngine;
using System.Collections;
namespace Player
{
    public class smooth_follow : MonoBehaviour
    {
        public Transform target;
        public float smoothTime = 0.3F;
        private Vector3 velocity = Vector3.zero;
        public CaracterController player;
        public float dist;
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<CaracterController>(); ;
        }

        void Update()
        {
            dist = Vector3.Distance(target.position, transform.position);
            if (dist > 1.2f)
            {
                player.LockMovment();
            }
            else
            {
                player.UnlockMovment();
            }
            // Define a target position above and behind the target transform
            Vector3 targetPosition = target.TransformPoint(new Vector3(0, 0, -1));

            // Smoothly move the camera towards that target position
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}