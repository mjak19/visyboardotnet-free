using WFViKy;
using WindowsInput;
using WindowsInput.Native;

namespace WFViKy
{
	internal static class Program
	{
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			
			ApplicationConfiguration.Initialize();
			kbdMain kbd = new kbdMain();

            Application.Run(kbd);
		}
	}
}