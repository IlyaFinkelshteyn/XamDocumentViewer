using Android.Gms.Ads;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamDocumentViewer.Droid.Renderers;
using XamDocumentViewer.Standard.Controls;

[assembly: ExportRenderer(typeof(AdControlView), typeof(AdViewRenderer))]
namespace XamDocumentViewer.Droid.Renderers
{
	public class AdViewRenderer : ViewRenderer<AdControlView, AdView>
	{
		string adUnitId = string.Empty;
		//Note you may want to adjust this, see further down.
		AdSize adSize = AdSize.SmartBanner;
		AdView adView;
		AdView CreateNativeAdControl()
		{
			if (adView != null)
				return adView;

			// This is a string in the Resources/values/strings.xml that I added or you can modify it here. This comes from admob and contains a / in it
			adUnitId = Forms.Context.Resources.GetString(Resource.String.banner_ad_unit_id);
			adView = new AdView(Forms.Context);
			adView.AdSize = adSize;
			adView.AdUnitId = adUnitId;

			var adParams = new LinearLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);

			adView.LayoutParameters = adParams;

			adView.LoadAd(new AdRequest
							.Builder()
							.Build());
			return adView;
		}

		protected override void OnElementChanged(ElementChangedEventArgs<AdControlView> e)
		{
			base.OnElementChanged(e);
			if (Control == null)
			{
				CreateNativeAdControl();
				SetNativeControl(adView);
			}
		}
	}
}