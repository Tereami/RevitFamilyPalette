using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;

namespace TestDockable3
{
	public class FamilyLoadOptions : IFamilyLoadOptions
	{
		public bool OnFamilyFound(bool familyInUse, out bool overwriteParameterValues)
		{
			TaskDialog.Show("Сообщение", "Семейство в проекте будет обновлено. Имя семейства: " + EventLoadfamily.familyPath);
			overwriteParameterValues = true;
			return true;
		}

		public bool OnSharedFamilyFound(Family sharedFamily, bool familyInUse, out FamilySource source, out bool overwriteParameterValues)
		{
			TaskDialog.Show("Сообщение", "Семейство в проекте будет обновлено. Имя семейства: " + sharedFamily.get_Name());
			overwriteParameterValues = true;
			source = 1;
			return true;
		}
	}
}
