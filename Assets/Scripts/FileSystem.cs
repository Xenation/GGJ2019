using UnityEngine;
using Xenon;

namespace GGJ2019 {
	[RequireComponent(typeof(Folder))]
	public class FileSystem : Singleton<FileSystem> {

		private Folder root;

		private void Awake() {
			root = GetComponent<Folder>();
		}

		private void Start() {
			Debug.Log(root.ToString());
		}

	}
}
