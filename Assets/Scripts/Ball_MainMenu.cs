using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Ball_MainMenu : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private GameObject centerPoint;
        
        // Rotate the ball around the centerPoint at a speed 
        private void Update()
        {
            transform.RotateAround(centerPoint.transform.position, Vector3.forward, speed * Time.deltaTime);
        }
      
    }
}