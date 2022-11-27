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
using Java.Lang;
using MvvmCross.Droid.Support.V7.AppCompat;
using Naxam.Busuu.Droid.Profile.Utils;
using Android.Support.V4.Content;
using Android.Support.V4.Content.Res;
using Android.Graphics;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.Droid; 
using Naxam.Busuu.Profile.ViewModels;
using MvvmCross.Core.ViewModels;
using System.ComponentModel;
using System.Linq.Expressions;
using Naxam.Busuu.Droid.Core.Utils;
using Naxam.Busuu.Droid.Core.Controls;
using Naxam.Busuu.Droid.Core.Adapter;
using Naxam.Busuu.Core.Models;

namespace Naxam.Busuu.Droid.Profile.Views
{
    [Activity]
    public class ChooseCountryView : MvxAppCompatActivity, IMvxNotifyPropertyChanged
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

        }

        private CountryModel _countrySelected;

        public CountryModel CountrySelected
        {
            get { return _countrySelected; }
            set
            {
                if (_countrySelected != value)
                {
                    _countrySelected = value;
                    (ViewModel as ChooseCountryViewModel).CountrySelected = _countrySelected;
                    RaisePropertyChanged("CountrySelected");
                }
            }
        }


        private MvxObservableCollection<CountryModel> _countries;

        //public MvxObservableCollection<CountryModel> countries
        //{
            //get { return _countries; }
            //set
            //{
            //    if (_countries != value)
            //    {
            //        _countries = value;
            //        if (countries != null)
            //            list.SetAdapter(new Adapter(this, countries, (c) =>
            //            {
            //                CountrySelected = c;
            //            }));
            //        RaisePropertyChanged(() => countries);
            //    }
            //}
        //}
       // HeaderListView list; 

        public event PropertyChangedEventHandler PropertyChanged;

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();




          //  list = new HeaderListView(this);

         //   SetContentView(list);  

            //countries = countries ?? (ViewModel as ChooseCountryViewModel).Countries;

            //if (countries != null)
            //    list.SetAdapter(new Adapter(this, countries, (c) =>
            //    {
            //        CountrySelected = c;
            //    }));

        }

        public bool ShouldAlwaysRaiseInpcOnUserInterfaceThread()
        {
            throw new NotImplementedException();
        }

        public void ShouldAlwaysRaiseInpcOnUserInterfaceThread(bool value)
        {
            throw new NotImplementedException();
        }

        public void RaisePropertyChanged<T>(Expression<Func<T>> property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property.Name));
        }

        public void RaisePropertyChanged(string whichProperty)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(whichProperty));
        }

        public void RaisePropertyChanged(PropertyChangedEventArgs changedArgs)
        {
            PropertyChanged?.Invoke(this, changedArgs);
        }

        //public class Adapter : SectionAdapter
        //{
        //    Context context;
        //    List<CountryModel> countries;
        //    List<IGrouping<char, CountryModel>> ListSection;
        //    Action<CountryModel> SelectCountry;
        //    public Adapter(Context context, IList<CountryModel> countries, Action<CountryModel> SelectCountry)
        //    {
        //        this.context = context;
        //        this.countries = new List<CountryModel>(countries);
        //        ListSection = new List<IGrouping<char, CountryModel>>();
        //        this.SelectCountry = SelectCountry;
        //        ListSection = countries.OrderBy(d => d.Country).GroupBy((d) => d.Country[0]).ToList();
        //    }

        //    public override int NumberOfSections()
        //    {
        //        return ListSection.Count;
        //    }

        //    public override int NumberOfRows(int section)
        //    {
        //        return ListSection.ElementAt(section < 0 ? 0 : section).Count<CountryModel>();
        //    }

        //    public CountryModel RowItem(int section, int row)
        //    {
        //        return ListSection.ElementAt(section).ElementAt(row);
        //    }

        //    public override Java.Lang.Object GetRowItem(int section, int row)
        //    {
        //        //ListSection.ElementAt(section).ElementAt(row);
        //        return null;
        //    }


        //    public override bool HasSectionHeaderView(int section)
        //    {
        //        return true;
        //    }


        //    public override View GetRowView(int section, int row, View convertView, ViewGroup parent)
        //    {
        //        if (convertView == null)
        //        {

        //            convertView = LayoutInflater.FromContext(context).Inflate(Resource.Layout.choose_country_row_item, null);
        //        }
        //        TextView txtCountry = convertView.FindViewById<TextView>(Resource.Id.txtCountry);
        //        TextView txtPhoneCode = convertView.FindViewById<TextView>(Resource.Id.txtPhoneCode);
        //        CountryModel country = RowItem(section, row);
        //        txtCountry.Text = country.Country;
        //        txtPhoneCode.Text = country.PhoneCode;
        //        return convertView;
        //    }


        //    public override int GetSectionHeaderViewTypeCount()
        //    {
        //        return 2;
        //    }


        //    public override int GetSectionHeaderItemViewType(int section)
        //    {
        //        return section % 2;
        //    }

        //    public override void OnRowItemClick(AdapterView parent, View view, int section, int row, long id)
        //    {
        //        base.OnRowItemClick(parent, view, section, row, id); 
        //        SelectCountry?.Invoke(RowItem(section, row));
        //    }

        //    public override View GetSectionHeaderView(int section, View convertView, ViewGroup parent)
        //    {

        //        if (convertView == null)
        //        {
        //            convertView = LayoutInflater.FromContext(context).Inflate(Resource.Layout.choose_country_header_item, null);
        //        }
        //        TextView txtHeader = convertView.FindViewById<TextView>(Resource.Id.txtHeader);

        //        txtHeader.Text = ListSection.ElementAt(section).Key + "";

        //        return convertView;
        //    }


        //}

    }
}