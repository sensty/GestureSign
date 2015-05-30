﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using GestureSign.Common.Plugins;
using ManagedWinapi.Windows;
using System.Windows.Controls;

namespace GestureSign.CorePlugins
{
	public class PreviousApplication : IPlugin
	{
		#region Private Variables

		IHostControl _HostControl = null;

		#endregion

		#region IAction Properties

		public string Name
		{
			get { return "上一窗口"; }
		}

		public string Description
		{
			get { return "切换到上一窗口 (相当于 Shift + Alt + Tab)"; }
		}

		public UserControl GUI
		{
			get { return null; }
		}

		public string Category
		{
			get { return "Windows"; }
		}

		public bool IsAction
		{
			get { return true; }
		}

		#endregion

		#region IAction Methods

		public void Initialize()
		{

		}

		public bool Gestured(PointInfo ActionPoint)
		{
			try
			{
				SystemWindow.ForegroundWindow = SystemWindow.AllToplevelWindows.Where
												(w => w.Visible &&	// Must be a visible windows
												 w.Title != "" &&	// Must have a window title
												(w.Style & WindowStyleFlags.POPUPWINDOW)
													!= WindowStyleFlags.POPUPWINDOW &&
												(w.ExtendedStyle & WindowExStyleFlags.TOOLWINDOW)
													!= WindowExStyleFlags.TOOLWINDOW	// Must not be a tool window
												).First(w => w != SystemWindow.ForegroundWindow);
			}
			catch (InvalidOperationException)
			{
				// Do nothing here, no other window open..
			}
			catch (Exception)
			{
				//MessageBox.Show("Oops! - " + ex.ToString());
			}
			finally{}
			return true;
		}

		public bool Deserialize(string SerializedData)
        {
            return true;
			// Nothing to deserialize
		}

		public string Serialize()
		{
			// Nothing to serialize, send empty string
			return "";
		}

		public void ShowGUI(bool IsNew)
		{
			// Nothing to do here
		}

		#endregion

		#region Host Control

		public IHostControl HostControl
		{
			get { return _HostControl; }
			set { _HostControl = value; }
		}

		#endregion
	}
}