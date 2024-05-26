namespace SIMA.LicenseKeyGenerator
{
    partial class Form1
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
            btn_CreateLicense = new Button();
            txt_Domain = new TextBox();
            label1 = new Label();
            txt_Version = new TextBox();
            label2 = new Label();
            lb_date = new Label();
            rtxt_License = new RichTextBox();
            label3 = new Label();
            chk_Issue = new CheckBox();
            chk_WorkFlow = new CheckBox();
            msk_date = new MaskedTextBox();
            txt_PassPharase = new TextBox();
            label4 = new Label();
            rtxt_PublicKey = new RichTextBox();
            label5 = new Label();
            label6 = new Label();
            SuspendLayout();
            // 
            // btn_CreateLicense
            // 
            btn_CreateLicense.Location = new Point(99, 161);
            btn_CreateLicense.Name = "btn_CreateLicense";
            btn_CreateLicense.Size = new Size(163, 40);
            btn_CreateLicense.TabIndex = 0;
            btn_CreateLicense.Text = "ایجاد لایسنس";
            btn_CreateLicense.UseVisualStyleBackColor = true;
            btn_CreateLicense.Click += btn_CreateLicense_Click;
            // 
            // txt_Domain
            // 
            txt_Domain.Location = new Point(99, 33);
            txt_Domain.Name = "txt_Domain";
            txt_Domain.Size = new Size(208, 23);
            txt_Domain.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(24, 41);
            label1.Name = "label1";
            label1.Size = new Size(56, 15);
            label1.TabIndex = 2;
            label1.Text = "نام دامنه :";
            // 
            // txt_Version
            // 
            txt_Version.Location = new Point(382, 38);
            txt_Version.Name = "txt_Version";
            txt_Version.Size = new Size(199, 23);
            txt_Version.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(343, 46);
            label2.Name = "label2";
            label2.Size = new Size(36, 15);
            label2.TabIndex = 2;
            label2.Text = "ورژن :";
            // 
            // lb_date
            // 
            lb_date.AutoSize = true;
            lb_date.Location = new Point(46, 70);
            lb_date.Name = "lb_date";
            lb_date.Size = new Size(34, 15);
            lb_date.TabIndex = 2;
            lb_date.Text = "تاریخ:";
            // 
            // rtxt_License
            // 
            rtxt_License.Location = new Point(99, 231);
            rtxt_License.Name = "rtxt_License";
            rtxt_License.ReadOnly = true;
            rtxt_License.Size = new Size(487, 96);
            rtxt_License.TabIndex = 3;
            rtxt_License.Text = "";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(35, 103);
            label3.Name = "label3";
            label3.Size = new Size(45, 15);
            label3.TabIndex = 4;
            label3.Text = "فیچرها :";
            // 
            // chk_Issue
            // 
            chk_Issue.AutoSize = true;
            chk_Issue.Location = new Point(99, 103);
            chk_Issue.Name = "chk_Issue";
            chk_Issue.Size = new Size(90, 19);
            chk_Issue.TabIndex = 5;
            chk_Issue.Text = "مدیریت ایشو";
            chk_Issue.UseVisualStyleBackColor = true;
            // 
            // chk_WorkFlow
            // 
            chk_WorkFlow.AutoSize = true;
            chk_WorkFlow.Location = new Point(195, 103);
            chk_WorkFlow.Name = "chk_WorkFlow";
            chk_WorkFlow.Size = new Size(78, 19);
            chk_WorkFlow.TabIndex = 5;
            chk_WorkFlow.Text = "مدیر فرآیند";
            chk_WorkFlow.UseVisualStyleBackColor = true;
            // 
            // msk_date
            // 
            msk_date.Location = new Point(99, 68);
            msk_date.Mask = "0000/00/00";
            msk_date.Name = "msk_date";
            msk_date.Size = new Size(208, 23);
            msk_date.TabIndex = 7;
            msk_date.ValidatingType = typeof(DateTime);
            // 
            // txt_PassPharase
            // 
            txt_PassPharase.Location = new Point(382, 70);
            txt_PassPharase.Name = "txt_PassPharase";
            txt_PassPharase.Size = new Size(199, 23);
            txt_PassPharase.TabIndex = 1;
            txt_PassPharase.Text = "SIM@_DEVEL0PMENT";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(313, 78);
            label4.Name = "label4";
            label4.Size = new Size(66, 15);
            label4.TabIndex = 2;
            label4.Text = "کد رمزنگاری:";
            // 
            // rtxt_PublicKey
            // 
            rtxt_PublicKey.Location = new Point(99, 350);
            rtxt_PublicKey.Name = "rtxt_PublicKey";
            rtxt_PublicKey.ReadOnly = true;
            rtxt_PublicKey.Size = new Size(487, 96);
            rtxt_PublicKey.TabIndex = 3;
            rtxt_PublicKey.Text = "";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(35, 312);
            label5.Name = "label5";
            label5.Size = new Size(52, 15);
            label5.TabIndex = 4;
            label5.Text = "لایسنس :";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(35, 431);
            label6.Name = "label6";
            label6.Size = new Size(65, 15);
            label6.TabIndex = 4;
            label6.Text = "کد عمومی :";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(598, 458);
            Controls.Add(msk_date);
            Controls.Add(chk_WorkFlow);
            Controls.Add(chk_Issue);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(rtxt_PublicKey);
            Controls.Add(rtxt_License);
            Controls.Add(lb_date);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txt_PassPharase);
            Controls.Add(txt_Version);
            Controls.Add(txt_Domain);
            Controls.Add(btn_CreateLicense);
            Name = "Form1";
            Text = "لایسنس";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btn_CreateLicense;
        private TextBox txt_Domain;
        private Label label1;
        private TextBox txt_Version;
        private Label label2;
        private Label lb_date;
        private RichTextBox rtxt_License;
        private Label label3;
        private CheckBox chk_Issue;
        private CheckBox chk_WorkFlow;
        private MaskedTextBox msk_date;
        private TextBox txt_PassPharase;
        private Label label4;
        private RichTextBox rtxt_PublicKey;
        private Label label5;
        private Label label6;
    }
}
