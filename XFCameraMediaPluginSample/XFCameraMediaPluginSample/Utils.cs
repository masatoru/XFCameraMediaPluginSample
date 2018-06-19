using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GeolocatorSample
{
	public static class Utils
	{
        static string GetDescription(Permission permission)
        {
            var table = new Dictionary<Permission, string>(){
                {Permission.Camera,"カメラ"},
                {Permission.Location,"位置情報"}
            };
            if(table.ContainsKey(permission)){
                return table[permission];
            }

            throw new Exception($"対応する文字列がありません permission={permission}");
        }

		public static async Task<bool> CheckPermissions(Permission permission)
		{
			var permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);
			bool request = false;
			if (permissionStatus == PermissionStatus.Denied)
			{
				if (Device.RuntimePlatform == Device.iOS)
				{
                    var desc = GetDescription(permission);
					var title = $"{desc}のアクセス許可が必要です";
					var question = $"このプラグインを使用するためには{desc}のアクセス許可が必要です。設定→プライバシーでアクセス許可をONにしてください。";
					var positive = "設定を開く";
					var negative = "あとで";
					var task = Application.Current?.MainPage?.DisplayAlert(title, question, positive, negative);
					if (task == null)
						return false;

					var result = await task;
					if (result)
					{
						CrossPermissions.Current.OpenAppSettings();
					}

					return false;
				}

				request = true;

			}

            // iOSではないとき？？？
			//if (request || permissionStatus != PermissionStatus.Granted)
			//{
			//	var newStatus = await CrossPermissions.Current.RequestPermissionsAsync(permission);
			//	if (newStatus.ContainsKey(permission) && newStatus[permission] != PermissionStatus.Granted)
			//	{
			//		var title = $"{permission} Permission";
			//		var question = $"To use the plugin the {permission} permission is required.";
			//		var positive = "Settings";
			//		var negative = "Maybe Later";
			//		var task = Application.Current?.MainPage?.DisplayAlert(title, question, positive, negative);
			//		if (task == null)
			//			return false;

			//		var result = await task;
			//		if (result)
			//		{
			//			CrossPermissions.Current.OpenAppSettings();
			//		}
			//		return false;
			//	}
			//}

			return true;
		}
	}
}
