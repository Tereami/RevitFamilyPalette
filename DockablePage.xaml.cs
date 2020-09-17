using Autodesk.Revit.UI;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;

namespace RevitFamilyPalette
{
	public partial class DockablePage : Page, IDockablePaneProvider, IComponentConnector, IStyleConnector
	{
		private Guid m_targetGuid;

		private DockPosition m_position = DockPosition.Left;

		private int m_left = 1;

		private int m_rigth = 1;

		private int m_top = 1;

		private int m_bottom = 1;

		private ExternalEvent m_exEvent;

		private EventLoadfamily m_Handler;

		//internal DockablePage MyDockableDialog;

		//internal TextBlock LabelTitle;

		//internal System.Windows.Controls.TabControl tabControl1;

		//internal System.Windows.Controls.Button AddFamilies;

		//internal System.Windows.Controls.Button buttonRefresh;

		//private bool _contentLoaded;

		public Dictionary<string, ObservableCollection<FamilyFileInfo>> dict
		{
			get;
			set;
		}

		public DockablePage(ExternalEvent exEvent, EventLoadfamily handler)
		{
			this.InitializeComponent();
			this.m_exEvent = exEvent;
			this.m_Handler = handler;
		}

		public void SetupDockablePane(DockablePaneProviderData data)
		{
			data.FrameworkElement = this;
			data.InitialState = new DockablePaneState();
			data.InitialState.DockPosition = DockPosition.Left;
			data.InitialState.TabBehind = DockablePanes.BuiltInDockablePanes.ProjectBrowser;
		}

		public void SetInitialDockingPatameters(int left, int rigth, int top, int bottom, DockPosition position, Guid targetGuid)
		{
			this.m_position = position;
			this.m_left = left;
			this.m_rigth = rigth;
			this.m_top = top;
			this.m_bottom = bottom;
			this.m_targetGuid = targetGuid;
		}

		private void MyDockableDialog_Loaded(object sender, RoutedEventArgs e)
		{
		}

		private void AddFamilies_Click(object sender, RoutedEventArgs e)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			bool flag = folderBrowserDialog.ShowDialog() != DialogResult.OK;
			if (!flag)
			{
				string selectedPath = folderBrowserDialog.SelectedPath;
				this.dict = FilesScaner.GetInfo(selectedPath);
				this.tabControl1.ItemsSource = this.dict;
			}
		}

		private void ListBox_MouseUp(object sender, MouseButtonEventArgs e)
		{
			System.Windows.Controls.ListView listView = sender as System.Windows.Controls.ListView;
			FamilyFileInfo familyFileInfo = (FamilyFileInfo)listView.SelectedItem;
			EventLoadfamily.familyPath = familyFileInfo.FilePath;
			this.m_exEvent.Raise();
		}

		private void buttonRefresh_Click(object sender, RoutedEventArgs e)
		{
			this.dict.Clear();
		}

		//[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
		//public void InitializeComponent()
		//{
		//	bool contentLoaded = this._contentLoaded;
		//	if (!contentLoaded)
		//	{
		//		this._contentLoaded = true;
		//		Uri resourceLocator = new Uri("/TestDockable3;component/dockablepage.xaml", UriKind.Relative);
		//		System.Windows.Application.LoadComponent(this, resourceLocator);
		//	}
		//}

		//[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		//void IComponentConnector.Connect(int connectionId, object target)
		//{
		//	switch (connectionId)
		//	{
		//	case 1:
		//		this.MyDockableDialog = (DockablePage)target;
		//		this.MyDockableDialog.Loaded += new RoutedEventHandler(this.MyDockableDialog_Loaded);
		//		return;
		//	case 2:
		//		this.LabelTitle = (TextBlock)target;
		//		return;
		//	case 3:
		//		this.tabControl1 = (System.Windows.Controls.TabControl)target;
		//		return;
		//	case 5:
		//		this.AddFamilies = (System.Windows.Controls.Button)target;
		//		this.AddFamilies.Click += new RoutedEventHandler(this.AddFamilies_Click);
		//		return;
		//	case 6:
		//		this.buttonRefresh = (System.Windows.Controls.Button)target;
		//		this.buttonRefresh.Click += new RoutedEventHandler(this.buttonRefresh_Click);
		//		return;
		//	}
		//	this._contentLoaded = true;
		//}

		//[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		//void IStyleConnector.Connect(int connectionId, object target)
		//{
		//	if (connectionId == 4)
		//	{
		//		((System.Windows.Controls.ListView)target).MouseUp += new MouseButtonEventHandler(this.ListBox_MouseUp);
		//	}
		//}
	}
}
