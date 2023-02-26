using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace QuickStart
{
    public class PlayerScript : NetworkBehaviour
    {
        public float normalSpeed;
        public float fastSpeed;

        public float movementSpeed;
        public float movementTime;
        public float rotationAmount;
        public Vector3 zoomAmount;


        public Vector3 newPosition;
        public Quaternion newRotation;
        public Vector3 newZoom;


        public override void OnStartLocalPlayer()
        {
            Camera.main.transform.SetParent(transform);
            Camera.main.transform.localPosition = new Vector3(0, 0, 0);
            newPosition = transform.position;
            newRotation = transform.rotation;
            newZoom = Camera.main.transform.localPosition;
        }

        void Update()
        {
            if (!isLocalPlayer)
            {
                return;
            }

            HandleMovementInput();
        }

        void HandleMovementInput()
        {
            //Movement speed
            if (Input.GetKey(KeyCode.LeftShift))
            {
                movementSpeed = fastSpeed;
            }
            else
            {
                movementSpeed = normalSpeed;
            }

            //Translation
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                newPosition += (transform.forward * movementSpeed);
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                newPosition += (transform.forward * -movementSpeed);
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                newPosition += (transform.right * movementSpeed);
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                newPosition += (transform.right * -movementSpeed);
            }


            //Rotation
            if (Input.GetKey(KeyCode.Q))
            {
                newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
            }

            if (Input.GetKey(KeyCode.E))
            {
                newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
            }

            //Zoom
            if (Input.GetKey(KeyCode.R))
            {
                newZoom += zoomAmount;
            }

            if (Input.GetKey(KeyCode.F))
            {
                newZoom += -zoomAmount;
            }

            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
            Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, newZoom,
                Time.deltaTime * movementTime);
        }
    }
}