using System;
using System.Text;

namespace GGJ2019 {
	public static class Extensions {

		public static string Indent(this string str, int indent) {
			StringBuilder sb = new StringBuilder(str);
			sb.Insert(0, "    ");
			for (int i = 0; i < sb.Length; i++) {
				if (sb[i] == '\n') {
					int si = i + 1;
					for (i = si; i < si + indent; i++) {
						sb.Insert(i, "    ");
					}
				}
			}
			return sb.ToString();
		}

	}
}
