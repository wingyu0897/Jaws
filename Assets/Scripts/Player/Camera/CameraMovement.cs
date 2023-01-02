using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Transform target;
	[SerializeField]
	private float maxDistance = 3f;
	[SerializeField]
	private float lerpSpeed = 5f;
    private Vector3 mousePos;
	private Vector3 currentPoint;

	private Camera cam;

	private void Awake()
	{
		cam = GetComponent<Camera>();
		currentPoint = Vector3.zero;
	}

	private void Update()
	{
		//���콺 ��ġ �ʱ�ȭ
		mousePos = Input.mousePosition;
		mousePos = cam.ScreenToWorldPoint(mousePos);
	}

	private void LateUpdate()
	{
		Vector3 point = GetCenterPosition();
		transform.position = point;
	}

	/// <summary>
	/// ī�޶� ��ġ�� ������ ã�� ��ȯ�ϴ� �Լ�.
	/// </summary>
	/// <returns>ī�޶� ��ġ�� ��ǥ ����</returns>
	private Vector3 GetCenterPosition()
	{
		//��ǥ�� ������ ���ڸ� ��ȯ
		if (target == null)
		{
			return transform.position;
		}

		//��ǥ�� ���콺 ��ġ�� �������� ������ ���
		Bounds bounds = new Bounds(target.position, Vector3.zero);
		bounds.Encapsulate(mousePos);

		//��ǥ ���� ��꿡 �ʿ��� ���� �� �Ÿ� ���
		Vector3 direction = (mousePos - target.position).normalized;
		float distance = Vector2.Distance(bounds.center, target.position);
		distance = Mathf.Clamp(distance, 0, maxDistance);

		//��ǥ ���� ���
		Vector3 point = direction * distance;
		currentPoint = Vector2.Lerp(currentPoint, point, Time.deltaTime * lerpSpeed); //Lerp�� Ȱ���Ͽ� <���� ��ġ> ������ <��ǥ ����> ������ �ε巴�� �̵�
		currentPoint.z = -10f;

		return target.position + currentPoint;
	}
}
