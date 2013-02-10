﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Olive
{
    public partial class MainWindow : Window
    {
        private List<TabItem> _tabs;
        private TabItem _tab;//Pestaña de añadir mas pestañas
        private int ID_Pest = 0; //Necesario id para cerrar pestañas debe de ser un valor unico sin posivilidad de repetir.

        public MainWindow()
        {
            try{
                InitializeComponent();
                //Iniciar array _tabs
                _tabs = new List<TabItem>();

                //Creamos la tab de agregar mas tabs
                _tab = new TabItem();
                _tab.Header = "+";

                //Añadimos la tab al array
                _tabs.Add(_tab);
                //Agregamos la primera pestaña normal
                this.AddTabItem();
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error iniciar la aplicaccion \n Error:"+ex);
            }
        }
        //Añadir Pestaña
        private void AddTabItem()
        {
            Pestañas.DataContext = null; //Lista a null
            ID_Pest++;//Sacamos new id

            TabItem tab = new TabItem();

            tab.Header = string.Format("Tab {0}", _tabs.Count);//Header path textblock definido en XAml
            tab.Name = string.Format("tab{0}", ID_Pest);//Dandole id a la pestaña
            tab.HeaderTemplate = Pestañas.FindResource("Plant_tabs") as DataTemplate;//agregar template creado en xaml


            //Texto de las pestañas
        /*    TextBox txt = new TextBox();
            txt.Name = "txt";

            tab.Content = lña;*/

            // Inserta antes de la ultima pestaña 
            _tabs.Insert(_tabs.Count - 1, tab);
            
            // bind tab control
            Pestañas.DataContext = _tabs;
            // select newly added tab item
            Pestañas.SelectedItem = tab;
 
        }

        //Button from tabsitems Button_Close_Click
        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            string tabName = (sender as Button).CommandParameter.ToString(); //Saca el nombre de la pestaña en la que esta el boton
            MessageBox.Show(tabName);
            var item = Pestañas.Items.Cast<TabItem>().Where(i => i.Name.Equals(tabName)).SingleOrDefault();

            TabItem tab = item as TabItem;

           if (tab != null)//Posible error
            {
                if (_tabs.Count < 3)//queda solo una pestaña < 3 
                {
                    Close();
                }
                else {
                    // tab seleccionada
                    TabItem selectedTab = Pestañas.SelectedItem as TabItem;
                    // clear tab control binding
                    Pestañas.DataContext = null;
                    _tabs.Remove(tab);//Borramos del listado
                    Pestañas.DataContext = _tabs; // restablece pestañas

                    if (selectedTab == null || selectedTab.Equals(tab))//Si la pestaña seleccionada es borrada, enfocamos otra.
                    {
                        selectedTab = _tabs[0];
                    }
                    Pestañas.SelectedItem = selectedTab;
                }
            }
        }

        private void Tab_change(object sender, SelectionChangedEventArgs e)//Cuando cambia la seleccion de pestaña
        {
            TabItem tab = Pestañas.SelectedItem as TabItem;
            if (tab == null) return;

            if (tab == _tab) //Si la pestaña seleccionada es la de añadir
            {
                AddTabItem();
            }
        }

        private void Pulsateclas(object sender, KeyEventArgs e)
        {

            // Ctrl + t Crea una pestaña
            if ((Keyboard.Modifiers == ModifierKeys.Control) && (e.Key == Key.T))
            {
                AddTabItem();
            }
            //Ctrl + P Imprime 
            if ((Keyboard.Modifiers == ModifierKeys.Control) && (e.Key == Key.P))
            {
                PrintDialog dialog = new PrintDialog();

                MessageBoxResult respuesta = MessageBox.Show("Desea imprimir ? ", "Impresión", MessageBoxButton.OKCancel);
                if (respuesta == MessageBoxResult.OK)
                {
                    // Imprimir la pantalla
                    if (dialog.ShowDialog() == true)
                    {
                        dialog.PrintVisual(this, "Impresión");
                    }
                }
                }

                //Ctrl + N Nueva ventana
                if ((Keyboard.Modifiers == ModifierKeys.Control) && (e.Key == Key.N))
                {
                    MainWindow window = new MainWindow();
                    window.Show();

                }
            // Ctrl + F
            /*  if ((Keyboard.Modifiers == ModifierKeys.Control) && (e.Key == Key.N))
              {
                  //Encontrar alguna manera de buscar en el contenido del webbrowser.
              }*/
        }


    }
}
