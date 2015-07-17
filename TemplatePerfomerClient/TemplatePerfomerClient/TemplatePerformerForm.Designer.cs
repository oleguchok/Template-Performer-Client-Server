namespace TemplatePerfomerClient
{
    partial class TemplatePerformerForm
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
            this.groupBoxLanguage = new System.Windows.Forms.GroupBox();
            this.radioButtonCSharp = new System.Windows.Forms.RadioButton();
            this.radioButtonJava = new System.Windows.Forms.RadioButton();
            this.textBoxTemplateText = new System.Windows.Forms.TextBox();
            this.labelTemplateText = new System.Windows.Forms.Label();
            this.listBoxNamespaces = new System.Windows.Forms.ListBox();
            this.textBoxNamespace = new System.Windows.Forms.TextBox();
            this.buttonAddNamespace = new System.Windows.Forms.Button();
            this.listBoxVariables = new System.Windows.Forms.ListBox();
            this.labelNamespaces = new System.Windows.Forms.Label();
            this.labelVariables = new System.Windows.Forms.Label();
            this.buttonDeleteNamespace = new System.Windows.Forms.Button();
            this.labelChoosingNamespace = new System.Windows.Forms.Label();
            this.comboBoxTypesOfVariables = new System.Windows.Forms.ComboBox();
            this.textBoxNameOfVariable = new System.Windows.Forms.TextBox();
            this.labelChoosingVariable = new System.Windows.Forms.Label();
            this.buttonAddVariable = new System.Windows.Forms.Button();
            this.buttonDeleteVariable = new System.Windows.Forms.Button();
            this.labelResult = new System.Windows.Forms.Label();
            this.buttonRender = new System.Windows.Forms.Button();
            this.textBoxResult = new System.Windows.Forms.TextBox();
            this.groupBoxLanguage.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxLanguage
            // 
            this.groupBoxLanguage.Controls.Add(this.radioButtonJava);
            this.groupBoxLanguage.Controls.Add(this.radioButtonCSharp);
            this.groupBoxLanguage.Location = new System.Drawing.Point(206, 169);
            this.groupBoxLanguage.Name = "groupBoxLanguage";
            this.groupBoxLanguage.Size = new System.Drawing.Size(108, 66);
            this.groupBoxLanguage.TabIndex = 1;
            this.groupBoxLanguage.TabStop = false;
            this.groupBoxLanguage.Text = "Choose language";
            // 
            // radioButtonCSharp
            // 
            this.radioButtonCSharp.AutoSize = true;
            this.radioButtonCSharp.Location = new System.Drawing.Point(6, 19);
            this.radioButtonCSharp.Name = "radioButtonCSharp";
            this.radioButtonCSharp.Size = new System.Drawing.Size(39, 17);
            this.radioButtonCSharp.TabIndex = 0;
            this.radioButtonCSharp.Text = "C#";
            this.radioButtonCSharp.UseVisualStyleBackColor = true;
            // 
            // radioButtonJava
            // 
            this.radioButtonJava.AutoSize = true;
            this.radioButtonJava.Location = new System.Drawing.Point(6, 42);
            this.radioButtonJava.Name = "radioButtonJava";
            this.radioButtonJava.Size = new System.Drawing.Size(48, 17);
            this.radioButtonJava.TabIndex = 1;
            this.radioButtonJava.Text = "Java";
            this.radioButtonJava.UseVisualStyleBackColor = true;
            // 
            // textBoxTemplateText
            // 
            this.textBoxTemplateText.Location = new System.Drawing.Point(12, 25);
            this.textBoxTemplateText.Multiline = true;
            this.textBoxTemplateText.Name = "textBoxTemplateText";
            this.textBoxTemplateText.Size = new System.Drawing.Size(302, 112);
            this.textBoxTemplateText.TabIndex = 2;
            // 
            // labelTemplateText
            // 
            this.labelTemplateText.AutoSize = true;
            this.labelTemplateText.Location = new System.Drawing.Point(9, 9);
            this.labelTemplateText.Name = "labelTemplateText";
            this.labelTemplateText.Size = new System.Drawing.Size(75, 13);
            this.labelTemplateText.TabIndex = 3;
            this.labelTemplateText.Text = "Template Text";
            // 
            // listBoxNamespaces
            // 
            this.listBoxNamespaces.FormattingEnabled = true;
            this.listBoxNamespaces.Location = new System.Drawing.Point(12, 169);
            this.listBoxNamespaces.Name = "listBoxNamespaces";
            this.listBoxNamespaces.Size = new System.Drawing.Size(188, 82);
            this.listBoxNamespaces.TabIndex = 5;
            // 
            // textBoxNamespace
            // 
            this.textBoxNamespace.Location = new System.Drawing.Point(12, 261);
            this.textBoxNamespace.Name = "textBoxNamespace";
            this.textBoxNamespace.Size = new System.Drawing.Size(188, 20);
            this.textBoxNamespace.TabIndex = 6;
            // 
            // buttonAddNamespace
            // 
            this.buttonAddNamespace.Location = new System.Drawing.Point(213, 261);
            this.buttonAddNamespace.Name = "buttonAddNamespace";
            this.buttonAddNamespace.Size = new System.Drawing.Size(75, 24);
            this.buttonAddNamespace.TabIndex = 7;
            this.buttonAddNamespace.Text = "Add";
            this.buttonAddNamespace.UseVisualStyleBackColor = true;
            // 
            // listBoxVariables
            // 
            this.listBoxVariables.FormattingEnabled = true;
            this.listBoxVariables.Location = new System.Drawing.Point(12, 330);
            this.listBoxVariables.Name = "listBoxVariables";
            this.listBoxVariables.Size = new System.Drawing.Size(188, 82);
            this.listBoxVariables.TabIndex = 8;
            // 
            // labelNamespaces
            // 
            this.labelNamespaces.AutoSize = true;
            this.labelNamespaces.Location = new System.Drawing.Point(15, 146);
            this.labelNamespaces.Name = "labelNamespaces";
            this.labelNamespaces.Size = new System.Drawing.Size(0, 13);
            this.labelNamespaces.TabIndex = 9;
            // 
            // labelVariables
            // 
            this.labelVariables.AutoSize = true;
            this.labelVariables.Location = new System.Drawing.Point(9, 314);
            this.labelVariables.Name = "labelVariables";
            this.labelVariables.Size = new System.Drawing.Size(50, 13);
            this.labelVariables.TabIndex = 10;
            this.labelVariables.Text = "Variables";
            // 
            // buttonDeleteNamespace
            // 
            this.buttonDeleteNamespace.Location = new System.Drawing.Point(125, 287);
            this.buttonDeleteNamespace.Name = "buttonDeleteNamespace";
            this.buttonDeleteNamespace.Size = new System.Drawing.Size(75, 20);
            this.buttonDeleteNamespace.TabIndex = 11;
            this.buttonDeleteNamespace.Text = "Delete";
            this.buttonDeleteNamespace.UseVisualStyleBackColor = true;
            // 
            // labelChoosingNamespace
            // 
            this.labelChoosingNamespace.AutoSize = true;
            this.labelChoosingNamespace.Location = new System.Drawing.Point(15, 292);
            this.labelChoosingNamespace.Name = "labelChoosingNamespace";
            this.labelChoosingNamespace.Size = new System.Drawing.Size(0, 13);
            this.labelChoosingNamespace.TabIndex = 12;
            // 
            // comboBoxTypesOfVariables
            // 
            this.comboBoxTypesOfVariables.FormattingEnabled = true;
            this.comboBoxTypesOfVariables.Location = new System.Drawing.Point(12, 421);
            this.comboBoxTypesOfVariables.Name = "comboBoxTypesOfVariables";
            this.comboBoxTypesOfVariables.Size = new System.Drawing.Size(188, 21);
            this.comboBoxTypesOfVariables.TabIndex = 13;
            // 
            // textBoxNameOfVariable
            // 
            this.textBoxNameOfVariable.Location = new System.Drawing.Point(12, 448);
            this.textBoxNameOfVariable.Name = "textBoxNameOfVariable";
            this.textBoxNameOfVariable.Size = new System.Drawing.Size(188, 20);
            this.textBoxNameOfVariable.TabIndex = 14;
            // 
            // labelChoosingVariable
            // 
            this.labelChoosingVariable.AutoSize = true;
            this.labelChoosingVariable.Location = new System.Drawing.Point(15, 478);
            this.labelChoosingVariable.Name = "labelChoosingVariable";
            this.labelChoosingVariable.Size = new System.Drawing.Size(0, 13);
            this.labelChoosingVariable.TabIndex = 15;
            // 
            // buttonAddVariable
            // 
            this.buttonAddVariable.Location = new System.Drawing.Point(212, 433);
            this.buttonAddVariable.Name = "buttonAddVariable";
            this.buttonAddVariable.Size = new System.Drawing.Size(76, 24);
            this.buttonAddVariable.TabIndex = 16;
            this.buttonAddVariable.Text = "Add";
            this.buttonAddVariable.UseVisualStyleBackColor = true;
            // 
            // buttonDeleteVariable
            // 
            this.buttonDeleteVariable.Location = new System.Drawing.Point(125, 473);
            this.buttonDeleteVariable.Name = "buttonDeleteVariable";
            this.buttonDeleteVariable.Size = new System.Drawing.Size(75, 23);
            this.buttonDeleteVariable.TabIndex = 17;
            this.buttonDeleteVariable.Text = "Delete";
            this.buttonDeleteVariable.UseVisualStyleBackColor = true;
            // 
            // labelResult
            // 
            this.labelResult.AutoSize = true;
            this.labelResult.Location = new System.Drawing.Point(385, 10);
            this.labelResult.Name = "labelResult";
            this.labelResult.Size = new System.Drawing.Size(37, 13);
            this.labelResult.TabIndex = 18;
            this.labelResult.Text = "Result";
            // 
            // buttonRender
            // 
            this.buttonRender.Location = new System.Drawing.Point(66, 511);
            this.buttonRender.Name = "buttonRender";
            this.buttonRender.Size = new System.Drawing.Size(134, 23);
            this.buttonRender.TabIndex = 19;
            this.buttonRender.Text = "Render";
            this.buttonRender.UseVisualStyleBackColor = true;
            // 
            // textBoxResult
            // 
            this.textBoxResult.Location = new System.Drawing.Point(388, 26);
            this.textBoxResult.Multiline = true;
            this.textBoxResult.Name = "textBoxResult";
            this.textBoxResult.Size = new System.Drawing.Size(302, 120);
            this.textBoxResult.TabIndex = 20;
            // 
            // TemplatePerformerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 541);
            this.Controls.Add(this.textBoxResult);
            this.Controls.Add(this.buttonRender);
            this.Controls.Add(this.labelResult);
            this.Controls.Add(this.buttonDeleteVariable);
            this.Controls.Add(this.buttonAddVariable);
            this.Controls.Add(this.labelChoosingVariable);
            this.Controls.Add(this.textBoxNameOfVariable);
            this.Controls.Add(this.comboBoxTypesOfVariables);
            this.Controls.Add(this.labelChoosingNamespace);
            this.Controls.Add(this.buttonDeleteNamespace);
            this.Controls.Add(this.labelVariables);
            this.Controls.Add(this.labelNamespaces);
            this.Controls.Add(this.listBoxVariables);
            this.Controls.Add(this.buttonAddNamespace);
            this.Controls.Add(this.textBoxNamespace);
            this.Controls.Add(this.listBoxNamespaces);
            this.Controls.Add(this.labelTemplateText);
            this.Controls.Add(this.textBoxTemplateText);
            this.Controls.Add(this.groupBoxLanguage);
            this.Name = "TemplatePerformerForm";
            this.Text = "Template Performer";
            this.groupBoxLanguage.ResumeLayout(false);
            this.groupBoxLanguage.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxLanguage;
        private System.Windows.Forms.RadioButton radioButtonJava;
        private System.Windows.Forms.RadioButton radioButtonCSharp;
        private System.Windows.Forms.TextBox textBoxTemplateText;
        private System.Windows.Forms.Label labelTemplateText;
        private System.Windows.Forms.ListBox listBoxNamespaces;
        private System.Windows.Forms.TextBox textBoxNamespace;
        private System.Windows.Forms.Button buttonAddNamespace;
        private System.Windows.Forms.ListBox listBoxVariables;
        private System.Windows.Forms.Label labelNamespaces;
        private System.Windows.Forms.Label labelVariables;
        private System.Windows.Forms.Button buttonDeleteNamespace;
        private System.Windows.Forms.Label labelChoosingNamespace;
        private System.Windows.Forms.ComboBox comboBoxTypesOfVariables;
        private System.Windows.Forms.TextBox textBoxNameOfVariable;
        private System.Windows.Forms.Label labelChoosingVariable;
        private System.Windows.Forms.Button buttonAddVariable;
        private System.Windows.Forms.Button buttonDeleteVariable;
        private System.Windows.Forms.Label labelResult;
        private System.Windows.Forms.Button buttonRender;
        private System.Windows.Forms.TextBox textBoxResult;
    }
}

