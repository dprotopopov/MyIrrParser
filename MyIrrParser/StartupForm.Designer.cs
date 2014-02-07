﻿namespace MyIrrParser
{
    partial class StartupForm
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
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxMaxThreads = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxMaxPages = new System.Windows.Forms.NumericUpDown();
            this.formAssistant1 = new DevExpress.XtraBars.FormAssistant();
            this.checkBoxOnlyTitle = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxRubric = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxMaxThreads)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxMaxPages)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Location = new System.Drawing.Point(144, 24);
            this.textBoxSearch.Multiline = true;
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(315, 23);
            this.textBoxSearch.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Строка поиска";
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(312, 224);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(66, 27);
            this.button1.TabIndex = 3;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(393, 224);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(66, 27);
            this.button2.TabIndex = 4;
            this.button2.Text = "Отмена";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 169);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Количество потоков";
            // 
            // textBoxMaxThreads
            // 
            this.textBoxMaxThreads.Location = new System.Drawing.Point(371, 169);
            this.textBoxMaxThreads.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.textBoxMaxThreads.Name = "textBoxMaxThreads";
            this.textBoxMaxThreads.Size = new System.Drawing.Size(88, 23);
            this.textBoxMaxThreads.TabIndex = 6;
            this.textBoxMaxThreads.Value = new decimal(new int[] {
            24,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(234, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "Максимальное количество страниц";
            // 
            // textBoxMaxPages
            // 
            this.textBoxMaxPages.Location = new System.Drawing.Point(371, 141);
            this.textBoxMaxPages.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.textBoxMaxPages.Name = "textBoxMaxPages";
            this.textBoxMaxPages.Size = new System.Drawing.Size(88, 23);
            this.textBoxMaxPages.TabIndex = 8;
            this.textBoxMaxPages.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // checkBoxOnlyTitle
            // 
            this.checkBoxOnlyTitle.AutoSize = true;
            this.checkBoxOnlyTitle.Location = new System.Drawing.Point(144, 62);
            this.checkBoxOnlyTitle.Name = "checkBoxOnlyTitle";
            this.checkBoxOnlyTitle.Size = new System.Drawing.Size(283, 21);
            this.checkBoxOnlyTitle.TabIndex = 9;
            this.checkBoxOnlyTitle.Text = "Искать только в названиях объявлений";
            this.checkBoxOnlyTitle.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Рубрика";
            // 
            // comboBoxRubric
            // 
            this.comboBoxRubric.FormattingEnabled = true;
            this.comboBoxRubric.Location = new System.Drawing.Point(144, 97);
            this.comboBoxRubric.Name = "comboBoxRubric";
            this.comboBoxRubric.Size = new System.Drawing.Size(315, 24);
            this.comboBoxRubric.TabIndex = 11;
            // 
            // StartupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 278);
            this.Controls.Add(this.comboBoxRubric);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.checkBoxOnlyTitle);
            this.Controls.Add(this.textBoxMaxPages);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxMaxThreads);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxSearch);
            this.Name = "StartupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Setup";
            ((System.ComponentModel.ISupportInitialize)(this.textBoxMaxThreads)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxMaxPages)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        public System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.NumericUpDown textBoxMaxThreads;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.NumericUpDown textBoxMaxPages;
        private DevExpress.XtraBars.FormAssistant formAssistant1;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.ComboBox comboBoxRubric;
        public System.Windows.Forms.CheckBox checkBoxOnlyTitle;
    }
}