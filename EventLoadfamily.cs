using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RevitFamilyPalette
{
	public class EventLoadfamily : IExternalEventHandler
	{
		public static string familyPath;

		public void Execute(UIApplication app)
		{
			Document document = app.ActiveUIDocument.Document;
			Family family = null;
			bool flag = false;
			using (Transaction transaction = new Transaction(document))
			{
				transaction.Start("LoadFamily");
				flag = document.LoadFamily(EventLoadfamily.familyPath, new FamilyLoadOptions(), out family);
				transaction.Commit();
			}
			bool flag2 = !flag;
			if (flag2)
			{
				string famName = Path.GetFileNameWithoutExtension(EventLoadfamily.familyPath);
				List<Family> list = (from i in new FilteredElementCollector(document).OfClass(typeof(Family)).WhereElementIsNotElementType()
				where i.Name.Equals(famName)
				select i).Cast<Family>().ToList<Family>();
				bool flag3 = list.Count == 0;
				if (flag3)
				{
					return;
				}
				family = list.First<Family>();
			}
			FamilySymbol familySymbol = null;
			using (IEnumerator<ElementId> enumerator = family.GetFamilySymbolIds().GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					ElementId current = enumerator.Current;
					familySymbol = (document.GetElement(current) as FamilySymbol);
				}
			}
			app.ActiveUIDocument.PostRequestForElementTypePlacement(familySymbol);
		}

		public string GetName()
		{
			return "Load family event";
		}
	}

	public class FamilyLoadOptions : IFamilyLoadOptions
	{
		public bool OnFamilyFound(bool familyInUse, out bool overwriteParameterValues)
		{
			overwriteParameterValues = false;
			return true;
		}

		public bool OnSharedFamilyFound(Family sharedFamily, bool familyInUse, out FamilySource source, out bool overwriteParameterValues)
		{
			source = FamilySource.Family;
			overwriteParameterValues = false;
			return true;
		}
	}
}
