using System.IO;
using System.Threading.Tasks;

namespace XamDocumentViewer.Standard.Abstractions
{
	public interface ISaveAndLoad
    {
		Task<Stream> GetLocalFileOutputStreamAsync(string fileName);

		Task<Stream> GetLocalFileInputStreamAsync(string fileName);
	}
}
