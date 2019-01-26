using UnityEngine;

namespace GGJ2019 {
	public class File : Element {
		
		private Folder folder;

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
