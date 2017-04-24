using Windows.Foundation;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using Calculator.Graph;
using Calculator.ViewModels;
using Math.Tokenizer;
using System;

// Dokumentaci k šabloně položky Prázdná stránka najdete na adrese https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x405

namespace Calculator.View
{
    /// <summary>
    /// Prázdná stránka, která se dá použít samostatně nebo v rámci objektu Frame
    /// </summary>
    public sealed partial class MainPage
    {
      
       
        private readonly MainPageViewModel _viewModel;
        /// <summary>
        /// CodeBehind for View
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            ApplicationView.PreferredLaunchViewSize = new Size(400, 800);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            _viewModel = new MainPageViewModel(Tree);
            DataContext = _viewModel;
          



        }

        

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.LostBySelection = true;
            _viewModel.SelectedItem = (MathOperatorDescription)LstVw.SelectedItem;
            TXB_Value.Focus(FocusState.Keyboard);
            TXB_Value.Select(_viewModel.Selection, 0);
            if (LstVw.SelectedItem != null)
            {
                _viewModel.SelectedItem = (MathOperatorDescription)LstVw.SelectedItem;
            }
            
            //LstVw.DeselectRange(new ItemIndexRange(0, 100));
        }

        static Point PtFromDPoint(DPoint dpt)
        {
            return new Point(dpt.X, dpt.Y);
        }
        /// <summary>
        /// resetup points of all connections
        /// </summary>
        public void Reconnect()
        {
            if (Tree.Connections != null)
            {
                SolidColorBrush brsh = new SolidColorBrush(Colors.Black);
                brsh.Opacity = 1;
                Line ln = new Line();
                ln.Stroke = brsh;
                ln.StrokeThickness = 3.0;
                new Point(0, 0);
                bool fHaveLastPoint;

                foreach (TreeConnection tcn in Tree.Connections)
                {
                    fHaveLastPoint = false;
                    foreach (DPoint dpt in tcn.LstPt)
                    {
                        
                        if (!fHaveLastPoint)
                        {
                            PtFromDPoint(tcn.LstPt[0]);
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
