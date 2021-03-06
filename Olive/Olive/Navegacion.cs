﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Olive
{
    class Navegacion
    {
        private Uri url;
        private String Name;
        private WebBrowser navegador;
        public Uri Home;

        public Navegacion()
        {
            this.navegador = new WebBrowser();
        }
        public Navegacion(string Home)
        {
            this.Home = new Uri(Home, UriKind.RelativeOrAbsolute);
            this.navegador = new WebBrowser();
            navegador.LoadCompleted += navegador_LoadCompleted;
            navegador.Navigating += navegador_Navigating;
            navegador.Navigate(Home);
        }

        void navegador_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {

                //navegador.Source.Host
                //MessageBox.Show(e.NavigationMode);
            }
            catch { }
        }
        void navegador_LoadCompleted(object sender, NavigationEventArgs e)
        {
            this.Name = navegador.Source.Host;
            this.url = e.Uri;
        }
        //getters and setters
        public string getName()
        {
            return Name;
        }
        public void setName(string ruta)
        {
            //this.url = new Uri(ruta, UriKind.RelativeOrAbsolute);
            this.Name = ruta;
        }

        public Uri geturl()
        {
            return url;
        }
        public void seturl(Uri ruta)
        {
            //this.url = new Uri(ruta, UriKind.RelativeOrAbsolute);
            this.url = ruta;
        }


        public WebBrowser getbrouser()
        {
            return navegador;
        }

        //Propiedades
        #region propiedades
        public void goAdelante()
        {
            try
            {
                if (navegador.CanGoForward)
                {
                    navegador.GoForward();
                }
            }
            catch
            {
                //MessageBox.Show("No se pudo \n Error: " + e );
            }
        }
        public void goAtras()
        {
            try
            {
                if (navegador.CanGoBack)
                {
                    navegador.GoBack();
                }
            }
            catch 
            {
               // MessageBox.Show("No se pudo \n Error: " + e);
            }
        }
        public void goHome()
        {
            try
            {
                navegador.Navigate(Home);
            }
            catch
            {
                //MessageBox.Show("Error inesperado \n Error: " + e);
            }
        }
        public void goUrl()
        {
            navegador.Navigate(url);
        }
        public void Refresh()
        {
            navegador.Refresh();
        }
        public void FindName(string find)
        {
            navegador.FindName(find);
        }
        public void search(string find)
        {
            this.url = new Uri("http://google.com/search?q=" + find, UriKind.RelativeOrAbsolute);
            navegador.Navigate(url);
        }
        #endregion

    }
}
