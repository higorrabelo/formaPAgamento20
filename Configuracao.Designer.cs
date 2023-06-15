namespace Solicitacao {
    partial class Configuracao {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Configuracao));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txIP = new System.Windows.Forms.TextBox();
            this.txLoja = new System.Windows.Forms.TextBox();
            this.txTerminal = new System.Windows.Forms.TextBox();
            this.txCliente = new System.Windows.Forms.TextBox();
            this.txAuto = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 37);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP Servidor";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 66);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "ID Loja";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 97);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "ID Terminal";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 127);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "CNPJ Cliente";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 155);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "CNPJ Automação";
            // 
            // txIP
            // 
            this.txIP.Location = new System.Drawing.Point(112, 36);
            this.txIP.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txIP.Name = "txIP";
            this.txIP.Size = new System.Drawing.Size(118, 20);
            this.txIP.TabIndex = 5;
            // 
            // txLoja
            // 
            this.txLoja.Location = new System.Drawing.Point(112, 64);
            this.txLoja.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txLoja.Name = "txLoja";
            this.txLoja.Size = new System.Drawing.Size(118, 20);
            this.txLoja.TabIndex = 6;
            // 
            // txTerminal
            // 
            this.txTerminal.Location = new System.Drawing.Point(112, 94);
            this.txTerminal.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txTerminal.Name = "txTerminal";
            this.txTerminal.Size = new System.Drawing.Size(118, 20);
            this.txTerminal.TabIndex = 7;
            // 
            // txCliente
            // 
            this.txCliente.Location = new System.Drawing.Point(112, 124);
            this.txCliente.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txCliente.Name = "txCliente";
            this.txCliente.Size = new System.Drawing.Size(118, 20);
            this.txCliente.TabIndex = 8;
            // 
            // txAuto
            // 
            this.txAuto.Location = new System.Drawing.Point(112, 155);
            this.txAuto.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txAuto.Name = "txAuto";
            this.txAuto.Size = new System.Drawing.Size(118, 20);
            this.txAuto.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(133, 197);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 22);
            this.button1.TabIndex = 10;
            this.button1.Text = "Salvar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Configuracao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 258);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txAuto);
            this.Controls.Add(this.txCliente);
            this.Controls.Add(this.txTerminal);
            this.Controls.Add(this.txLoja);
            this.Controls.Add(this.txIP);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Configuracao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuração";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txIP;
        private System.Windows.Forms.TextBox txLoja;
        private System.Windows.Forms.TextBox txTerminal;
        private System.Windows.Forms.TextBox txCliente;
        private System.Windows.Forms.TextBox txAuto;
        private System.Windows.Forms.Button button1;
    }
}