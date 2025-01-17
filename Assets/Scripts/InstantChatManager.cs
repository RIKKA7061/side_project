using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI; // ScrollRect, LayoutGroup 관련 클래스를 사용하려면 필요

public class InstantChatManager : MonoBehaviour
{
	public TMP_InputField inputField; // 입력창
	public GameObject textPrefab; // 텍스트 프리펩
	public RectTransform content; // 채팅창
	public ScrollRect scrollRect; // ScrollRect 컴포넌트 (스크롤을 가능하게 하기 위해 추가)
	private float spacing = 10f; // 텍스트 간의 기본 간격

	void Start()
	{
		// 채팅창에 VerticalLayoutGroup을 추가
		VerticalLayoutGroup layoutGroup = content.gameObject.AddComponent<VerticalLayoutGroup>();
		layoutGroup.spacing = spacing; // 텍스트 간의 간격 설정
		layoutGroup.childForceExpandHeight = false; // 텍스트 높이가 강제로 늘어나지 않도록 설정
		layoutGroup.childAlignment = TextAnchor.UpperCenter; // 텍스트 정렬을 위쪽으로 설정

		// VerticalLayoutGroup의 padding을 설정하여 여백을 조정 (시작 높이를 조금 낮추기)
		layoutGroup.padding = new RectOffset(0, 0, 20, 20); // 상단 여백을 20으로 설정

		// ScrollRect 컴포넌트 설정 (스크롤을 가능하게 하기 위해 설정)
		if (scrollRect == null)
		{
			scrollRect = content.GetComponentInParent<ScrollRect>(); // ScrollRect가 부모 객체에 있다고 가정
		}
	}

	public void OnClick_InstantChat() // 보내기 클릭
	{
		string userInput = inputField.text; // 입력
		if (string.IsNullOrEmpty(userInput)) return; // 공백 시 건너뛰기

		string formattedTime = DateTime.Now.ToString("tt hh:mm"); // 시간 포맷

		// 새로운 텍스트 프리펩 생성
		GameObject newTextObject = Instantiate(textPrefab, content);

		// 텍스트 설정
		TMP_Text tmpText = newTextObject.GetComponent<TMP_Text>();
		tmpText.text = $"{userInput} ({formattedTime})";

		// 텍스트의 실제 높이를 측정하여 content의 높이를 조정
		AdjustContentHeight(newTextObject);

		// 텍스트가 추가된 후 자동으로 스크롤을 아래로 이동
		ScrollToBottom();
	}

	private void AdjustContentHeight(GameObject newTextObject)
	{
		// 텍스트 프리펩의 RectTransform을 가져온 후, 높이를 계산합니다.
		RectTransform textRect = newTextObject.GetComponent<RectTransform>();
		TMP_Text tmpText = newTextObject.GetComponent<TMP_Text>();

		// TMP_Text의 preferredHeight를 사용하여 정확한 높이를 계산합니다.
		float textHeight = tmpText.preferredHeight;

		// content의 RectTransform을 가져와서 높이를 설정
		RectTransform contentRect = content.GetComponent<RectTransform>();
		float currentHeight = contentRect.rect.height;
		float newHeight = currentHeight + textHeight + spacing; // 텍스트 높이 + 간격 만큼 높이 증가

		// Set the new height for content, while ensuring no overflow occurs.
		contentRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, newHeight);

		// Layout update to ensure proper calculation of heights
		LayoutRebuilder.ForceRebuildLayoutImmediate(contentRect);
	}


	// ScrollRect를 사용하여 자동으로 맨 아래로 스크롤
	private void ScrollToBottom()
	{
		// 스크롤을 맨 아래로 이동시킴
		scrollRect.verticalNormalizedPosition = 0f; // 스크롤을 0으로 설정하면 가장 아래로 이동
	}
}
