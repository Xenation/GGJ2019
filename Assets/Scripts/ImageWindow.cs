using UnityEngine;
using UnityEngine.UI;

namespace GGJ2019 {
	public class ImageWindow : Window {

		public Sprite[] possibleSprites;

		private Image image;

		protected override void Awake() {
			base.Awake();
			image = GetComponent<Image>();
			image.sprite = possibleSprites[Random.Range(0, possibleSprites.Length)];
			//rectTransform.anchorMin = Vector2.zero;
			//rectTransform.anchorMax = Vector2.one;
			//rectTransform.sizeDelta = Vector2.one;
		}

	}
}
