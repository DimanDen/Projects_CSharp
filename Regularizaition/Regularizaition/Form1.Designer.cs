namespace Regularizaition
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
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
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.btmLoadRealization = new System.Windows.Forms.Button();
            this.textBoxQteor = new System.Windows.Forms.TextBox();
            this.labelQteor = new System.Windows.Forms.Label();
            this.labelTP = new System.Windows.Forms.Label();
            this.textBoxTp = new System.Windows.Forms.TextBox();
            this.buttonRegularization = new System.Windows.Forms.Button();
            this.labelTeachInterval = new System.Windows.Forms.Label();
            this.textBoxTeachInterval = new System.Windows.Forms.TextBox();
            this.label_T_AKF = new System.Windows.Forms.Label();
            this.textBox_T_AKF = new System.Windows.Forms.TextBox();
            this.label_t1 = new System.Windows.Forms.Label();
            this.textBoxT1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxOptTeachInt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxQminSKO = new System.Windows.Forms.TextBox();
            this.buttonSaveResults = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btmLoadRealization
            // 
            this.btmLoadRealization.Location = new System.Drawing.Point(12, 274);
            this.btmLoadRealization.Name = "btmLoadRealization";
            this.btmLoadRealization.Size = new System.Drawing.Size(136, 88);
            this.btmLoadRealization.TabIndex = 0;
            this.btmLoadRealization.Text = "Загрузить реализацию случайного процесса";
            this.btmLoadRealization.UseVisualStyleBackColor = true;
            this.btmLoadRealization.Click += new System.EventHandler(this.btnLoadRealization_Click);
            // 
            // textBoxQteor
            // 
            this.textBoxQteor.Location = new System.Drawing.Point(203, 100);
            this.textBoxQteor.Name = "textBoxQteor";
            this.textBoxQteor.Size = new System.Drawing.Size(100, 22);
            this.textBoxQteor.TabIndex = 3;
            this.textBoxQteor.Text = "2,333";
            // 
            // labelQteor
            // 
            this.labelQteor.AutoSize = true;
            this.labelQteor.Location = new System.Drawing.Point(5, 105);
            this.labelQteor.Name = "labelQteor";
            this.labelQteor.Size = new System.Drawing.Size(192, 17);
            this.labelQteor.TabIndex = 6;
            this.labelQteor.Text = "Теоретическое значение Q";
            // 
            // labelTP
            // 
            this.labelTP.AutoSize = true;
            this.labelTP.Location = new System.Drawing.Point(5, 29);
            this.labelTP.Name = "labelTP";
            this.labelTP.Size = new System.Drawing.Size(169, 17);
            this.labelTP.TabIndex = 7;
            this.labelTP.Text = "Время прогнозирования";
            // 
            // textBoxTp
            // 
            this.textBoxTp.Location = new System.Drawing.Point(180, 24);
            this.textBoxTp.Name = "textBoxTp";
            this.textBoxTp.Size = new System.Drawing.Size(100, 22);
            this.textBoxTp.TabIndex = 8;
            this.textBoxTp.Text = "160";
            // 
            // buttonRegularization
            // 
            this.buttonRegularization.Location = new System.Drawing.Point(270, 273);
            this.buttonRegularization.Name = "buttonRegularization";
            this.buttonRegularization.Size = new System.Drawing.Size(135, 89);
            this.buttonRegularization.TabIndex = 9;
            this.buttonRegularization.Text = "Провести регуляризацию";
            this.buttonRegularization.UseVisualStyleBackColor = true;
            this.buttonRegularization.Click += new System.EventHandler(this.buttonRegularization_Click);
            // 
            // labelTeachInterval
            // 
            this.labelTeachInterval.AutoSize = true;
            this.labelTeachInterval.Location = new System.Drawing.Point(5, 68);
            this.labelTeachInterval.Name = "labelTeachInterval";
            this.labelTeachInterval.Size = new System.Drawing.Size(139, 17);
            this.labelTeachInterval.TabIndex = 10;
            this.labelTeachInterval.Text = "Интервал обучения";
            // 
            // textBoxTeachInterval
            // 
            this.textBoxTeachInterval.Location = new System.Drawing.Point(155, 63);
            this.textBoxTeachInterval.Name = "textBoxTeachInterval";
            this.textBoxTeachInterval.Size = new System.Drawing.Size(100, 22);
            this.textBoxTeachInterval.TabIndex = 11;
            this.textBoxTeachInterval.Text = "160";
            // 
            // label_T_AKF
            // 
            this.label_T_AKF.AutoSize = true;
            this.label_T_AKF.Location = new System.Drawing.Point(5, 144);
            this.label_T_AKF.Name = "label_T_AKF";
            this.label_T_AKF.Size = new System.Drawing.Size(164, 17);
            this.label_T_AKF.TabIndex = 12;
            this.label_T_AKF.Text = "Период затухания АКФ";
            // 
            // textBox_T_AKF
            // 
            this.textBox_T_AKF.Location = new System.Drawing.Point(186, 139);
            this.textBox_T_AKF.Name = "textBox_T_AKF";
            this.textBox_T_AKF.Size = new System.Drawing.Size(100, 22);
            this.textBox_T_AKF.TabIndex = 13;
            this.textBox_T_AKF.Text = "500";
            // 
            // label_t1
            // 
            this.label_t1.AutoSize = true;
            this.label_t1.Location = new System.Drawing.Point(5, 180);
            this.label_t1.Name = "label_t1";
            this.label_t1.Size = new System.Drawing.Size(90, 17);
            this.label_t1.TabIndex = 14;
            this.label_t1.Text = "Параметр t1";
            // 
            // textBoxT1
            // 
            this.textBoxT1.Location = new System.Drawing.Point(120, 175);
            this.textBoxT1.Name = "textBoxT1";
            this.textBoxT1.Size = new System.Drawing.Size(100, 22);
            this.textBoxT1.TabIndex = 15;
            this.textBoxT1.Text = "77";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(351, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(233, 17);
            this.label1.TabIndex = 16;
            this.label1.Text = "Оптимальный интервал обучения";
            // 
            // textBoxOptTeachInt
            // 
            this.textBoxOptTeachInt.Location = new System.Drawing.Point(587, 19);
            this.textBoxOptTeachInt.Name = "textBoxOptTeachInt";
            this.textBoxOptTeachInt.Size = new System.Drawing.Size(100, 22);
            this.textBoxOptTeachInt.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(351, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(187, 34);
            this.label2.TabIndex = 18;
            this.label2.Text = "Минимальное значение \r\nсреднего квадрата ошибок";
            // 
            // textBoxQminSKO
            // 
            this.textBoxQminSKO.Location = new System.Drawing.Point(587, 63);
            this.textBoxQminSKO.Name = "textBoxQminSKO";
            this.textBoxQminSKO.Size = new System.Drawing.Size(100, 22);
            this.textBoxQminSKO.TabIndex = 19;
            // 
            // buttonSaveResults
            // 
            this.buttonSaveResults.Location = new System.Drawing.Point(535, 274);
            this.buttonSaveResults.Name = "buttonSaveResults";
            this.buttonSaveResults.Size = new System.Drawing.Size(136, 88);
            this.buttonSaveResults.TabIndex = 20;
            this.buttonSaveResults.Text = "Сохранить результаты";
            this.buttonSaveResults.UseVisualStyleBackColor = true;
            this.buttonSaveResults.Click += new System.EventHandler(this.buttonSaveResults_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 405);
            this.Controls.Add(this.buttonSaveResults);
            this.Controls.Add(this.textBoxQminSKO);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxOptTeachInt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxT1);
            this.Controls.Add(this.label_t1);
            this.Controls.Add(this.textBox_T_AKF);
            this.Controls.Add(this.label_T_AKF);
            this.Controls.Add(this.textBoxTeachInterval);
            this.Controls.Add(this.labelTeachInterval);
            this.Controls.Add(this.buttonRegularization);
            this.Controls.Add(this.textBoxTp);
            this.Controls.Add(this.labelTP);
            this.Controls.Add(this.labelQteor);
            this.Controls.Add(this.textBoxQteor);
            this.Controls.Add(this.btmLoadRealization);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btmLoadRealization;
        private System.Windows.Forms.TextBox textBoxQteor;
        private System.Windows.Forms.Label labelQteor;
        private System.Windows.Forms.Label labelTP;
        private System.Windows.Forms.TextBox textBoxTp;
        private System.Windows.Forms.Button buttonRegularization;
        private System.Windows.Forms.Label labelTeachInterval;
        private System.Windows.Forms.TextBox textBoxTeachInterval;
        private System.Windows.Forms.Label label_T_AKF;
        private System.Windows.Forms.TextBox textBox_T_AKF;
        private System.Windows.Forms.Label label_t1;
        private System.Windows.Forms.TextBox textBoxT1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxOptTeachInt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxQminSKO;
        private System.Windows.Forms.Button buttonSaveResults;
    }
}

