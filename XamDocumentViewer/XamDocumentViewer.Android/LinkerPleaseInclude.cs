using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using XamDocumentViewer.Droid.Renderers;
using Xam.FormsPlugin.Abstractions;

namespace XamDocumentViewer.Droid
{
	public class LinkerPleaseInclude
	{
		public void Include()
		{
			var x = new System.ComponentModel.ReferenceConverter(typeof(ResponsiveWebViewRenderer));
			var y = new System.ComponentModel.ReferenceConverter(typeof(AdMobView));
		}
	}
}