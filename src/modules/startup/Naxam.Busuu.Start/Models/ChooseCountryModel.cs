using System;
using MvvmCross.Plugins.Messenger;
using Naxam.Busuu.Core.Models;

namespace Naxam.Busuu.Start.Models
{
    public class ChooseCountryModel : MvxMessage
    {
		public ChooseCountryModel(object sender) : base(sender)
        {
		}

		public CountryModel Value { get; set; }
    }
}
