namespace gestion1
{
    partial class gestiondecategoriedesproduits
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(gestiondecategoriedesproduits));
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties5 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties6 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties7 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties8 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.CategorieGV = new Guna.UI2.WinForms.Guna2DataGridView();
            this.deletectgrbtn = new System.Windows.Forms.Button();
            this.Editcatgrbtn = new System.Windows.Forms.Button();
            this.addctgrbtn = new System.Windows.Forms.Button();
            this.nomctgrtb = new Bunifu.UI.WinForms.BunifuTextBox();
            this.retourbtn = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CategorieGV)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1076, 100);
            this.panel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(269, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(530, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "Gestion des categories des produits";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.retourbtn);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.CategorieGV);
            this.panel2.Controls.Add(this.deletectgrbtn);
            this.panel2.Controls.Add(this.Editcatgrbtn);
            this.panel2.Controls.Add(this.addctgrbtn);
            this.panel2.Controls.Add(this.nomctgrtb);
            this.panel2.Location = new System.Drawing.Point(0, 97);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1076, 554);
            this.panel2.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Highlight;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 523);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1076, 31);
            this.panel3.TabIndex = 17;
            // 
            // CategorieGV
            // 
            this.CategorieGV.AllowUserToDeleteRows = false;
            this.CategorieGV.AllowUserToResizeColumns = false;
            this.CategorieGV.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            this.CategorieGV.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CategorieGV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.CategorieGV.ColumnHeadersHeight = 25;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.CategorieGV.DefaultCellStyle = dataGridViewCellStyle6;
            this.CategorieGV.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.CategorieGV.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.CategorieGV.Location = new System.Drawing.Point(384, 39);
            this.CategorieGV.Name = "CategorieGV";
            this.CategorieGV.RowHeadersVisible = false;
            this.CategorieGV.RowHeadersWidth = 51;
            this.CategorieGV.RowTemplate.Height = 30;
            this.CategorieGV.Size = new System.Drawing.Size(664, 424);
            this.CategorieGV.TabIndex = 14;
            this.CategorieGV.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.CategorieGV.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.CategorieGV.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.CategorieGV.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.CategorieGV.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.CategorieGV.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.CategorieGV.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.CategorieGV.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.CategorieGV.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.CategorieGV.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Arial", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CategorieGV.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.CategorieGV.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.CategorieGV.ThemeStyle.HeaderStyle.Height = 25;
            this.CategorieGV.ThemeStyle.ReadOnly = false;
            this.CategorieGV.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.CategorieGV.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.CategorieGV.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CategorieGV.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.CategorieGV.ThemeStyle.RowsStyle.Height = 30;
            this.CategorieGV.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.CategorieGV.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.CategorieGV.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CategorieGV_CellContentClick);
            // 
            // deletectgrbtn
            // 
            this.deletectgrbtn.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.deletectgrbtn.FlatAppearance.BorderSize = 2;
            this.deletectgrbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deletectgrbtn.Location = new System.Drawing.Point(246, 206);
            this.deletectgrbtn.Name = "deletectgrbtn";
            this.deletectgrbtn.Size = new System.Drawing.Size(103, 39);
            this.deletectgrbtn.TabIndex = 13;
            this.deletectgrbtn.Text = "Supprimer";
            this.deletectgrbtn.UseVisualStyleBackColor = true;
            this.deletectgrbtn.Click += new System.EventHandler(this.deletectgrbtn_Click);
            // 
            // Editcatgrbtn
            // 
            this.Editcatgrbtn.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.Editcatgrbtn.FlatAppearance.BorderSize = 2;
            this.Editcatgrbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Editcatgrbtn.Location = new System.Drawing.Point(137, 206);
            this.Editcatgrbtn.Name = "Editcatgrbtn";
            this.Editcatgrbtn.Size = new System.Drawing.Size(103, 39);
            this.Editcatgrbtn.TabIndex = 12;
            this.Editcatgrbtn.Text = "Modifier";
            this.Editcatgrbtn.UseVisualStyleBackColor = true;
            this.Editcatgrbtn.Click += new System.EventHandler(this.Editcatgrbtn_Click);
            // 
            // addctgrbtn
            // 
            this.addctgrbtn.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.addctgrbtn.FlatAppearance.BorderSize = 2;
            this.addctgrbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addctgrbtn.Location = new System.Drawing.Point(28, 206);
            this.addctgrbtn.Name = "addctgrbtn";
            this.addctgrbtn.Size = new System.Drawing.Size(103, 39);
            this.addctgrbtn.TabIndex = 11;
            this.addctgrbtn.Text = "Ajouter";
            this.addctgrbtn.UseVisualStyleBackColor = true;
            this.addctgrbtn.Click += new System.EventHandler(this.addctgrbtn_Click);
            // 
            // nomctgrtb
            // 
            this.nomctgrtb.AcceptsReturn = false;
            this.nomctgrtb.AcceptsTab = false;
            this.nomctgrtb.AnimationSpeed = 200;
            this.nomctgrtb.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.nomctgrtb.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.nomctgrtb.BackColor = System.Drawing.Color.Transparent;
            this.nomctgrtb.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("nomctgrtb.BackgroundImage")));
            this.nomctgrtb.BorderColorActive = System.Drawing.Color.DodgerBlue;
            this.nomctgrtb.BorderColorDisabled = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.nomctgrtb.BorderColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.nomctgrtb.BorderColorIdle = System.Drawing.Color.Silver;
            this.nomctgrtb.BorderRadius = 1;
            this.nomctgrtb.BorderThickness = 1;
            this.nomctgrtb.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.nomctgrtb.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.nomctgrtb.DefaultFont = new System.Drawing.Font("Segoe UI", 9.25F);
            this.nomctgrtb.DefaultText = "";
            this.nomctgrtb.FillColor = System.Drawing.Color.White;
            this.nomctgrtb.HideSelection = true;
            this.nomctgrtb.IconLeft = null;
            this.nomctgrtb.IconLeftCursor = System.Windows.Forms.Cursors.IBeam;
            this.nomctgrtb.IconPadding = 10;
            this.nomctgrtb.IconRight = null;
            this.nomctgrtb.IconRightCursor = System.Windows.Forms.Cursors.IBeam;
            this.nomctgrtb.Lines = new string[0];
            this.nomctgrtb.Location = new System.Drawing.Point(28, 95);
            this.nomctgrtb.MaxLength = 32767;
            this.nomctgrtb.MinimumSize = new System.Drawing.Size(1, 1);
            this.nomctgrtb.Modified = false;
            this.nomctgrtb.Multiline = false;
            this.nomctgrtb.Name = "nomctgrtb";
            stateProperties5.BorderColor = System.Drawing.Color.DodgerBlue;
            stateProperties5.FillColor = System.Drawing.Color.Empty;
            stateProperties5.ForeColor = System.Drawing.Color.Empty;
            stateProperties5.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.nomctgrtb.OnActiveState = stateProperties5;
            stateProperties6.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            stateProperties6.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            stateProperties6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            stateProperties6.PlaceholderForeColor = System.Drawing.Color.DarkGray;
            this.nomctgrtb.OnDisabledState = stateProperties6;
            stateProperties7.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            stateProperties7.FillColor = System.Drawing.Color.Empty;
            stateProperties7.ForeColor = System.Drawing.Color.Empty;
            stateProperties7.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.nomctgrtb.OnHoverState = stateProperties7;
            stateProperties8.BorderColor = System.Drawing.Color.Silver;
            stateProperties8.FillColor = System.Drawing.Color.White;
            stateProperties8.ForeColor = System.Drawing.Color.Empty;
            stateProperties8.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.nomctgrtb.OnIdleState = stateProperties8;
            this.nomctgrtb.Padding = new System.Windows.Forms.Padding(3);
            this.nomctgrtb.PasswordChar = '\0';
            this.nomctgrtb.PlaceholderForeColor = System.Drawing.Color.Silver;
            this.nomctgrtb.PlaceholderText = "Nom du categorie";
            this.nomctgrtb.ReadOnly = false;
            this.nomctgrtb.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.nomctgrtb.SelectedText = "";
            this.nomctgrtb.SelectionLength = 0;
            this.nomctgrtb.SelectionStart = 0;
            this.nomctgrtb.ShortcutsEnabled = true;
            this.nomctgrtb.Size = new System.Drawing.Size(321, 54);
            this.nomctgrtb.Style = Bunifu.UI.WinForms.BunifuTextBox._Style.Bunifu;
            this.nomctgrtb.TabIndex = 9;
            this.nomctgrtb.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.nomctgrtb.TextMarginBottom = 0;
            this.nomctgrtb.TextMarginLeft = 3;
            this.nomctgrtb.TextMarginTop = 0;
            this.nomctgrtb.TextPlaceholder = "Nom du categorie";
            this.nomctgrtb.UseSystemPasswordChar = false;
            this.nomctgrtb.WordWrap = true;
            this.nomctgrtb.TextChanged += new System.EventHandler(this.nomclientaddtb_TextChanged);
            // 
            // retourbtn
            // 
            this.retourbtn.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.retourbtn.FlatAppearance.BorderSize = 2;
            this.retourbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.retourbtn.Location = new System.Drawing.Point(28, 301);
            this.retourbtn.Name = "retourbtn";
            this.retourbtn.Size = new System.Drawing.Size(103, 39);
            this.retourbtn.TabIndex = 18;
            this.retourbtn.Text = "Retour";
            this.retourbtn.UseVisualStyleBackColor = true;
            this.retourbtn.Click += new System.EventHandler(this.retourbtn_Click);
            // 
            // gestiondecategoriedesproduits
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1076, 650);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "gestiondecategoriedesproduits";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestion des categories des produits";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CategorieGV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private Guna.UI2.WinForms.Guna2DataGridView CategorieGV;
        private System.Windows.Forms.Button deletectgrbtn;
        private System.Windows.Forms.Button Editcatgrbtn;
        private System.Windows.Forms.Button addctgrbtn;
        private Bunifu.UI.WinForms.BunifuTextBox nomctgrtb;
        private System.Windows.Forms.Button retourbtn;
    }
}