namespace ServiceApiExample
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            main_grid = new DataGridView();
            error_label = new Label();
            partName_label = new Label();
            noData_panel = new Panel();
            label1 = new Label();
            name_label = new Label();
            reset_button = new Button();
            ((System.ComponentModel.ISupportInitialize)main_grid).BeginInit();
            noData_panel.SuspendLayout();
            SuspendLayout();
            // 
            // main_grid
            // 
            main_grid.AllowUserToAddRows = false;
            main_grid.AllowUserToDeleteRows = false;
            main_grid.AllowUserToOrderColumns = true;
            main_grid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            main_grid.BackgroundColor = SystemColors.Window;
            main_grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            main_grid.Location = new Point(33, 43);
            main_grid.Name = "main_grid";
            main_grid.Size = new Size(1018, 550);
            main_grid.TabIndex = 0;
            // 
            // error_label
            // 
            error_label.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            error_label.AutoSize = true;
            error_label.ForeColor = Color.Red;
            error_label.Location = new Point(33, 627);
            error_label.Name = "error_label";
            error_label.Size = new Size(62, 15);
            error_label.TabIndex = 1;
            error_label.Text = "error_label";
            // 
            // partName_label
            // 
            partName_label.AutoSize = true;
            partName_label.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            partName_label.Location = new Point(33, 19);
            partName_label.Name = "partName_label";
            partName_label.Size = new Size(79, 21);
            partName_label.TabIndex = 2;
            partName_label.Text = "PartName";
            // 
            // noData_panel
            // 
            noData_panel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            noData_panel.BackColor = SystemColors.Window;
            noData_panel.BorderStyle = BorderStyle.FixedSingle;
            noData_panel.Controls.Add(label1);
            noData_panel.Location = new Point(301, 351);
            noData_panel.Name = "noData_panel";
            noData_panel.Size = new Size(480, 232);
            noData_panel.TabIndex = 3;
            noData_panel.Visible = false;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16F);
            label1.Location = new Point(193, 100);
            label1.Name = "label1";
            label1.Size = new Size(92, 30);
            label1.TabIndex = 0;
            label1.Text = "No Data";
            // 
            // name_label
            // 
            name_label.AutoSize = true;
            name_label.Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
            name_label.Location = new Point(118, 19);
            name_label.Name = "name_label";
            name_label.Size = new Size(98, 21);
            name_label.TabIndex = 4;
            name_label.Text = "name_label";
            // 
            // reset_button
            // 
            reset_button.Anchor = AnchorStyles.Bottom;
            reset_button.Location = new Point(507, 599);
            reset_button.Name = "reset_button";
            reset_button.Size = new Size(81, 23);
            reset_button.TabIndex = 5;
            reset_button.Text = "Reset Data";
            reset_button.UseVisualStyleBackColor = true;
            reset_button.Click += reset_button_Click;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1084, 665);
            Controls.Add(reset_button);
            Controls.Add(name_label);
            Controls.Add(noData_panel);
            Controls.Add(partName_label);
            Controls.Add(error_label);
            Controls.Add(main_grid);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(400, 400);
            Name = "Main";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Demo API";
            FormClosed += Main_FormClosed;
            Load += Main_Load;
            ((System.ComponentModel.ISupportInitialize)main_grid).EndInit();
            noData_panel.ResumeLayout(false);
            noData_panel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView main_grid;
        private Label error_label;
        private Label partName_label;
        private Panel noData_panel;
        private Label label1;
        private Label name_label;
        private Button reset_button;
    }
}
