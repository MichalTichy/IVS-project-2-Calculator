using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Windows.Input;
using Windows.UI.Xaml.Shapes;
using TreeContainer;
using GraphLayout;
using Windows.UI;
using Math.Tokenizer;

// Dokumentaci k šabloně položky Prázdná stránka najdete na adrese https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x405

namespace Calculator
{
    /// <summary>
    /// Prázdná stránka, která se dá použít samostatně nebo v rámci objektu Frame
    /// </summary>
    public sealed partial class MainPage : Page
    {
      
       
        private readonly MainPage_ViewModel viewModel;
        public MainPage()
        {
            this.InitializeComponent();
            ApplicationView.PreferredLaunchViewSize = new Size(400, 800);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            viewModel = new Calculator.MainPage_ViewModel(Tree);
            this.DataContext = viewModel;
          



        }

      



        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.LostBySelection = true;
            viewModel.SelectedItem = (MathOperatorDescription)LstVw.SelectedItem;
            TXB_Value.Focus(FocusState.Keyboard);
            TXB_Value.Select(viewModel.Selection, 0);
            if (LstVw.SelectedItem != null)
            {
                viewModel.SelectedItem = (MathOperatorDescription)LstVw.SelectedItem;
            }
            
            //LstVw.DeselectRange(new ItemIndexRange(0, 100));
        }

        static Point PtFromDPoint(DPoint dpt)
        {
            return new Point(dpt.X, dpt.Y);
        }
        public void cosi()
        {
            if (Tree.Connections != null)
            {
                SolidColorBrush brsh = new SolidColorBrush(Colors.Black);
                brsh.Opacity = 0.5;
                Line ln = new Line();
                ln.Stroke = brsh;
                ln.StrokeThickness = 1.0;
                Point ptLast = new Point(0, 0);
                bool fHaveLastPoint = false;

                foreach (TreeConnection tcn in Tree.Connections)
                {
                    fHaveLastPoint = false;
                    foreach (DPoint dpt in tcn.LstPt)
                    {
                        
                        if (!fHaveLastPoint)
                        {
                            ptLast = PtFromDPoint(tcn.LstPt[0]);
                            fHaveLastPoint = true;
                            continue;
                        }
                        ln.X1 = PtFromDPoint(tcn.LstPt[0]).X;
                        ln.Y1 = PtFromDPoint(tcn.LstPt[0]).Y;
                        ln.X2 = PtFromDPoint(dpt).X;
                        ln.Y2 = PtFromDPoint(dpt).Y;
                        //Tree.Children.Add(ln);
                        

                    }

                }
            }


        }
    }
}
