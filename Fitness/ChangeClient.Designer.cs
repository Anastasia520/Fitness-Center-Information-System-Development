namespace Fitness
{
    partial class ChangeClient
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
            this.btnChange = new System.Windows.Forms.Button();
            this.txtId = new System.Windows.Forms.TextBox();
            this.lblId = new System.Windows.Forms.Label();
            this.cmbPass = new System.Windows.Forms.ComboBox();
            this.lblPass = new System.Windows.Forms.Label();
            this.btnGo = new System.Windows.Forms.Button();
            this.dtpBirth = new System.Windows.Forms.DateTimePicker();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtSurname = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.cmbSex = new System.Windows.Forms.ComboBox();
            this.lblSex = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblBirth = new System.Windows.Forms.Label();
            this.lblSurname = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblRegChange = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnChange
            // 
            this.btnChange.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(150)))), ((int)(((byte)(153)))));
            this.btnChange.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(150)))), ((int)(((byte)(153)))));
            this.btnChange.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChange.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnChange.Location = new System.Drawing.Point(289, 97);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(122, 35);
            this.btnChange.TabIndex = 38;
            this.btnChange.Text = "Изменить";
            this.btnChange.UseVisualStyleBackColor = false;
            this.btnChange.Visible = false;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // txtId
            // 
            this.txtId.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtId.Location = new System.Drawing.Point(211, 102);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(59, 28);
            this.txtId.TabIndex = 37;
            this.txtId.Visible = false;
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblId.Location = new System.Drawing.Point(72, 102);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(121, 23);
            this.lblId.TabIndex = 36;
            this.lblId.Text = "ID клиента:";
            this.lblId.Visible = false;
            // 
            // cmbPass
            // 
            this.cmbPass.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbPass.FormattingEnabled = true;
            this.cmbPass.Items.AddRange(new object[] {
            "Bronze",
            "Gold pass",
            "Silver pass",
            "Simple pass"});
            this.cmbPass.Location = new System.Drawing.Point(211, 440);
            this.cmbPass.Name = "cmbPass";
            this.cmbPass.Size = new System.Drawing.Size(121, 29);
            this.cmbPass.TabIndex = 35;
            this.cmbPass.Visible = false;
            // 
            // lblPass
            // 
            this.lblPass.AutoSize = true;
            this.lblPass.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblPass.Location = new System.Drawing.Point(61, 441);
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size(132, 23);
            this.lblPass.TabIndex = 34;
            this.lblPass.Text = "Абонемент:";
            this.lblPass.Visible = false;
            // 
            // btnGo
            // 
            this.btnGo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(150)))), ((int)(((byte)(153)))));
            this.btnGo.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(150)))), ((int)(((byte)(153)))));
            this.btnGo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGo.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnGo.Location = new System.Drawing.Point(138, 497);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(212, 62);
            this.btnGo.TabIndex = 31;
            this.btnGo.Text = "Изменить данные";
            this.btnGo.UseVisualStyleBackColor = false;
            this.btnGo.Visible = false;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // dtpBirth
            // 
            this.dtpBirth.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dtpBirth.Location = new System.Drawing.Point(211, 268);
            this.dtpBirth.Name = "dtpBirth";
            this.dtpBirth.Size = new System.Drawing.Size(200, 28);
            this.dtpBirth.TabIndex = 30;
            this.dtpBirth.Visible = false;
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtEmail.Location = new System.Drawing.Point(211, 388);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(100, 28);
            this.txtEmail.TabIndex = 29;
            this.txtEmail.Visible = false;
            // 
            // txtSurname
            // 
            this.txtSurname.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtSurname.Location = new System.Drawing.Point(211, 206);
            this.txtSurname.Name = "txtSurname";
            this.txtSurname.Size = new System.Drawing.Size(100, 28);
            this.txtSurname.TabIndex = 28;
            this.txtSurname.Visible = false;
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtName.Location = new System.Drawing.Point(211, 150);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(100, 28);
            this.txtName.TabIndex = 27;
            this.txtName.Visible = false;
            // 
            // cmbSex
            // 
            this.cmbSex.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbSex.FormattingEnabled = true;
            this.cmbSex.Items.AddRange(new object[] {
            "М",
            "Ж"});
            this.cmbSex.Location = new System.Drawing.Point(211, 331);
            this.cmbSex.Name = "cmbSex";
            this.cmbSex.Size = new System.Drawing.Size(121, 29);
            this.cmbSex.TabIndex = 26;
            this.cmbSex.Visible = false;
            // 
            // lblSex
            // 
            this.lblSex.AutoSize = true;
            this.lblSex.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblSex.Location = new System.Drawing.Point(140, 331);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(53, 23);
            this.lblSex.TabIndex = 25;
            this.lblSex.Text = "Пол:";
            this.lblSex.Visible = false;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblEmail.Location = new System.Drawing.Point(126, 388);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(67, 23);
            this.lblEmail.TabIndex = 24;
            this.lblEmail.Text = "Email:";
            this.lblEmail.Visible = false;
            // 
            // lblBirth
            // 
            this.lblBirth.AutoSize = true;
            this.lblBirth.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblBirth.Location = new System.Drawing.Point(63, 250);
            this.lblBirth.Name = "lblBirth";
            this.lblBirth.Size = new System.Drawing.Size(130, 46);
            this.lblBirth.TabIndex = 23;
            this.lblBirth.Text = "Дата \r\nрожджения:";
            this.lblBirth.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblBirth.Visible = false;
            // 
            // lblSurname
            // 
            this.lblSurname.AutoSize = true;
            this.lblSurname.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblSurname.Location = new System.Drawing.Point(85, 206);
            this.lblSurname.Name = "lblSurname";
            this.lblSurname.Size = new System.Drawing.Size(108, 23);
            this.lblSurname.TabIndex = 22;
            this.lblSurname.Text = "Фамилия:";
            this.lblSurname.Visible = false;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblName.Location = new System.Drawing.Point(136, 155);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(57, 23);
            this.lblName.TabIndex = 21;
            this.lblName.Text = "Имя:";
            this.lblName.Visible = false;
            // 
            // lblRegChange
            // 
            this.lblRegChange.AutoSize = true;
            this.lblRegChange.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblRegChange.Location = new System.Drawing.Point(33, 1);
            this.lblRegChange.Name = "lblRegChange";
            this.lblRegChange.Size = new System.Drawing.Size(225, 60);
            this.lblRegChange.TabIndex = 20;
            this.lblRegChange.Text = "Изменить данные\r\nклиента";
            this.lblRegChange.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(193)))), ((int)(((byte)(26)))));
            this.panel1.Location = new System.Drawing.Point(1, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(462, 17);
            this.panel1.TabIndex = 39;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(222)))), ((int)(((byte)(152)))));
            this.panel2.Controls.Add(this.lblRegChange);
            this.panel2.Location = new System.Drawing.Point(92, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(282, 61);
            this.panel2.TabIndex = 40;
            // 
            // ChangeClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(248)))), ((int)(((byte)(235)))));
            this.ClientSize = new System.Drawing.Size(462, 593);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnChange);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.cmbPass);
            this.Controls.Add(this.lblPass);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.dtpBirth);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtSurname);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.cmbSex);
            this.Controls.Add(this.lblSex);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.lblBirth);
            this.Controls.Add(this.lblSurname);
            this.Controls.Add(this.lblName);
            this.Name = "ChangeClient";
            this.Text = "ChangeClient";
            this.Load += new System.EventHandler(this.ChangeClient_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnChange;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.ComboBox cmbPass;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.DateTimePicker dtpBirth;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtSurname;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.ComboBox cmbSex;
        private System.Windows.Forms.Label lblSex;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblBirth;
        private System.Windows.Forms.Label lblSurname;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblRegChange;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}