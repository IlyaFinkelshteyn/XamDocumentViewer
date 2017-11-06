using Xamarin.Forms.Platform.UWP;
using XamDocumentViewer.Droid.Renderers;
using XamDocumentViewer.Standard.Controls;

[assembly: ExportRenderer(typeof(AdControlView), typeof(AdViewRenderer))]
namespace XamDocumentViewer.Droid.Renderers
{
	public class AdViewRenderer : ViewRenderer<AdControlView, FormsTextBox>
	{
		string bannerId = "1100001579";
		//AdControl adView;
		string applicationID = "9pb4vjp3k60p";
		//void CreateNativeAdControl()
		//{
		//	if (adView != null)
		//		return;

		//	var width = 300;
		//	var height = 50;
		//	if (AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Desktop")
		//	{
		//		width = 728;
		//		height = 90;
		//	}
		//	// Setup your BannerView, review AdSizeCons class for more Ad sizes. 
		//	adView = new AdControl
		//	{
		//		ApplicationId = applicationID,
		//		AdUnitId = bannerId,
		//		HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center,
		//		VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Bottom,
		//		Height = height,
		//		Width = width
		//	};

		//}

		//protected override void OnElementChanged(ElementChangedEventArgs<AdControlView> e)
		//{
		//	base.OnElementChanged(e);
		//	if (Control == null)
		//	{
		//		CreateNativeAdControl();
		//		SetNativeControl(adView);
		//	}
		//}
	}
}