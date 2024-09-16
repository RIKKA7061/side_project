using UnityEngine;

public class FixedAspectRatio : MonoBehaviour
{
	public float targetAspectRatio = 16.0f / 9.0f; // ��ǥ ���� ���� ����

	void Start()
	{
		// ī�޶� ��������
		Camera camera = GetComponent<Camera>();

		// ȭ�� ���� ���
		float aspectRatio = (float)Screen.width / Screen.height;

		// ī�޶� Rect ����
		if (aspectRatio > targetAspectRatio)
		{
			// ȭ���� �ʹ� ���� ���
			float scaleheight = targetAspectRatio / aspectRatio;
			Rect rect = camera.rect;
			rect.width = 1.0f;
			rect.height = scaleheight;
			rect.y = (1.0f - scaleheight) / 2.0f;
			camera.rect = rect;
		}
		else
		{
			// ȭ���� �ʹ� ���� ���
			float scalewidth = aspectRatio / targetAspectRatio;
			Rect rect = camera.rect;
			rect.height = 1.0f;
			rect.width = scalewidth;
			rect.x = (1.0f - scalewidth) / 2.0f;
			camera.rect = rect;
		}
	}
}