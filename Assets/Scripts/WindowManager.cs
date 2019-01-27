using System.Collections.Generic;
using UnityEngine;
using Xenon;

namespace GGJ2019 {
	public class WindowManager : Singleton<WindowManager> {

		public FolderWindow[] availableFolderWindows;
		public ImageWindow imgWindow;
		public ImageWindow webWindow;
		public ImageWindow textWindow;
		public ImageWindow pdfWindow;

		private Stack<Window> windowStack = new Stack<Window>();

		public Transform frontWindows;

		public void OpenWindow(GridIcon icon) {
			GameObject go = null;
			if (icon.isFolder) {
				go = Instantiate(availableFolderWindows[Random.Range(0, availableFolderWindows.Length)].gameObject, frontWindows);
				FolderWindow foldWin = go.GetComponent<FolderWindow>();
				windowStack.Push(foldWin);
			} else {
				ImageWindow imgWin = null;
				switch (icon.ext) {
					case "png":
					case "jpg":
					case "bmp":
						go = Instantiate(imgWindow.gameObject, frontWindows);
						break;
					case "html":
						go = Instantiate(webWindow.gameObject, frontWindows);
						break;
					case "txt":
						go = Instantiate(textWindow.gameObject, frontWindows);
						break;
					case "pdf":
						go = Instantiate(pdfWindow.gameObject, frontWindows);
						break;
				}
				imgWin = go?.GetComponent<ImageWindow>();
				windowStack.Push(imgWin);
			}
		}

		public void CloseTopWindow() {
			Destroy(windowStack.Pop().gameObject);
		}

		public bool isPlayerOnTopMostWindow() {
			FolderWindow win = windowStack.Peek() as FolderWindow;
			if (win == null) return false;
			return win.gridRect.Contains(Player.I.GridPos);
		}

		public Window GetTopWindow() {
			try {
				return windowStack.Peek();
			} catch (System.InvalidOperationException e) {
				return null;
			}
		}

		public Vector2 GetTopWindowClosePosition() {
			Window win = GetTopWindow();
			if (win == null) return Vector2.zero;
			return win.closeBtn.position;
		}

	}
}
