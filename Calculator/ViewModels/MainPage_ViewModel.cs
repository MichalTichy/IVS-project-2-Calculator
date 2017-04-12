using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Math;
using Math.Nodes.Functions;
using Math.Nodes.Values;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml.Controls;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Core;

namespace Calculator
{
   public class MainPage_ViewModel : INotifyPropertyChanged
    {

        public MainPage_ViewModel()
        {
            tk = new Tokenizer();
            lst = tk.GetPossibleNextMathOperators(ExpressionPartTypes.Number);
            extree = new ExpressionTreeBuilder<Math.Tokenizer>((Tokenizer)tk);
            OutputColor.Color = Colors.Black;
            
        }
        private MathOperatorDescription selectedItem;
        private Math.ITokenizer tk;
        private ICollection<string> collectionWithExpressionPartTypes;
        private IExpressionTreeBuilder extree;
        private ICollection<MathOperatorDescription> lst;
        private Math.Nodes.INode node;
        private string val = "0";
        private string result = "";
        private bool isParsable =false;
        private ICollection<(string, MathOperatorDescription)> ts2;
        private int selection;
        private Windows.UI.Xaml.Media.SolidColorBrush outputColor = new Windows.UI.Xaml.Media.SolidColorBrush();
        



        public string Values
        {
            get => val; 
            set
            {
                val = value;
                evaluate(true);
                OnPropertyChanged("Values");
            }
        }

        public ICollection<MathOperatorDescription> Lst
        {
            get => lst;
            set
            {
                lst = value;
                OnPropertyChanged("Lst");
            }
        }

        public string Result
        {
            get { return result; }
            set { result = value;
                OnPropertyChanged("Result");
                        }
        }

        public MathOperatorDescription SelectedItem
        { get => selectedItem;
          set
          {
                Debug.WriteLine("tets");
                if (value != selectedItem)
                {
                    selectedItem = value;
                    Values += $" {SelectedItem.TextRepresentation}";
                    evaluate(true);
                    OnPropertyChanged("SelectedItem");
                    
                }
          }
        }

        public int Selection
        {
            get => selection;
            set
            {
                selection = value;
                OnPropertyChanged("Selection");
            }
        }

        public Windows.UI.Xaml.Media.SolidColorBrush OutputColor
        {
            get => outputColor;
            set
            {
                outputColor = value;
                OnPropertyChanged("OutputColor");
            }
        }

        public bool IsParsable
        {
            get => isParsable;
            set
            {
                isParsable = value;
                OnPropertyChanged("IsParsable");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

    

    public void something(object sender, RoutedEventArgs e)
        {
            evaluate(false);
            Debug.WriteLine(sender.ToString());
        }

        private void evaluate(bool edit)
        {
            string valueToEvalution = val;
            if (edit)
            {
                valueToEvalution = val.Substring(0, selection);
            }

            try
            {
                IsParsable = true;
                
                node = extree.ParseExpression(valueToEvalution);
                collectionWithExpressionPartTypes = tk.SplitExpressionToTokens(valueToEvalution);
               
                
                ts2 = tk.AssignOperatorDescriptionToTokens(collectionWithExpressionPartTypes);

            }
            catch
            {
                IsParsable = false;
                Debug.WriteLine("catch");
            }
            finally
            {
                //Debug.WriteLine($"last expr {collectionWithExpressionPartTypes.Last<string>()}");
                Lst = tk.GetPossibleNextMathOperators(Tokenizer.GetPrecedingExpressionPartType(ts2.Last<(string, MathOperatorDescription)>()));
                Debug.WriteLine($"test {ts2.Last<(string, MathOperatorDescription)>().Item1}");
                if (IsParsable)
                {
                    
                }
                else
                {
                    Lst = null;
                }
                if (!edit)
                {
                    if (!IsParsable)
                    {
                        OutputColor.Color = Colors.Red;
                    }
                    else
                    {
                        OutputColor.Color = Colors.Black;
                    }
                    
                }
            }
            


            
           


        }
        public void SelectionChanged()
        {
            Debug.WriteLine($"xxxx {selection} .. {Values.Length}");
            if (selection != Values.Length)
            {
                evaluate(true);
            }
            else evaluate(false);


        }

        public void KeyPressed(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                int totalyNonUsefull;
                if (isParsable && (int.TryParse(collectionWithExpressionPartTypes.Last<string>(), out totalyNonUsefull)))
                {
                    Result = node.Evaluate().ToString();
                }
            }
           

          
        }
    }
}
