namespace дом_стр_322
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.description = new System.Windows.Forms.TextBox();
            this.exist = new System.Windows.Forms.ComboBox();
            this.goHere = new System.Windows.Forms.Button();
            this.goThroughtTheDoor = new System.Windows.Forms.Button();
            this.check = new System.Windows.Forms.Button();
            this.Hide = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // description
            // 
            this.description.Location = new System.Drawing.Point(37, 12);
            this.description.Multiline = true;
            this.description.Name = "description";
            this.description.Size = new System.Drawing.Size(291, 185);
            this.description.TabIndex = 0;
            // 
            // exist
            // 
            this.exist.FormattingEnabled = true;
            this.exist.Location = new System.Drawing.Point(207, 212);
            this.exist.Name = "exist";
            this.exist.Size = new System.Drawing.Size(121, 24);
            this.exist.TabIndex = 1;
            this.exist.SelectedIndexChanged += new System.EventHandler(this.exist_SelectedIndexChanged);
            // 
            // goHere
            // 
            this.goHere.Location = new System.Drawing.Point(37, 212);
            this.goHere.Name = "goHere";
            this.goHere.Size = new System.Drawing.Size(75, 23);
            this.goHere.TabIndex = 2;
            this.goHere.Text = "Go here";
            this.goHere.UseVisualStyleBackColor = true;
            this.goHere.Click += new System.EventHandler(this.goHere_Click);
            // 
            // goThroughtTheDoor
            // 
            this.goThroughtTheDoor.Location = new System.Drawing.Point(37, 253);
            this.goThroughtTheDoor.Name = "goThroughtTheDoor";
            this.goThroughtTheDoor.Size = new System.Drawing.Size(291, 24);
            this.goThroughtTheDoor.TabIndex = 3;
            this.goThroughtTheDoor.Text = "Go throught the door";
            this.goThroughtTheDoor.UseVisualStyleBackColor = true;
            this.goThroughtTheDoor.Visible = false;
            this.goThroughtTheDoor.Click += new System.EventHandler(this.goThroughtTheDoor_Click);
            // 
            // check
            // 
            this.check.Location = new System.Drawing.Point(37, 298);
            this.check.Name = "check";
            this.check.Size = new System.Drawing.Size(291, 24);
            this.check.TabIndex = 4;
            this.check.Text = "check";
            this.check.UseVisualStyleBackColor = true;
            this.check.Click += new System.EventHandler(this.check_Click);
            // 
            // Hide
            // 
            this.Hide.Location = new System.Drawing.Point(37, 342);
            this.Hide.Name = "Hide";
            this.Hide.Size = new System.Drawing.Size(291, 28);
            this.Hide.TabIndex = 5;
            this.Hide.Text = "Hide";
            this.Hide.UseVisualStyleBackColor = true;
            this.Hide.Click += new System.EventHandler(this.Hide_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Hide);
            this.Controls.Add(this.check);
            this.Controls.Add(this.goThroughtTheDoor);
            this.Controls.Add(this.goHere);
            this.Controls.Add(this.exist);
            this.Controls.Add(this.description);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox description;
        private System.Windows.Forms.ComboBox exist;
        private System.Windows.Forms.Button goHere;
        private System.Windows.Forms.Button goThroughtTheDoor;
        private System.Windows.Forms.Button check;
        private System.Windows.Forms.Button Hide;
        private System.Windows.Forms.Timer timer1;
    }
}

