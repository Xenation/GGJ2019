using UnityEngine;

namespace GGJ2019 {
	public class File : Element {
		
		public string ext {
			get {
				return name.Substring(name.LastIndexOf('.') + 1);
			}
		}

		private Folder folder;

		private void Awake() {
			sprite = ExtensionManager.I.GetDefinition(ext).defaultIcon;
		}

		public void MoveTo(Folder nFolder) {
			folder.RemoveElement(this);
			nFolder.AddElement(this);
			folder = nFolder;
		}

		public override string ToString() {
			return name;
		}

	}
}
