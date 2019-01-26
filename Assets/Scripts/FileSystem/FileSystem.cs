using UnityEngine;
using Xenon;

namespace GGJ2019 {
	[RequireComponent(typeof(Folder), typeof(ExtensionManager))]
	public class FileSystem : Singleton<FileSystem> {

		public string desktopPath = "C:/Users/You/Desktop/";

		private Folder root;

		private void Awake() {
			root = GetComponent<Folder>();
		}

		private void Start() {
			Debug.Log("FILESYSTEM:\n" + root.ToString());
			Debug.Log(GetFile(desktopPath + "Edgy"));
		}

		public Element GetElement(string path) {
			path = path.Trim().Split(':')[1].Trim('/');
			string[] pathNames = path.Split('/');
			Folder currentFolder = root;
			for (int i = 0; i < pathNames.Length - 1; i++) {
				currentFolder = currentFolder.GetElement(pathNames[i]) as Folder;
				if (currentFolder == null) {
					return null;
				}
			}
			if (pathNames.Length > 0) {
				return currentFolder.GetElement(pathNames[pathNames.Length - 1]);
			}
			return null;
		}

		public Folder GetFolder(string path) {
			return GetElement(path) as Folder;
		}

		public File GetFile(string path) {
			return GetElement(path) as File;
		}

	}
}
