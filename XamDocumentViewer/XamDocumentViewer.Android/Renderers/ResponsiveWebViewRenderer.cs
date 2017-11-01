using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamDocumentViewer.Droid.Renderers;

[assembly: ExportRenderer(typeof(WebView), typeof(ResponsiveWebViewRenderer))]
namespace XamDocumentViewer.Droid.Renderers
{
	public class ResponsiveWebViewRenderer : WebViewRenderer
	{
		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);
			if (Control != null)
			{
				Control.Settings.BuiltInZoomControls = true;
				Control.Settings.DisplayZoomControls = false;

				Control.Settings.LoadWithOverviewMode = true;
				Control.Settings.UseWideViewPort = true;
			}
		}
	}
}