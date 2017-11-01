using System;

using Xamarin.Forms;
using XamDocumentViewer.Standard.Dtos;

namespace XamDocumentViewer.Standard
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			MainPage = new XamDocumentViewer.Standard.MainPage();
		}
		public App(DocumentDetails documentDetails)
		{
			InitializeComponent();

			if (documentDetails.Type.Equals(".Doc", StringComparison.CurrentCultureIgnoreCase))
			{
				documentDetails.Type = "Doc";
				MainPage = new XamDocumentViewer.Standard.MainPage(documentDetails);
			}
			else if (documentDetails.Type.Equals(".Docx", StringComparison.CurrentCultureIgnoreCase))
			{
				documentDetails.Type = "Docx";
				MainPage = new XamDocumentViewer.Standard.MainPage(documentDetails);
			}
			else
			{
				throw new Exception("Invalid document type.");
			}
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
