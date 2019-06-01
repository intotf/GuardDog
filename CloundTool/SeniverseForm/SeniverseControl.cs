using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeatherLib;

namespace SeniverseForm
{
    public partial class SeniverseControl : UserControl
    {
        public Daily Daily { get; private set; }

        public void Refresh(Daily model)
        {
            var properties = model.GetType().GetProperties();
            foreach (var control in this.Controls)
            {
                if (control.GetType() == typeof(Label))
                {
                    var lb = (Label)control;
                    if (properties.Select(it => it.Name).Contains(lb.Name))
                    {
                        var value = properties.Where(it => it.Name == lb.Name).FirstOrDefault().GetValue(model);
                        if (lb.Name == "wind_direction")
                        {
                            value = value + " / " + model.wind_scale + "级";
                        }
                        lb.Text = value == null ? "" : value.ToString();
                    }
                }
                if (control.GetType() == typeof(PictureBox))
                {
                    var pic = (PictureBox)control;
                    pic.ImageLocation = "icon/3d_180/" + model.code_day + ".png";
                }
            }
        }

        public SeniverseControl(Daily model)
        {
            InitializeComponent();
            this.Daily = model;
            Refresh(model);
        }
    }
}
