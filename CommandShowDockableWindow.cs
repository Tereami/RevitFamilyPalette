using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;

namespace RevitFamilyPalette
{
	[Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
	public class CommandShowDockableWindow : IExternalCommand
	{
		public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
		{
			DockablePaneId dockablePaneId = new DockablePaneId(Application.paneGuid);
			DockablePane dockablePane = commandData.Application.GetDockablePane(dockablePaneId);
			dockablePane.Show();
			return 0;
		}
	}
}
