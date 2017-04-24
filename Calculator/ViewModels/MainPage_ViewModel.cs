﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Math;
using Math.ExpressionTreeBuilder;
using Math.Tokenizer;
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
using TreeContainer;
using GraphLayout;

namespace Calculator
{
   public class MainPage_ViewModel : INotifyPropertyChanged
    {

        private TreeContainer.TreeContainer tre;
        public MainPage_ViewModel(TreeContainer.TreeContainer tree)
        {
            
            tk = new Tokenizer();

            tre = tree;
            //lst = tk.GetPossibleNextMathOperators(ExpressionPartTypes.Number);
            extree = new ExpressionTreeBuilder<Tokenizer>((Tokenizer)tk);
            Parse(false);
            OutputColor.Color = Colors.Black;

        }
        private MathOperatorDescription selectedItem;
        private ITokenizer tk;
        private ICollection<string> collectionWithExpressionPartTypes;
        private IExpressionTreeBuilder extree;
        private ICollection<MathOperatorDescription> lst;
        private Math.Nodes.INode node;
        private string val = "";
        private string result = "";
        private bool isParsable =false;
        private ICollection<(string, MathOperatorDescription)> ts2;
        private int selection;
        private Windows.UI.Xaml.Media.SolidColorBrush outputColor = new Windows.UI.Xaml.Media.SolidColorBrush();
        public int oldsel;
        public bool LostBySelection = false;
        private MathOperatorDescription lastSelecetedItem;
        private List<TreeConnection> conn;

        public MathOperatorDescription LastSelecetedItem
        {
            get => lastSelecetedItem;
            set
            {
                lastSelecetedItem = value;
                OnPropertyChanged("LastSelectedItem");
            }
        } 

        #region Bindings
        public string Values
        {
            get => val; 
            set
            {
                val = value;
                Parse(true);
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
                //Debug.WriteLine("tets");
                    selectedItem = value;
                if (selectedItem != null)
                {
                    int i = selectedItem.TextRepresentation.Length;
                    string newValues = val.Insert(selection, selectedItem.TextRepresentation);
                    Selection +=i;
                    Values = newValues;
                    //Values += $" {SelectedItem.TextRepresentation}";
                }
                    
                    OnPropertyChanged("SelectedItem");
          }
        }

        public int Selection
        {
            get => selection;
            set
            {
                oldsel = selection;
                if ((oldsel == 0 && value == 0) || value != 0)
                {
                    selection = value;
                }
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

        public List<TreeConnection> Conn
        {
            get => conn;
            set
            {
                conn = value;
                OnPropertyChanged("Conn");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region TreeNodeHandling
        Math.Nodes.INode getLeftNode(Math.Nodes.INode n)
        {
            if (!(n is Math.Nodes.Values.NumberNode))
            {
                while (!(n is Math.Nodes.Values.NumberNode) && n != null)
                {
                    if (n is Math.Nodes.Functions.Binary.IBinaryOperationNode)
                    {
                        n = ((Math.Nodes.Functions.Binary.IBinaryOperationNode)n).LeftNode;
                    }
                    else
                    {
                        n = ((Math.Nodes.Functions.Unary.IUnaryOperationNode)n).ChildNode;
                    }
                    
                
                }
            }
            
            return n;
        }

        Math.Nodes.INode getFirst(Math.Nodes.INode n)
        {
            if (n == null) return null;
            else return getLeftNode(n);
        }

        Math.Nodes.INode getNext(Math.Nodes.INode n)
        {
           
                if (!(n is Math.Nodes.Values.NumberNode) && (!(n is Math.Nodes.Functions.Unary.IUnaryOperationNode)) && ((Math.Nodes.Functions.Binary.IBinaryOperationNode)n).RightNode != null)
                {
                    return getLeftNode(((Math.Nodes.Functions.Binary.IBinaryOperationNode)n).RightNode);
                }
                else 
                {
                    while ((n.Parent != null) && (n.Parent is Math.Nodes.Functions.Binary.IBinaryOperationNode) && n == ((Math.Nodes.Functions.Binary.IBinaryOperationNode) n.Parent).RightNode)
                    {
                        n = n.Parent;
                    }
                }
                    return n.Parent;
                
        }

        string GetNodeTextRepre(Math.Nodes.INode n)
        {
            IReadOnlyCollection<MathOperatorDescription> mod = tk.RegisteredOperators;
            foreach (MathOperatorDescription ss in mod)
            {
                if (ss.NodeType == n.GetType())
                {
                    return ss.TextRepresentation;
                }
                
            }
            if (n is Math.Nodes.Values.NumberNode)
            {
                return ((Math.Nodes.Values.NumberNode)n).Evaluate().ToString();
            }
            else
            {
                return "x";
            }
        }
       
        void foo(Math.Nodes.INode n)
        {
            tre.Clear();
            tre.Root = n.GetHashCode().ToString();
            tre.AddNode(GetNodeTextRepre(n), n.GetHashCode().ToString(), (string)null);

            n = getFirst(n);
            
            int i = 0;
            while (n!=null)
            {
               
                if (n.Parent != null)
                {
                    tre.AddNode(GetNodeTextRepre(n), n.GetHashCode().ToString(), (n.Parent).GetHashCode().ToString());
                }
                n = getNext(n);
                
                i++;
            }
        }

#endregion
        private void Parse(bool edit)
        {
            if (val != "")
            {


                string valueToParse = val;
                if (edit)
                {
                    valueToParse = val.Substring(0, selection);
                }

                try
                {
                    IsParsable = true;

                    node = extree.ParseExpression(valueToParse);
                    foo(node);
                    collectionWithExpressionPartTypes = tk.SplitExpressionToTokens(valueToParse);
                    ts2 = tk.AssignOperatorDescriptionToTokens(collectionWithExpressionPartTypes);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    IsParsable = false;
                }
                finally
                {
                    if (!IsParsable)
                    {
                        Lst = null;
                    }
                    else
                    {
                        Lst = tk.GetPossibleNextMathOperators(Tokenizer.GetPrecedingExpressionPartType(ts2.Last<(string, MathOperatorDescription)>()));
                        LostBySelection = false;

                    }
                    if (!edit)
                    {
                        OutputColor.Color = (!isParsable) ? Colors.Red : Colors.Black;
                    }
                }


            }
            else
            {
                Result = "";
                tre.Clear();
            }
        }

       
       
        public void SelectionChanged()
        {
            Debug.WriteLine($"xxxx {selection} .. {Values.Length}");
            if (selection != Values.Length)
            {
                Parse(true);
            }
            else Parse(false);


        }


        public void InputLostFocus(object sender, RoutedEventArgs e)
        {
            Selection = oldsel;
        }

        public void KeyPressed(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                Eval();
            }
           

          
        }
        public void Eval()
        {
            if (isParsable)
            {
                try
                {
                    Result = node.Evaluate().ToString();
                }
                catch (Exception ex)
                {


                    DisplayErrDialog(ex.Message);
                }
            }
        }

        private async void DisplayErrDialog(string cont)
        {
            
            ContentDialog errDialog = new ContentDialog
            {
                Title = "Chyba",
                Content = cont, PrimaryButtonText="ok"
            };

            ContentDialogResult result = await errDialog.ShowAsync();
            
        }

    }

  


}
