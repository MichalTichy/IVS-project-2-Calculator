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


namespace Calculator
{
   public class MainPage_ViewModel : INotifyPropertyChanged
    {

        public MainPage_ViewModel()
        {
            tk = new Tokenizer();
            lst = tk.GetPossibleNextMathOperators(ExpressionPartTypes.Number);
            extree = new ExpressionTreeBuilder<Math.Tokenizer>((Tokenizer)tk);
            
        }
        private MathOperatorDescription selectedItem;
        private Math.ITokenizer tk;
        private ICollection<string> collectionWithExpressionPartTypes;
        private IExpressionTreeBuilder extree;
        private ICollection<MathOperatorDescription> lst;
        private Math.Nodes.INode node;
        private string val = "";
        private string result;
        private bool isParsable;
        private ICollection<(string, MathOperatorDescription)> ts2;
        private int selection;



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
                    Values += SelectedItem.TextRepresentation;
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

    

    public void something()
        {
            evaluate(false);
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
                isParsable = true;
                
                node = extree.ParseExpression(valueToEvalution);
                collectionWithExpressionPartTypes = tk.SplitExpressionToTokens(valueToEvalution);
               
                
                ts2 = tk.AssignOperatorDescriptionToTokens(collectionWithExpressionPartTypes);

            }
            catch
            {
                isParsable = false;
                Debug.WriteLine("catch");
            }
            finally
            {
                Debug.WriteLine($"last expr {collectionWithExpressionPartTypes.Last<string>()}");
                Lst = tk.GetPossibleNextMathOperators(Tokenizer.GetPrecedingExpressionPartType(ts2.Last<(string, MathOperatorDescription)>()));
                Debug.WriteLine($"test {ts2.Last<(string, MathOperatorDescription)>().Item1}");
                if (isParsable)
                {
                    int totalyNonUsefull;
                    if (int.TryParse(collectionWithExpressionPartTypes.Last<string>(), out totalyNonUsefull))
                    {
                        
                        Result = node.Evaluate().ToString();
                    }
                }
                else
                {
                    Lst = null;
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

        
    }
}
