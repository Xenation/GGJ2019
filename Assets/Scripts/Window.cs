using UnityEngine;

namespace GGJ2019 {
	public class Window : MonoBehaviour {
		
		[System.NonSerialized] public RectTransform rectTransform;
		[System.NonSerialized] public RectTransform closeBtn;

		protected virtual void Awake() {
			rectTransform = GetComponent<RectTransform>();
			closeBtn = rectTransform.Find("Close").GetComponent<RectTransform>();
		}

	}
}
