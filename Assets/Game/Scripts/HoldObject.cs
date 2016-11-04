using UnityEngine;
using System.Collections;

public class HoldObject : MonoBehaviour {

	float drag = 1.0f;
	float angularDrag = 5.0f;
	bool attachToCenterOfMass = false;

	private FixedJoint fixedJoint;

	void Update() {

		// Make sure the user pressed the mouse down
		if (!Input.GetMouseButtonDown (0))
			return;

		var mainCamera = FindCamera();

		// We need to actually hit an object
		RaycastHit hit;
		if (!Physics.Raycast(mainCamera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2)), out hit, 100))
			return;

		// We need to hit a rigidbody that is not kinematic
		if (!hit.rigidbody || hit.rigidbody.isKinematic)
			return;

		if (!fixedJoint) {

			GameObject go = new GameObject("Rigidbody dragger");
			Rigidbody body = go.AddComponent<Rigidbody>() as Rigidbody;
			fixedJoint = go.AddComponent<FixedJoint>() as FixedJoint;
			body.isKinematic = true;
		}

		fixedJoint.transform.position = hit.point;
		if (attachToCenterOfMass) {

			Vector3 anchor = transform.TransformDirection(hit.rigidbody.centerOfMass) + hit.rigidbody.transform.position;
			anchor = fixedJoint.transform.InverseTransformPoint(anchor);
			fixedJoint.anchor = anchor;
		} else {
			fixedJoint.anchor = Vector3.zero;
		}

		fixedJoint.connectedBody = hit.rigidbody;

		StartCoroutine("DragObject", hit.distance);
	}

	IEnumerator DragObject (float distance) {

		float oldDrag = fixedJoint.connectedBody.drag;
		float oldAngularDrag = fixedJoint.connectedBody.angularDrag;

		fixedJoint.connectedBody.drag = drag;
		fixedJoint.connectedBody.angularDrag = angularDrag;

		Camera mainCamera = FindCamera();

		while (Input.GetMouseButton (0)) {

			var ray = mainCamera.ScreenPointToRay (new Vector2(Screen.width / 2, Screen.height / 2));
			fixedJoint.transform.position = ray.GetPoint(distance);
			yield return null;
		}

		if (fixedJoint.connectedBody) {

			fixedJoint.connectedBody.drag = oldDrag;
			fixedJoint.connectedBody.angularDrag = oldAngularDrag;
			fixedJoint.connectedBody = null;
		}
	}

	Camera FindCamera () {
		if (GetComponent<Camera>())
			return GetComponent<Camera>();
		else
			return Camera.main;
	}
}
