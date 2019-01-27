using System.Collections.Generic;
using UnityEngine;
using Xenon;

namespace GGJ2019 {
	public class WindowManager : Singleton<WindowManager> {

		public FolderWindow[] availableFolderWindows;
		public ImageWindow imgWindow;
		public ImageWindow webWindow;
		public ImageWindow textWindow;

		private Stack<Window> windowStack = new Stack<Window>();

		public void OpenWindow(Element element) {
			GameObject go = null;
			switch (element) {
				case Folder folder:
					go = Instantiate(availableFolderWindows[Random.Range(0, availableFolderWindows.Length)].gameObject, transform);
					FolderWindow foldWin = go.GetComponent<FolderWindow>();
					windowStack.Push(foldWin);
					break;
				case File file:
					ImageWindow imgWin = null;
					switch (file.ext) {
						case "png":
						case "jpg":
						case "bmp":
							go = Instantiate(imgWindow.gameObject, transform);
							break;
						case "html":
							go = Instantiate(webWindow.gameObject, transform);
							break;
						case "txt":
							go = Instantiate(textWindow.gameObject, transform);
							break;
					}
					imgWin = go?.GetComponent<ImageWindow>();
					windowStack.Push(imgWin);
					break;
			}
		}

		public void CloseTopWindow() {
			Destroy(windowStack.Pop());
		}

		public bool isPlayerOnTopMostWindow() {
			FolderWindow win = windowStack.Peek() as FolderWindow;
			if (win == null) return false;
			return win.gridRect.Contains(Player.I.GridPos);
		}

		public Window GetTopWindow() {
			return windowStack.Peek();
		}

		public Vector2 GetTopWindowClosePosition() {
			Window win = GetTopWindow();
			if (win == null) return Vector2.zero;
			return win.closeBtn.position;
		}

	}
}
