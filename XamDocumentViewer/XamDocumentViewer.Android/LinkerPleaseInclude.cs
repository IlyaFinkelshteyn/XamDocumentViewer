using Android.Gms.Ads;
using XamDocumentViewer.Droid.Renderers;

namespace XamDocumentViewer.Droid
{
	public class LinkerPleaseInclude
	{
		public void Include()
		{
			var x = new System.ComponentModel.ReferenceConverter(typeof(ResponsiveWebViewRenderer));
			var y = new System.ComponentModel.ReferenceConverter(typeof(AdView));
		}
	}
}