using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using FolderOverride;
using FolderOverride.ProcessElements;

namespace FolderOverride.Pages
{
    /// <summary>
    /// Interaction logic for Page_Main.xaml
    /// </summary>
    public partial class Page_Main : Page
    {
        public Page_Main()
        {
            InitializeComponent();
        }

        private void btnProceed_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var fom = new FolderOverrideMgr(txtSrcFolder.Text, txtDestFolder.Text);
                fom.Proceed();
                
                
                
                
                
                
                
                //AddingFiles.Page_SettingPaths p = new AddingFiles.Page_SettingPaths(
                //    new AddingFiles.Process());

                //p.PrepareStep();

                //this.NavigationService.Navigate(p);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }

        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            var fom = new FolderOverrideMgr(txtSrcFolder.Text, txtDestFolder.Text);
            fom.Proceed();

        }

    
    
    
    
    }
}
