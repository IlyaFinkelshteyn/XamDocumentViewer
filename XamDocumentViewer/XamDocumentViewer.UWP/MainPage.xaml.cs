using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace XamDocumentViewer.UWP
{
	public sealed partial class MainPage
	{
		public MainPage()
		{
			this.InitializeComponent();
		}

		protected override async void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);
			var args = e.Parameter as Windows.ApplicationModel.Activation.IActivatedEventArgs;
			if (args != null)
			{
				if (args.Kind == Windows.ApplicationModel.Activation.ActivationKind.File)
				{
					var fileArgs = args as Windows.ApplicationModel.Activation.FileActivatedEventArgs;
					string strFilePath = fileArgs.Files[0].Path;
					var file = (StorageFile)fileArgs.Files[0];
					Stream stream = await file.OpenStreamForReadAsync();
					LoadApplication(new Standard.App(new Standard.Dtos.DocumentDetails
					{
						Stream = stream,
						Type = Path.GetExtension(file.Path)
					}));
				}
			}
			else
			{
				LoadApplication(new Standard.App());
			}
		}
	}
}
