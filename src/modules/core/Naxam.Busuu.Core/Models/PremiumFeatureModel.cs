using System;
using System.Collections.Generic;
using MvvmCross.Core.ViewModels;

namespace Naxam.Busuu.Core.Models
{
    public class PremiumFeatureModel:MvxNotifyPropertyChanged
    {

        private string _feature;

        public string Feature
		{
			get { return _feature; }
			set
			{
				if (_feature != value)
				{
					_feature = value;
					RaisePropertyChanged();
				}
			}
		}

		private string _image;

		public string Image
		{
			get { return _image; }
			set
			{
				if (_image != value)
				{
					_image = value;
					RaisePropertyChanged();
				}
			}
		}

        public PremiumFeatureModel(string feature, string image)
        {
            Feature = feature;
            Image = image;
        }
    }
}
