using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherMap.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherMap.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaClima : ContentPage
    {
        public PaginaClima()
        {
            InitializeComponent();
            this.BindingContext = new Clima();
        }
        private async void btnBuscar_Clicked(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtCiudad.Text))
            {
                var clima = await ServicioClima.ConsultarClima(txtCiudad.Text);

                if (clima != null)
                {
                    this.BindingContext = clima;
                    btnBuscar.Text = "Buscar de nuevo";
                }
            }
        }
    }
}