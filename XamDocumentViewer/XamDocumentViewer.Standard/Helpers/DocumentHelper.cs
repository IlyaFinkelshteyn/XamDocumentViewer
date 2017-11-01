using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamDocumentViewer.Standard.Abstractions;

namespace XamDocumentViewer.Standard.Helpers
{
	public class DocumentHelper
    {
		public async Task ConvertToHTML(Stream inputDocStream, string outputFileName, string inputDocumentType)
		{
			//Loads the template document

			WordDocument document = new WordDocument(inputDocStream, FormatType.Docx);

			Stream outputStream = await DependencyService.Get<ISaveAndLoad>().GetLocalFileOutputStreamAsync(outputFileName);

			//Saves the document as Html file

			document.Save(outputStream, FormatType.Html);

			//Closes the document 

			document.Close();
			outputStream.Dispose();
		}
	}
}
