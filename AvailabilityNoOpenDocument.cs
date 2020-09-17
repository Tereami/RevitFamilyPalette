using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;

namespace TestDockable3
{
	public class AvailabilityNoOpenDocument : IExternalCommandAvailability
	{
		public bool IsCommandAvailable(UIApplication uiApp, CategorySet b)
		{
			return uiApp.get_ActiveUIDocument() == null;
		}
	}
}
