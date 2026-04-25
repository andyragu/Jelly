using UnityEngine;
using UnityEngine.InputSystem;

public class MouseInput : MonoBehaviour
{

    public float pressureForce;

    public float pressureOffset;

    private Ray mouseRay;
    private RaycastHit raycastHit;

    // Update is called once per frame
    void Update()
    {
        if(Mouse.current.leftButton.isPressed)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            mouseRay = Camera.main.ScreenPointToRay(mousePos);

            if(Physics.Raycast(mouseRay, out raycastHit))
            {
                Jellifier jellifier = raycastHit.collider.GetComponent<Jellifier>();
                if(jellifier != null)
                {
                    Vector3 inputPoint = raycastHit.point + (raycastHit.normal * pressureOffset);
                    jellifier.ApplyPressureToPoint(inputPoint, pressureForce);
                }
            }
        }
    }
}
