using UnityEngine;
using Xenon;

namespace GGJ2019 {
	public class ExtensionDefinition {
		public string shortName;
		public string fullName;
		public Sprite defaultIcon;
	}

	public class ExtensionManager : Singleton<ExtensionManager> {

		public ExtensionDefinition[] definitions;

		public ExtensionDefinition GetDefinition(string shortName) {
			for (int i = 0; i < definitions.Length; i++) {
				if (definitions[i].shortName.Equals(shortName)) {
					return definitions[i];
				}
			}
			return null;
		}

	}
}
