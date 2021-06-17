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
using System.Xml.Linq;
using System.Data;
using System.Collections.ObjectModel;

namespace SimplexMethod
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {       
        private DataTable MyTempDataTable = new DataTable();
        private DataTable MyFunctionDataTable = new DataTable();
        public DataTable _myTempDataTable
        {
            get { return MyTempDataTable; }
            set { MyTempDataTable = value; }
        }
        public DataTable _myFunctionDataTable
        {
            get { return MyFunctionDataTable; }
            set { MyFunctionDataTable = value; }
        } 
        public MainWindow()
        {
            InitializeComponent();            
            
            CommandBinding binding = new CommandBinding(ApplicationCommands.New);
            binding.Executed +=new ExecutedRoutedEventHandler(Next_Executed);
            this.CommandBindings.Add(binding);
        }
        void Next_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            int _columns = 0;
            int _rows = 0;
            try
            {
                _columns = Convert.ToInt32(TextBx1.Text);
                _rows = Convert.ToInt32(TextBx2.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Вводите только цифры. Ошибка конвертации {0}", ex.ToString());
            }
            TextBx1.Clear();
            TextBx2.Clear();
            DataTableClear();
            DataInsertColumns(_columns);            
            ItemValues(_rows);
            
            FunctionDataGrid.ItemsSource = _myFunctionDataTable.DefaultView;
            MyDataTable.ItemsSource = _myTempDataTable.DefaultView;

        }
        void DataTableClear()
        {
            if (_myTempDataTable != null)
            {
                _myTempDataTable.Columns.Clear();
                _myTempDataTable.Rows.Clear();              
            }         
        }
        void DataInsertColumns(int columns)
        {
            for (int i = 0; i < columns; i++)
            {
                _myTempDataTable.Columns.Add("x"+(i+1).ToString());
            }
            InsertColumnInToFunctionDataGrid();
            _myTempDataTable.Columns.Add("Базис");          
        }
        void InsertColumnInToFunctionDataGrid()
        {
            _myFunctionDataTable = _myTempDataTable.Clone();
            DataRow _temDR = _myFunctionDataTable.NewRow();
            for (int i = 0; i < _myFunctionDataTable.Columns.Count; i++)
                _temDR[i] = 0;
            _myFunctionDataTable.Rows.Add(_temDR);
        }
        void ItemValues(int _rows)
        {
            
            for (int i = 0; i < _rows; i++)           
            {
                DataRow _DataRow = _myTempDataTable.NewRow();
                for (int a = 0; a < _myTempDataTable.Columns.Count; a++)
                    _DataRow[a] = 0;             
                _myTempDataTable.Rows.Add(_DataRow);
            }            
        }
        private void EditSelectedCellMethod(object sender, DataGridCellEditEndingEventArgs e)
       {
            if (sender.Equals(MyDataTable))
            {
                DataTable dtG = ((DataView)MyDataTable.ItemsSource).ToTable();
                _myTempDataTable = dtG;
            }
            else
            {
                DataTable dtF = ((DataView)FunctionDataGrid.ItemsSource).ToTable();
               _myFunctionDataTable= dtF;
            }

        }      
    }
}
