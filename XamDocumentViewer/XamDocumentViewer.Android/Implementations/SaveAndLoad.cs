using System.IO;
using System.Threading.Tasks;
using XamDocumentViewer.Droid.Implementations;
using XamDocumentViewer.Standard.Abstractions;

[assembly: Xamarin.Forms.Dependency(typeof(SaveAndLoad))]
namespace XamDocumentViewer.Droid.Implementations
{
	public class SaveAndLoad : ISaveAndLoad
	{
		public Task<Stream> GetLocalFileInputStreamAsync(string fileName)
		{
			string folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
			FileStream fileStream = new FileStream(Path.Combine(folderPath, fileName), FileMode.Open);
			return Task<Stream>.Factory.StartNew(() => fileStream);
		}

		public Task<Stream> GetLocalFileOutputStreamAsync(string fileName)
		{
			string folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
			FileStream fileStream = new FileStream(Path.Combine(folderPath, fileName), FileMode.Create, FileAccess.ReadWrite, FileShare.None);
			return Task<Stream>.Factory.StartNew(() => fileStream);
		}
	}
}