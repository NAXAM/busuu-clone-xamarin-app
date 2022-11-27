using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using Naxam.Busuu.Core.Models;

namespace Naxam.Busuu.iOS.Start.Cells
{
    public partial class CountryCell : MvxTableViewCell
	{
		public CountryCell (IntPtr handle) : base (handle)
		{
			this.DelayBind(() =>
			{
                var setBinding = this.CreateBindingSet<CountryCell, CountryModel>();
                setBinding.Bind(lblCountry).To(n => n.Country);
                setBinding.Bind(lblPhoneCode).To(n => n.PhoneCode);
				setBinding.Apply();
			});
		}
	}
}
