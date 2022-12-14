// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using Naxam.Busuu.Core.Models;
using UIKit;

namespace Naxam.Busuu.iOS.Profile.Cells
{
	public partial class InterfaceLanguageCell : MvxTableViewCell
	{
		public InterfaceLanguageCell (IntPtr handle) : base (handle)
		{
			this.DelayBind(() =>
			{
				var setBinding = this.CreateBindingSet<InterfaceLanguageCell, LanguageModel>();
                setBinding.Bind(lblLanguage).To(n => n.Language);
				setBinding.Apply();
			});
		}
	}
}
