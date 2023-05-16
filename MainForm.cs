using DarkUI.Controls;

namespace TOTKActorRepacker
{
    public partial class MainForm : DarkUI.Forms.DarkForm
    {
        public MainForm()
        {
            InitializeComponent();
            CreateOptionsTable();
        }

        private void CreateOptionsTable()
        {
            TableLayoutPanel tlp = new TableLayoutPanel() { Name = "tlp_Inner", Dock = DockStyle.Top, Width = 100 };
            
            AddOptions(tlp);

            pnl_Main.Controls.Add(tlp);
        }

        private void AddOptions(TableLayoutPanel tlp)
        {
            TableLayoutPanel option = new TableLayoutPanel() { Name = "tlp_Option", Dock = DockStyle.Fill, ColumnCount = 3, RowCount = 1 };

            // Set width of columns
            foreach (var width in new float[] { 15f, 50f, 35f })
                option.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, width));
            // Add fields
            AnchorStyles anchorStyle = (AnchorStyles.Left | AnchorStyles.Right);
            option.Controls.Add(new DarkCheckBox { Name = "chk_Option", Checked = true, Anchor = anchorStyle }, 0, 0);
            option.Controls.Add(new DarkLabel { Name = "lbl_Option", Text = "OptionName", Anchor = anchorStyle }, 1, 0 );
            option.Controls.Add(new DarkTextBox { Name = "txt_Option", Anchor = anchorStyle }, 2, 0);

            tlp.Controls.Add(option);
        }
    }
}