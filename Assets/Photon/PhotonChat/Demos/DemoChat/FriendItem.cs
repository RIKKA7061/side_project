using UnityEngine;
using UnityEngine.UI;

namespace Photon.Chat.Demo
{
	/// <summary>
	/// Friend UI item used to represent the friend status as well as message. 
	/// It aims at showing how to share health for a friend that plays on a different room than you for example.
	/// But of course the message can be anything and a lot more complex.
	/// </summary>
	public class FriendItem : MonoBehaviour
	{
		[HideInInspector]
		public string FriendId
		{
			set { this.NameLabel.text = value; }
			get { return this.NameLabel.text; }
		}

		public Text NameLabel;
		public Text StatusLabel;
		public Text Health;

		private Button statusButton; // 버튼 컴포넌트 참조를 위한 변수

		public void Awake()
		{
			this.Health.text = string.Empty;

			// 현재 오브젝트에서 Button 컴포넌트를 자동으로 찾음
			this.statusButton = this.GetComponent<Button>();

			if (this.statusButton == null)
			{
				Debug.LogWarning("Button component not found on the GameObject.");
			}
		}

		public void OnFriendStatusUpdate(int status, bool gotMessage, object message)
		{
			string _status;

			switch (status)
			{
				case 1:
					_status = "Invisible";
					break;
				case 2:
					_status = "온라인";

					// 버튼 NormalColor를 초록색으로 변경
					if (this.statusButton != null)
					{
						ColorBlock colorBlock = this.statusButton.colors;
						colorBlock.normalColor = Color.green; // 초록색으로 변경
						this.statusButton.colors = colorBlock;
					}
					break;
				case 3:
					_status = "Away";
					break;
				case 4:
					_status = "Do not disturb";
					break;
				case 5:
					_status = "Looking For Game/Group";
					break;
				case 6:
					_status = "Playing";
					break;
				default:
					_status = "Offline";
					break;
			}

			this.StatusLabel.text = _status;

			if (gotMessage)
			{
				string _health = string.Empty;
				if (message != null)
				{
					string[] _messages = message as string[];
					if (_messages != null && _messages.Length >= 2)
					{
						_health = (string)_messages[1] + "%";
					}
				}

				this.Health.text = _health;
			}
		}
	}
}
