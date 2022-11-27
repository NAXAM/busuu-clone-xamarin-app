using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Platform.Converters;
using static Naxam.Busuu.Learning.Models.ExerciseModel;

namespace Naxam.Busuu.Droid.Learning.Converter
{
    public class ExerciseTypeToDrawableConverter : IMvxValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ExerciseType type = (ExerciseType)value;
            switch (type)
            {
                case ExerciseType.Conversation:
                    return Resource.Drawable.icon_bubbles;
                case ExerciseType.Discover:
                    return Resource.Drawable.icon_book_search;
                case ExerciseType.Evolution:
                    return Resource.Drawable.icon_book_side;
                case ExerciseType.Memorise:
                    return Resource.Drawable.icon_lightning;
                case ExerciseType.Practice:
                    return Resource.Drawable.icon_book_tick;
                case ExerciseType.Vocabulary:
                    return Resource.Drawable.icon_vocabulary;
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}