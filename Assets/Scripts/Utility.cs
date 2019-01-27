using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace GGJ2019 {
	public static class Utility {

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

		public static Vector2Int GetGridPos(this GridLayoutGroup grid, int childIndex) {
			int cellsPerLine = (int) ((grid.transform as RectTransform).rect.x / (grid.cellSize.x + grid.spacing.x));
			int x = childIndex % cellsPerLine;
			int y = childIndex / cellsPerLine;
			return new Vector2Int(x, y);
		}

		public static Vector2 GetCanvasPos(this GridLayoutGroup grid, Vector2Int gridPos) {
			Vector2 actualCellSize = grid.cellSize + grid.spacing;
			return new Vector2(actualCellSize.x * gridPos.x, actualCellSize.y * gridPos.y);
		}

	}
}
