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
		//마우스 위치 초기화
		mousePos = Input.mousePosition;
		mousePos = cam.ScreenToWorldPoint(mousePos);
	}

	private void LateUpdate()
	{
		Vector3 point = GetCenterPosition();
		transform.position = point;
	}

	/// <summary>
	/// 카메라가 위치할 지점을 찾아 반환하는 함수.
	/// </summary>
	/// <returns>카메라가 위치할 목표 지점</returns>
	private Vector3 GetCenterPosition()
	{
		//목표가 없으면 제자리 반환
		if (target == null)
		{
			return transform.position;
		}

		//목표와 마우스 위치를 기준으로 범위를 계산
		Bounds bounds = new Bounds(target.position, Vector3.zero);
		bounds.Encapsulate(mousePos);

		//목표 지점 계산에 필요한 방향 및 거리 계산
		Vector3 direction = (mousePos - target.position).normalized;
		float distance = Vector2.Distance(bounds.center, target.position);
		distance = Mathf.Clamp(distance, 0, maxDistance);

		//목표 지점 계산
		Vector3 point = direction * distance;
		currentPoint = Vector2.Lerp(currentPoint, point, Time.deltaTime * lerpSpeed); //Lerp를 활용하여 <현재 위치> 변수가 <목표 지점> 값으로 부드럽게 이동
		currentPoint.z = -10f;

		return target.position + currentPoint;
	}
}
