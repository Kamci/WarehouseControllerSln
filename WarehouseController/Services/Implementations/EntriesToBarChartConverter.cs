using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entry = Microcharts.ChartEntry;

namespace WarehouseController.Services.Implementations
{
    public class EntriesToBarChartConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var entries = value as List<ChartEntry>;
            if (entries == null || entries.Count == 0)
            {
                return new BarChart
                {
                    Entries = new List<ChartEntry>
                        {
                            new ChartEntry(300)
                            {
                                 Label = "", 
                                 ValueLabel = "SELECT WAREHOUSE",
                                 Color = SKColor.Parse("#006A1B9A"),
                                 ValueLabelColor = SKColors.LightSkyBlue,
                            },
                        },
                    LabelOrientation = Orientation.Horizontal,
                    ValueLabelOrientation = Orientation.Horizontal,
                    ValueLabelTextSize = 30,
                    BackgroundColor = SKColors.Transparent,
                    
                    
                };
            }

            return new BarChart { Entries = entries };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}
