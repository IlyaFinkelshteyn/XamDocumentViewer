using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Database;
using Android.Gms.Ads;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Widget;
using System;
using System.IO;
using System.Threading.Tasks;
using XamDocumentViewer.Standard;

namespace XamDocumentViewer.Droid
{
	[Activity(Label = "XamDocumentViewer", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	[IntentFilter(new[] { Intent.ActionView }, DataScheme = "rtsp", DataHost = "*", Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable }, DataMimeTypes = new string[] { "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "application/msword" }, Icon = "@drawable/icon")]
	[IntentFilter(new[] { Intent.ActionView }, DataMimeTypes = new string[] { "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "application/msword" }, Categories = new[] { Intent.CategoryDefault }, Icon = "@drawable/icon")]
	[IntentFilter(new[] { Intent.ActionView }, DataScheme = "http", DataMimeTypes = new string[] { "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "application/msword" }, Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable }, Icon = "@drawable/icon")]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
			TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;
			AndroidEnvironment.UnhandledExceptionRaiser += AndroidEnvironmentOnUnhandledException;

			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);
			MobileAds.Initialize(ApplicationContext, "ca-app-pub-5565829267699786~3193049561");
			Tuple<Stream, string> fileDetails = GetFileStreamAndFileType();

			if (fileDetails.Item1 == null)
			{
				LoadApplication(new App());
			}
			else
			{
				LoadApplication(new App(new Standard.Dtos.DocumentDetails
				{
					Stream = fileDetails.Item1,
					Type = fileDetails.Item2
				}));
			}
		}

		private void AndroidEnvironmentOnUnhandledException(object sender, RaiseThrowableEventArgs e)
		{
			e.Handled = true;
			throw new NotImplementedException();
		}

		private void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
		{
			throw new NotImplementedException();
		}

		private void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			throw new NotImplementedException();
		}

		private Tuple<Stream, string> GetFileStreamAndFileType()
		{
			FileStream fileStream = null;
			string fileType = null;
			string filePath = null;
			if (!string.IsNullOrWhiteSpace(Intent.DataString))
			{
				filePath = getPath(this.ApplicationContext, Intent.Data);
				fileType = Path.GetExtension(filePath);
				Toast.MakeText(this, filePath, ToastLength.Long).Show();

				fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true);
			}

			return new Tuple<Stream, string>(fileStream, fileType);
		}

		public string getPath(Context context, Android.Net.Uri uri)
		{

			bool isKitKat = Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat;

			// DocumentProvider
			if (isKitKat && DocumentsContract.IsDocumentUri(context, uri))
			{
				// ExternalStorageProvider
				if (isExternalStorageDocument(uri))
				{
					String docId = DocumentsContract.GetDocumentId(uri);
					String[] split = docId.Split(":".ToCharArray());
					String type = split[0];

					if ("primary".Equals(type, StringComparison.CurrentCultureIgnoreCase))
					{
						return Android.OS.Environment.ExternalStorageDirectory + "/" + split[1];
					}

					// TODO handle non-primary volumes
				}
				// DownloadsProvider
				else if (isDownloadsDocument(uri))
				{

					String id = DocumentsContract.GetDocumentId(uri);
					Android.Net.Uri contentUri = ContentUris.WithAppendedId(
							Android.Net.Uri.Parse("content://downloads/public_downloads"), long.Parse(id));

					return getDataColumn(context, contentUri, null, null);
				}
				// MediaProvider
				else if (isMediaDocument(uri))
				{
					String docId = DocumentsContract.GetDocumentId(uri);
					String[] split = docId.Split(":".ToCharArray());
					String type = split[0];

					Android.Net.Uri contentUri = null;
					if ("image".Equals(type))
					{
						contentUri = MediaStore.Images.Media.ExternalContentUri;
					}
					else if ("video".Equals(type))
					{
						contentUri = MediaStore.Video.Media.ExternalContentUri;
					}
					else if ("audio".Equals(type))
					{
						contentUri = MediaStore.Audio.Media.ExternalContentUri;
					}

					String selection = "_id=?";
					String[] selectionArgs = new String[] {
						split[1]
				};

					return getDataColumn(context, contentUri, selection, selectionArgs);
				}
			}
			// MediaStore (and general)
			else if ("content".Equals(uri.Scheme, StringComparison.CurrentCultureIgnoreCase))
			{

				// Return the remote address
				if (isGooglePhotosUri(uri))
					return uri.LastPathSegment;

				return getDataColumn(context, uri, null, null);
			}
			// File
			else if ("file".Equals(uri.Scheme, StringComparison.CurrentCultureIgnoreCase))
			{
				return uri.Path;
			}

			return null;
		}

		public static String getDataColumn(Context context, Android.Net.Uri uri, String selection,
									   String[] selectionArgs)
		{

			ICursor cursor = null;
			String column = "_data";
			String[] projection = {
				column

		};

			try
			{
				cursor = context.ContentResolver.Query(uri, projection, selection, selectionArgs,
						null);
				if (cursor != null && cursor.MoveToFirst())
				{
					int index = cursor.GetColumnIndexOrThrow(column);
					return cursor.GetString(index);
				}
			}
			finally
			{
				if (cursor != null)
					cursor.Close();
			}
			return null;
		}


		/**
		 * @param uri The Uri to check.
		 * @return Whether the Uri authority is ExternalStorageProvider.
		 */
		public bool isExternalStorageDocument(Android.Net.Uri uri)
		{
			return "com.android.externalstorage.documents".Equals(uri.Authority);
		}

		/**
		 * @param uri The Uri to check.
		 * @return Whether the Uri authority is DownloadsProvider.
		 */
		public bool isDownloadsDocument(Android.Net.Uri uri)
		{
			return "com.android.providers.downloads.documents".Equals(uri.Authority);
		}

		/**
		 * @param uri The Uri to check.
		 * @return Whether the Uri authority is MediaProvider.
		 */
		public bool isMediaDocument(Android.Net.Uri uri)
		{
			return "com.android.providers.media.documents".Equals(uri.Authority);
		}

		/**
		 * @param uri The Uri to check.
		 * @return Whether the Uri authority is Google Photos.
		 */
		public bool isGooglePhotosUri(Android.Net.Uri uri)
		{
			return "com.google.android.apps.photos.content".Equals(uri.Authority);
		}
	}
}

