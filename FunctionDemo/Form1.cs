using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraEditors;
using DevExpress.Utils;

namespace FunctionDemo
{
    public partial class Form1 : XtraForm
    {
        public Form1()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitPivotGrid();
            BindChart();
        }
        void BindChart()
        {
            chartControl.DataSource = pivotGridControl;
        }
        void InitPivotGrid()
        {
            // Change this property to transpose the chart.
            pivotGridControl.OptionsChartDataSource.ProvideDataByColumns = false;

            pivotGridControl.BeginUpdate();
            pivotGridControl.DataSource = GetData("SalesPerson");
            SetFilter();
            pivotGridControl.EndUpdate();
            SetSelection();
        }
        void SetFilter()
        {
            fieldProductName.FilterValues.SetValues(new object[] {
                "Chai",
                "Chang",
                "Chocolade",
                "Filo Mix",
                "Geitost",
                "Ikura",
                "Konbu",
                "Maxilaku",
                "Pavlova",
                "Spegesild",
                "Tourtiere"
            }, PivotFilterType.Included, true);
            fieldOrderYear.FilterValues.SetValues(new object[] { 1995 }, PivotFilterType.Included, true);
        }
        void SetSelection()
        {
            pivotGridControl.Cells.SetSelectionByFieldValues(false, new object[] { "Chocolade" });
            pivotGridControl.Cells.SetSelectionByFieldValues(false, new object[] { "Chai" });
        }
        IList GetData(string tableName)
        {
            var xmlFileName = FilesHelper.FindingFileName(Application.StartupPath, "Data\\nwindSalesPerson.xml");
            if (string.IsNullOrEmpty(xmlFileName))
                return null;
            using (DataSet dataSet = new DataSet())
            {
                dataSet.ReadXml(xmlFileName);
                return dataSet.Tables[tableName].DefaultView;
            }
        }
    }
}
