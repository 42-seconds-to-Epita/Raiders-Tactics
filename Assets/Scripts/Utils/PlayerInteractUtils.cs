using UnityEngine;

namespace QuickStart.Utils
{
    public class PlayerInteractUtils
    {
        private static Plane plane = new Plane(Vector3.up, 0);
        
        public static Vector3 GetMouseWorldPosition() {
            float distance;
            Vector3 worldPosition = new Vector3();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out distance))
            {
                worldPosition = ray.GetPoint(distance);
            }

            return worldPosition;
        }
        
    }
}