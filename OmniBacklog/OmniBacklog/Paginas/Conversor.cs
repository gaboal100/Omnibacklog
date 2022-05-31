using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Data;
using OmniBacklog.MODEL;

namespace OmniBacklog.Paginas
{
    public class Conversor : IMultiValueConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            //Colección que toma los items superiores
            List<object> items = new List<object>();

            //Recorre los valores y los añade a la lista de hijos
            for (int i = 0; i < values.Length; i++)
            {
                IEnumerable childs = values[i] as IEnumerable ?? new List<object> { values[i] };
                
                //Recorre los hijos de childs y los añade a la lista de items
                foreach (var child in childs) { items.Add(child); }
            }

            return items;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Cannot perform reverse-conversion");
        }
    }
}
