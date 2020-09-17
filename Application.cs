using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;

using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace RevitFamilyPalette
{
	[Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
	public class Application : IExternalApplication
	{
		public static string assemblyPath = "";

		public static string iconsPath = "";

		public static Guid paneGuid = new Guid("{23765a2e-e050-42fa-a7f1-04ab19cfd055}");

		private DockablePage m_window = null;

		public Result OnStartup(UIControlledApplication uiControlledApp)
		{
			Application.assemblyPath = typeof(Application).Assembly.Location;
			Application.iconsPath = Path.Combine(Path.GetDirectoryName(Application.assemblyPath), "icons");
			string text = "Weandrevit";
			try
			{
				uiControlledApp.CreateRibbonTab(text);
			}
			catch
			{
			}
			RibbonPanel ribbonPanel = uiControlledApp.CreateRibbonPanel(text, "BIM Library");
			PushButton pushButton = ribbonPanel.AddItem(new PushButtonData("ShowCatalog", "Показать\nпанель", Application.assemblyPath, "TestDockable3.CommandShowDockableWindow")) as PushButton;
			pushButton.LargeImage = new BitmapImage(new Uri(Path.Combine(Application.iconsPath, "catalog.png")));
			pushButton.ToolTip = "Панель BIM-Library для загрузки и установки семейств в проекте";
			this.RegisterWindow(uiControlledApp);
			return 0;
		}

		public Result OnShutdown(UIControlledApplication application)
		{
			return 0;
		}

		private void RegisterWindow(UIControlledApplication app)
		{
			EventLoadfamily eventLoadfamily = new EventLoadfamily();
			ExternalEvent exEvent = ExternalEvent.Create(eventLoadfamily);
			DockablePaneProviderData dockablePaneProviderData = new DockablePaneProviderData();
			DockablePage dockablePage = new DockablePage(exEvent, eventLoadfamily);
			this.m_window = dockablePage;
			dockablePaneProviderData.FrameworkElement = dockablePage;
			dockablePaneProviderData.InitialState = new DockablePaneState();
			dockablePaneProviderData.InitialState.DockPosition = DockPosition.Left;;
			dockablePaneProviderData.InitialState.TabBehind = DockablePanes.BuiltInDockablePanes.ProjectBrowser;;
			DockablePaneId dockablePaneId = new DockablePaneId(Application.paneGuid);
			app.RegisterDockablePane(dockablePaneId, "BIM-Library", dockablePage);
			app.ViewActivated += new EventHandler<ViewActivatedEventArgs>(this.Application_ViewActivated);
		}

		private void Application_ViewActivated(object sender, ViewActivatedEventArgs args)
		{
			this.m_window.LabelTitle.Text = args.Document.Title;
		}
	}
}
