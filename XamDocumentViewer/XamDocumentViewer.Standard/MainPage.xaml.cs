using System.IO;
using Xamarin.Forms;
using XamDocumentViewer.Standard.Abstractions;
using XamDocumentViewer.Standard.Dtos;
using XamDocumentViewer.Standard.Helpers;

namespace XamDocumentViewer.Standard
{
	public partial class MainPage : ContentPage
	{
		private Stream mFileStream;
		private string mDocumentType;
		private string mHtmlFileName;

		public MainPage()
		{
			InitializeComponent();
		}

		public MainPage(DocumentDetails documentDetails)
		{
			mFileStream = documentDetails.Stream;
			mDocumentType = documentDetails.Type;
			InitializeComponent();
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			LoadingSpinner.IsRunning = true;
			LoadingSpinner.IsVisible = true;

			if (mFileStream != null)
			{
				mHtmlFileName = "Test.html";

				DocumentHelper documentHelper = new DocumentHelper();
				await documentHelper.ConvertToHTML(mFileStream, mHtmlFileName, mDocumentType);
				mFileStream.Dispose();

				Stream htmlReaderStream = await DependencyService.Get<ISaveAndLoad>().GetLocalFileInputStreamAsync(mHtmlFileName);

				StreamReader streamReader = new StreamReader(htmlReaderStream);
				string htmlContent = await streamReader.ReadToEndAsync();
				streamReader.Dispose();

				webView.Source = new HtmlWebViewSource
				{
					Html = htmlContent
				};
			}
			else
			{
				webView.Source = new HtmlWebViewSource
				{
					Html = "<center>Choose file directly from file browser and choose open with My Document Viewer to open the file.</center>"
				};
			}
			LoadingSpinner.IsRunning = false;
			LoadingSpinner.IsVisible = false;
		}
	}
}
