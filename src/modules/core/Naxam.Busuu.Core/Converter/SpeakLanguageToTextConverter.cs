using MvvmCross.Platform.Converters;
using Naxam.Busuu.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Naxam.Busuu.Core.Converter
{
    public class SpeakLanguageToTextConverter : MvxValueConverter<IList<LanguageModel>, string>
    {
        protected override string Convert(IList<LanguageModel> value, Type targetType, object parameter, CultureInfo culture)
        {
            string text = "";
            int count = value.Count;
            switch (count)
            {
                case 0:
                    return "";
                case 1:
                    return value[0].Language;
            }
            for (int i = 0; i < value.Count; i++)
            {
                if (i == value.Count - 2)
                {
                    text += value[i].Language + " and ";
                    continue;
                }
                if (i == value.Count - 1)
                {
                    text += value[i].Language;
                    continue;
                }
                text += value[i] + ", ";
            }
            return text;
        }
    }
}
