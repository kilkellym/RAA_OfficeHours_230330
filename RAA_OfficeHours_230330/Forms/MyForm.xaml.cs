using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using static System.Collections.Specialized.BitVector32;


namespace RAA_OfficeHours_230330
{
    /// <summary>
    /// Interaction logic for Window.xaml
    /// </summary>
    public partial class MyForm : Window
    {
        public Document myDoc;
        public DocumentSet myDocSet;
        ObservableCollection<Document> documents { get; set; }
        ObservableCollection<Element> elements { get; set; }

        public MyForm(DocumentSet docs, string action)
        {
            InitializeComponent();

            myDocSet = docs;

            documents = new ObservableCollection<Document>();
            elements = new ObservableCollection<Element>();

            //Title 
            lblTitle.Content = action;

            //Add open documents 
            foreach (Document doc in docs)
            {
                documents.Add(doc);
            }

            cmbFiles.ItemsSource = documents;
            cmbFiles.DisplayMemberPath = "Title";

            lbxData.ItemsSource = elements;
            //lbxData.DisplayMemberPath = "Name"; 

          
            //Display first item
            cmbFiles.SelectedIndex = 0;
        }
        
        private void btnAll_Click(object sender, RoutedEventArgs e)
        {
            lbxData.SelectAll();
        }

        private void btnNone_Click(object sender, RoutedEventArgs e)
        {
            lbxData.UnselectAll();
        }
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            List<Element> selectedItems = new List<Element>();

            foreach(View curView in lbxData.SelectedItems)
            {
                selectedItems.Add(curView);
            }

            this.DialogResult = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public List<string> GetSelectedItemsWPF()
        {
            List<string> returnList = new List<string>();

            foreach (string item in lbxData.SelectedItems)
            {
                returnList.Add(item);
            }

            return returnList;
        }

        private void cmbFiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Document curDoc = cmbFiles.SelectedItem as Document;

            elements.Clear();
            foreach(Element el in GetAllViewTemplates(curDoc))
            {
                elements.Add(el);  
            }
        }

        public List<View> GetAllViewTemplates(Document doc)
        {
            List<View> returnList = new List<View>();

            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfClass(typeof(View));

            foreach (View curView in collector)
            {
                if (curView.IsTemplate == true)
                {
                    returnList.Add(curView);
                }
            }

            return returnList;
        }

        //public static List<string> GetUnUsedViewTemplates(Document curDoc)
        //{
        //    FilteredElementCollector usedVT = new FilteredElementCollector(curDoc).OfClass(typeof(View));

        //    // Views
        //    IList<Element> elements = usedVT.ToElements();

        //    // Ids
        //    List<ElementId> usedVTIds = new List<ElementId>();
        //    List<ElementId> unUsedVTIds = new List<ElementId>();

        //    // Names
        //    List<string> usedVTNames = new List<string>();
        //    List<string> unUsedVTNames = new List<string>();


        //    foreach (View v in elements)
        //    {
        //        if (v.ViewTemplateId != ElementId.InvalidElementId)
        //        {
        //            usedVTIds.Add(v.ViewTemplateId);
        //        }

        //        else
        //        {

        //            unUsedVTIds.Add(ElementId.InvalidElementId);
        //        }
        //    }


        //    //Get View item name
        //    foreach (ElementId evt in unUsedVTIds)
        //    {
        //        Element element = curDoc.GetElement(evt);
        //        string vtNam = element.Name;

        //        unUsedVTNames.Add(vtNam);
        //    }

        //    foreach (ElementId evt in usedVTIds)
        //    {
        //        Element element = curDoc.GetElement(evt);
        //        string vtNam = element.Name;

        //        usedVTNames.Add(vtNam);
        //    }

        }
    }
