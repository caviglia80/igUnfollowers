namespace UnFollowers
{
	partial class Panel
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
			btnGetList = new Button();
			l1 = new Label();
			l2 = new Label();
			btnIniciarSesion = new Button();
			txtUser = new TextBox();
			btnUnfollow = new Button();
			lvSeguidos = new ListView();
			Seguidos = new ColumnHeader();
			txtBuscarSeguidos = new TextBox();
			txtBuscarLB = new TextBox();
			lvWhiteList = new ListView();
			columnHeader1 = new ColumnHeader();
			lSeguidos = new Label();
			lWB = new Label();
			btnUnfollowed = new Button();
			cbProcesosVisibles = new CheckBox();
			cbComenzarAuto = new CheckBox();
			cbSeguro = new CheckBox();
			gb1 = new GroupBox();
			cbVerHistorias = new CheckBox();
			gb1.SuspendLayout();
			SuspendLayout();
			// 
			// btnGetList
			// 
			btnGetList.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
			btnGetList.Location = new Point(5, 33);
			btnGetList.Name = "btnGetList";
			btnGetList.Size = new Size(141, 23);
			btnGetList.TabIndex = 1;
			btnGetList.Text = "Obtener Seguidos";
			btnGetList.UseVisualStyleBackColor = true;
			btnGetList.Click += btnList_Click;
			// 
			// l1
			// 
			l1.AutoSize = true;
			l1.Location = new Point(5, 1);
			l1.Name = "l1";
			l1.Size = new Size(13, 15);
			l1.TabIndex = 3;
			l1.Text = "0";
			// 
			// l2
			// 
			l2.AutoSize = true;
			l2.Location = new Point(47, 1);
			l2.Name = "l2";
			l2.Size = new Size(13, 15);
			l2.TabIndex = 4;
			l2.Text = "0";
			// 
			// btnIniciarSesion
			// 
			btnIniciarSesion.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			btnIniciarSesion.Location = new Point(664, 33);
			btnIniciarSesion.Name = "btnIniciarSesion";
			btnIniciarSesion.Size = new Size(130, 23);
			btnIniciarSesion.TabIndex = 7;
			btnIniciarSesion.Text = "Iniciar Sesion";
			btnIniciarSesion.UseVisualStyleBackColor = true;
			btnIniciarSesion.Click += btnIniciarSesion_Click;
			// 
			// txtUser
			// 
			txtUser.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			txtUser.Location = new Point(664, 4);
			txtUser.Name = "txtUser";
			txtUser.Size = new Size(130, 23);
			txtUser.TabIndex = 8;
			txtUser.Text = "fraancaviglia";
			txtUser.TextAlign = HorizontalAlignment.Center;
			// 
			// btnUnfollow
			// 
			btnUnfollow.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
			btnUnfollow.Location = new Point(5, 62);
			btnUnfollow.Name = "btnUnfollow";
			btnUnfollow.Size = new Size(141, 23);
			btnUnfollow.TabIndex = 10;
			btnUnfollow.Text = "Start Unfollow";
			btnUnfollow.UseVisualStyleBackColor = true;
			btnUnfollow.Click += btnUnfollow_Click;
			// 
			// lvSeguidos
			// 
			lvSeguidos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
			lvSeguidos.Columns.AddRange(new ColumnHeader[] { Seguidos });
			lvSeguidos.FullRowSelect = true;
			lvSeguidos.HeaderStyle = ColumnHeaderStyle.None;
			lvSeguidos.Location = new Point(152, 33);
			lvSeguidos.Name = "lvSeguidos";
			lvSeguidos.Size = new Size(250, 189);
			lvSeguidos.TabIndex = 11;
			lvSeguidos.UseCompatibleStateImageBehavior = false;
			lvSeguidos.View = View.Details;
			lvSeguidos.DoubleClick += lvSeguidos_DoubleClick;
			// 
			// Seguidos
			// 
			Seguidos.Text = "Seguidos";
			Seguidos.Width = 200;
			// 
			// txtBuscarSeguidos
			// 
			txtBuscarSeguidos.Location = new Point(152, 4);
			txtBuscarSeguidos.Name = "txtBuscarSeguidos";
			txtBuscarSeguidos.Size = new Size(250, 23);
			txtBuscarSeguidos.TabIndex = 12;
			txtBuscarSeguidos.TextChanged += txtBuscar_TextChanged;
			// 
			// txtBuscarLB
			// 
			txtBuscarLB.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			txtBuscarLB.Location = new Point(408, 4);
			txtBuscarLB.Name = "txtBuscarLB";
			txtBuscarLB.Size = new Size(250, 23);
			txtBuscarLB.TabIndex = 14;
			txtBuscarLB.TextChanged += txtBuscarLB_TextChanged;
			// 
			// lvWhiteList
			// 
			lvWhiteList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
			lvWhiteList.Columns.AddRange(new ColumnHeader[] { columnHeader1 });
			lvWhiteList.FullRowSelect = true;
			lvWhiteList.HeaderStyle = ColumnHeaderStyle.None;
			lvWhiteList.Location = new Point(408, 33);
			lvWhiteList.Name = "lvWhiteList";
			lvWhiteList.Size = new Size(250, 189);
			lvWhiteList.TabIndex = 13;
			lvWhiteList.UseCompatibleStateImageBehavior = false;
			lvWhiteList.View = View.Details;
			lvWhiteList.DoubleClick += lvWhiteList_DoubleClick;
			// 
			// columnHeader1
			// 
			columnHeader1.Text = "Seguidos";
			columnHeader1.Width = 200;
			// 
			// lSeguidos
			// 
			lSeguidos.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			lSeguidos.AutoSize = true;
			lSeguidos.Location = new Point(152, 225);
			lSeguidos.Name = "lSeguidos";
			lSeguidos.Size = new Size(55, 15);
			lSeguidos.TabIndex = 15;
			lSeguidos.Text = "Seguidos";
			// 
			// lWB
			// 
			lWB.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			lWB.AutoSize = true;
			lWB.Location = new Point(408, 225);
			lWB.Name = "lWB";
			lWB.Size = new Size(69, 15);
			lWB.TabIndex = 16;
			lWB.Text = "Lista Blanca";
			// 
			// btnUnfollowed
			// 
			btnUnfollowed.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
			btnUnfollowed.Location = new Point(5, 91);
			btnUnfollowed.Name = "btnUnfollowed";
			btnUnfollowed.Size = new Size(141, 23);
			btnUnfollowed.TabIndex = 17;
			btnUnfollowed.Text = "Unfollowed";
			btnUnfollowed.UseVisualStyleBackColor = true;
			btnUnfollowed.Click += btnUnfollowed_Click;
			// 
			// cbProcesosVisibles
			// 
			cbProcesosVisibles.AutoSize = true;
			cbProcesosVisibles.Location = new Point(6, 22);
			cbProcesosVisibles.Name = "cbProcesosVisibles";
			cbProcesosVisibles.Size = new Size(114, 19);
			cbProcesosVisibles.TabIndex = 18;
			cbProcesosVisibles.Text = "Procesos visibles";
			cbProcesosVisibles.UseVisualStyleBackColor = true;
			// 
			// cbComenzarAuto
			// 
			cbComenzarAuto.AutoSize = true;
			cbComenzarAuto.Checked = true;
			cbComenzarAuto.CheckState = CheckState.Checked;
			cbComenzarAuto.Location = new Point(6, 45);
			cbComenzarAuto.Name = "cbComenzarAuto";
			cbComenzarAuto.Size = new Size(107, 19);
			cbComenzarAuto.TabIndex = 19;
			cbComenzarAuto.Text = "Comienzo auto";
			cbComenzarAuto.UseVisualStyleBackColor = true;
			// 
			// cbSeguro
			// 
			cbSeguro.AutoSize = true;
			cbSeguro.Checked = true;
			cbSeguro.CheckState = CheckState.Checked;
			cbSeguro.Location = new Point(6, 68);
			cbSeguro.Name = "cbSeguro";
			cbSeguro.Size = new Size(101, 19);
			cbSeguro.TabIndex = 20;
			cbSeguro.Text = "Seguro (lento)";
			cbSeguro.UseVisualStyleBackColor = true;
			cbSeguro.CheckedChanged += cbSeguro_CheckedChanged;
			// 
			// gb1
			// 
			gb1.Controls.Add(cbVerHistorias);
			gb1.Controls.Add(cbProcesosVisibles);
			gb1.Controls.Add(cbSeguro);
			gb1.Controls.Add(cbComenzarAuto);
			gb1.Location = new Point(12, 120);
			gb1.Name = "gb1";
			gb1.Size = new Size(134, 120);
			gb1.TabIndex = 21;
			gb1.TabStop = false;
			// 
			// cbVerHistorias
			// 
			cbVerHistorias.AutoSize = true;
			cbVerHistorias.Checked = true;
			cbVerHistorias.CheckState = CheckState.Checked;
			cbVerHistorias.Location = new Point(6, 91);
			cbVerHistorias.Name = "cbVerHistorias";
			cbVerHistorias.Size = new Size(89, 19);
			cbVerHistorias.TabIndex = 21;
			cbVerHistorias.Text = "Ver historias";
			cbVerHistorias.UseVisualStyleBackColor = true;
			// 
			// Panel
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(798, 244);
			Controls.Add(gb1);
			Controls.Add(btnUnfollowed);
			Controls.Add(btnUnfollow);
			Controls.Add(lvSeguidos);
			Controls.Add(lWB);
			Controls.Add(lSeguidos);
			Controls.Add(txtBuscarLB);
			Controls.Add(lvWhiteList);
			Controls.Add(txtBuscarSeguidos);
			Controls.Add(txtUser);
			Controls.Add(btnIniciarSesion);
			Controls.Add(l2);
			Controls.Add(l1);
			Controls.Add(btnGetList);
			Name = "Panel";
			SizeGripStyle = SizeGripStyle.Hide;
			StartPosition = FormStartPosition.CenterScreen;
			Text = "UnFollowers";
			TopMost = true;
			Load += Panel_Load;
			gb1.ResumeLayout(false);
			gb1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion
		private Button btnGetList;
		private Label l1;
		private Label l2;
		private Button btnIniciarSesion;
		private TextBox txtUser;
		private Button btnUnfollow;
		private ListView lvSeguidos;
		private TextBox txtBuscarSeguidos;
		private ColumnHeader Seguidos;
		private TextBox txtBuscarLB;
		private ListView lvWhiteList;
		private ColumnHeader columnHeader1;
		private Label lSeguidos;
		private Label lWB;
		private Button btnUnfollowed;
		private CheckBox cbProcesosVisibles;
		private CheckBox cbComenzarAuto;
		private CheckBox cbSeguro;
		private GroupBox gb1;
		private CheckBox cbVerHistorias;
	}
}