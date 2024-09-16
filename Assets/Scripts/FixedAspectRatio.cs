using UnityEngine;

public class FixedAspectRatio : MonoBehaviour
{
	public float targetAspectRatio = 16.0f / 9.0f; // 목표 가로 세로 비율

	void Start()
	{
		// 카메라 가져오기
		Camera camera = GetComponent<Camera>();

		// 화면 비율 계산
		float aspectRatio = (float)Screen.width / Screen.height;

		// 카메라 Rect 조절
		if (aspectRatio > targetAspectRatio)
		{
			// 화면이 너무 넓을 경우
			float scaleheight = targetAspectRatio / aspectRatio;
			Rect rect = camera.rect;
			rect.width = 1.0f;
			rect.height = scaleheight;
			rect.y = (1.0f - scaleheight) / 2.0f;
			camera.rect = rect;
		}
		else
		{
			// 화면이 너무 높을 경우
			float scalewidth = aspectRatio / targetAspectRatio;
			Rect rect = camera.rect;
			rect.height = 1.0f;
			rect.width = scalewidth;
			rect.x = (1.0f - scalewidth) / 2.0f;
			camera.rect = rect;
		}
	}
}