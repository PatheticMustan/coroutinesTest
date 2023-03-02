using UnityEngine;

public class CameraMovement : MonoBehaviour {
	[Range(0.1f, 9f)] public float sensitivity = 2f;
	[Range(0f, 90f)] public float yRotationLimit = 88f;
	public float mouseX, mouseY;

	private void Start() {
		mouseX = 0;
		mouseY = 0;
	}

	void Update() {
		mouseX += Input.GetAxis("Mouse X") * sensitivity;
		mouseY += Input.GetAxis("Mouse Y") * sensitivity;
		mouseY = Mathf.Clamp(mouseY, -yRotationLimit, yRotationLimit);

		Quaternion xQuat = Quaternion.AngleAxis(mouseX, Vector3.up);
		Quaternion yQuat = Quaternion.AngleAxis(mouseY, Vector3.left);

		transform.localRotation = xQuat * yQuat;
	}
}