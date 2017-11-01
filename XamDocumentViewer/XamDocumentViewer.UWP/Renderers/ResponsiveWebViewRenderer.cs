using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using XamDocumentViewer.UWP.Renderers;

[assembly: ExportRenderer(typeof(WebView), typeof(ResponsiveWebViewRenderer))]
namespace XamDocumentViewer.UWP.Renderers
{
	public class ResponsiveWebViewRenderer : WebViewRenderer
	{
		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);
			if (Control != null)
			{
				Control.Settings.IsJavaScriptEnabled = true;

			}
		}
	}
}
