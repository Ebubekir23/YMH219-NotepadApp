namespace NotepadApp
{
    partial class AnaForm
    {
        private System.ComponentModel.IContainer components = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            menu = new MenuStrip();
            mnuDosya = new ToolStripMenuItem();
            mnuYeni = new ToolStripMenuItem();
            mnuKaydet = new ToolStripMenuItem();
            mnuDisaAktar = new ToolStripMenuItem();
            mnuAyirici1 = new ToolStripSeparator();
            mnuYenidenYukle = new ToolStripMenuItem();
            mnuKonumuAc = new ToolStripMenuItem();
            mnuAyirici2 = new ToolStripSeparator();
            mnuCikis = new ToolStripMenuItem();
            durumCubugu = new StatusStrip();
            lblNotSayisi = new ToolStripStatusLabel();
            lblKayitDurumu = new ToolStripStatusLabel();
            lblDosya = new ToolStripStatusLabel();
            lblAra = new Label();
            txtAra = new TextBox();
            lblFiltre = new Label();
            cmbFiltre = new ComboBox();
            lstNotlar = new ListBox();
            lblBaslik = new Label();
            txtBaslik = new TextBox();
            lblKategori = new Label();
            cmbKategori = new ComboBox();
            lblTarihler = new Label();
            rtbIcerik = new RichTextBox();
            btnYeni = new Button();
            btnKaydet = new Button();
            btnSil = new Button();
            btnDisaAktar = new Button();
            menu.SuspendLayout();
            durumCubugu.SuspendLayout();
            SuspendLayout();
            //
            // menu
            //
            menu.Items.AddRange(new ToolStripItem[] { mnuDosya });
            menu.Location = new Point(0, 0);
            menu.Name = "menu";
            menu.Size = new Size(980, 28);
            //
            // mnuDosya
            //
            mnuDosya.DropDownItems.AddRange(new ToolStripItem[] { mnuYeni, mnuKaydet, mnuDisaAktar, mnuAyirici1, mnuYenidenYukle, mnuKonumuAc, mnuAyirici2, mnuCikis });
            mnuDosya.Name = "mnuDosya";
            mnuDosya.Text = "&Dosya";
            //
            // mnuYeni
            //
            mnuYeni.Name = "mnuYeni";
            mnuYeni.ShortcutKeys = Keys.Control | Keys.N;
            mnuYeni.Text = "Yeni Not";
            mnuYeni.Click += btnYeni_Click;
            //
            // mnuKaydet
            //
            mnuKaydet.Name = "mnuKaydet";
            mnuKaydet.ShortcutKeys = Keys.Control | Keys.S;
            mnuKaydet.Text = "Notu Kaydet";
            mnuKaydet.Click += btnKaydet_Click;
            //
            // mnuDisaAktar
            //
            mnuDisaAktar.Name = "mnuDisaAktar";
            mnuDisaAktar.ShortcutKeys = Keys.Control | Keys.E;
            mnuDisaAktar.Text = "Notu .txt Olarak Dışa Aktar...";
            mnuDisaAktar.Click += btnDisaAktar_Click;
            //
            // mnuYenidenYukle
            //
            mnuYenidenYukle.Name = "mnuYenidenYukle";
            mnuYenidenYukle.Text = "Notları Dosyadan Yeniden Yükle";
            mnuYenidenYukle.Click += mnuYenidenYukle_Click;
            //
            // mnuKonumuAc
            //
            mnuKonumuAc.Name = "mnuKonumuAc";
            mnuKonumuAc.Text = "Not Dosyasının Konumunu Aç";
            mnuKonumuAc.Click += mnuKonumuAc_Click;
            //
            // mnuCikis
            //
            mnuCikis.Name = "mnuCikis";
            mnuCikis.Text = "Çıkış";
            mnuCikis.Click += mnuCikis_Click;
            //
            // durumCubugu
            //
            durumCubugu.Items.AddRange(new ToolStripItem[] { lblNotSayisi, lblKayitDurumu, lblDosya });
            durumCubugu.Location = new Point(0, 594);
            durumCubugu.Name = "durumCubugu";
            durumCubugu.Size = new Size(980, 26);
            //
            // lblNotSayisi
            //
            lblNotSayisi.Name = "lblNotSayisi";
            lblNotSayisi.BorderSides = ToolStripStatusLabelBorderSides.Right;
            //
            // lblKayitDurumu
            //
            lblKayitDurumu.Name = "lblKayitDurumu";
            lblKayitDurumu.BorderSides = ToolStripStatusLabelBorderSides.Right;
            //
            // lblDosya
            //
            lblDosya.Name = "lblDosya";
            lblDosya.ForeColor = Color.DimGray;
            lblDosya.Spring = true;
            lblDosya.TextAlign = ContentAlignment.MiddleLeft;
            //
            // lblAra
            //
            lblAra.AutoSize = true;
            lblAra.Location = new Point(12, 42);
            lblAra.Name = "lblAra";
            lblAra.Text = "Ara:";
            //
            // txtAra
            //
            txtAra.Location = new Point(70, 38);
            txtAra.Name = "txtAra";
            txtAra.PlaceholderText = "başlık, içerik veya kategori";
            txtAra.Size = new Size(270, 27);
            txtAra.TabIndex = 0;
            txtAra.TextChanged += Filtre_Degisti;
            //
            // lblFiltre
            //
            lblFiltre.AutoSize = true;
            lblFiltre.Location = new Point(12, 76);
            lblFiltre.Name = "lblFiltre";
            lblFiltre.Text = "Kategori:";
            //
            // cmbFiltre
            //
            cmbFiltre.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFiltre.Location = new Point(84, 72);
            cmbFiltre.Name = "cmbFiltre";
            cmbFiltre.Size = new Size(256, 28);
            cmbFiltre.TabIndex = 1;
            cmbFiltre.SelectedIndexChanged += Filtre_Degisti;
            //
            // lstNotlar
            //
            lstNotlar.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            lstNotlar.HorizontalScrollbar = true;
            lstNotlar.IntegralHeight = false;
            lstNotlar.Location = new Point(12, 108);
            lstNotlar.Name = "lstNotlar";
            lstNotlar.Size = new Size(328, 478);
            lstNotlar.TabIndex = 2;
            lstNotlar.SelectedIndexChanged += lstNotlar_SelectedIndexChanged;
            //
            // lblBaslik
            //
            lblBaslik.AutoSize = true;
            lblBaslik.Location = new Point(356, 42);
            lblBaslik.Name = "lblBaslik";
            lblBaslik.Text = "Başlık:";
            //
            // txtBaslik
            //
            txtBaslik.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtBaslik.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            txtBaslik.Location = new Point(430, 38);
            txtBaslik.MaxLength = 100;
            txtBaslik.Name = "txtBaslik";
            txtBaslik.Size = new Size(538, 31);
            txtBaslik.TabIndex = 3;
            txtBaslik.TextChanged += Duzenleyici_Degisti;
            //
            // lblKategori
            //
            lblKategori.AutoSize = true;
            lblKategori.Location = new Point(356, 80);
            lblKategori.Name = "lblKategori";
            lblKategori.Text = "Kategori:";
            //
            // cmbKategori
            //
            cmbKategori.Location = new Point(430, 76);
            cmbKategori.MaxLength = 30;
            cmbKategori.Name = "cmbKategori";
            cmbKategori.Size = new Size(200, 28);
            cmbKategori.TabIndex = 4;
            cmbKategori.TextChanged += Duzenleyici_Degisti;
            //
            // lblTarihler
            //
            lblTarihler.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblTarihler.ForeColor = Color.DimGray;
            lblTarihler.Location = new Point(356, 110);
            lblTarihler.Name = "lblTarihler";
            lblTarihler.Size = new Size(612, 24);
            lblTarihler.TextAlign = ContentAlignment.MiddleLeft;
            //
            // rtbIcerik
            //
            rtbIcerik.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rtbIcerik.Font = new Font("Segoe UI", 11F);
            rtbIcerik.Location = new Point(356, 138);
            rtbIcerik.Name = "rtbIcerik";
            rtbIcerik.Size = new Size(612, 402);
            rtbIcerik.TabIndex = 5;
            rtbIcerik.Text = "";
            rtbIcerik.TextChanged += Duzenleyici_Degisti;
            //
            // btnYeni
            //
            btnYeni.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnYeni.Location = new Point(356, 548);
            btnYeni.Name = "btnYeni";
            btnYeni.Size = new Size(120, 38);
            btnYeni.TabIndex = 6;
            btnYeni.Text = "Yeni Not";
            btnYeni.UseVisualStyleBackColor = true;
            btnYeni.Click += btnYeni_Click;
            //
            // btnKaydet
            //
            btnKaydet.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnKaydet.Location = new Point(482, 548);
            btnKaydet.Name = "btnKaydet";
            btnKaydet.Size = new Size(120, 38);
            btnKaydet.TabIndex = 7;
            btnKaydet.Text = "Kaydet";
            btnKaydet.UseVisualStyleBackColor = true;
            btnKaydet.Click += btnKaydet_Click;
            //
            // btnSil
            //
            btnSil.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSil.Location = new Point(608, 548);
            btnSil.Name = "btnSil";
            btnSil.Size = new Size(120, 38);
            btnSil.TabIndex = 8;
            btnSil.Text = "Sil";
            btnSil.UseVisualStyleBackColor = true;
            btnSil.Click += btnSil_Click;
            //
            // btnDisaAktar
            //
            btnDisaAktar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnDisaAktar.Location = new Point(734, 548);
            btnDisaAktar.Name = "btnDisaAktar";
            btnDisaAktar.Size = new Size(160, 38);
            btnDisaAktar.TabIndex = 9;
            btnDisaAktar.Text = ".txt Dışa Aktar";
            btnDisaAktar.UseVisualStyleBackColor = true;
            btnDisaAktar.Click += btnDisaAktar_Click;
            //
            // AnaForm
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(980, 620);
            Controls.Add(btnDisaAktar);
            Controls.Add(btnSil);
            Controls.Add(btnKaydet);
            Controls.Add(btnYeni);
            Controls.Add(rtbIcerik);
            Controls.Add(lblTarihler);
            Controls.Add(cmbKategori);
            Controls.Add(lblKategori);
            Controls.Add(txtBaslik);
            Controls.Add(lblBaslik);
            Controls.Add(lstNotlar);
            Controls.Add(cmbFiltre);
            Controls.Add(lblFiltre);
            Controls.Add(txtAra);
            Controls.Add(lblAra);
            Controls.Add(durumCubugu);
            Controls.Add(menu);
            MainMenuStrip = menu;
            MinimumSize = new Size(900, 520);
            Name = "AnaForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Not Defteri";
            FormClosing += AnaForm_FormClosing;
            Load += AnaForm_Load;
            menu.ResumeLayout(false);
            menu.PerformLayout();
            durumCubugu.ResumeLayout(false);
            durumCubugu.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private MenuStrip menu = null!;
        private ToolStripMenuItem mnuDosya = null!;
        private ToolStripMenuItem mnuYeni = null!;
        private ToolStripMenuItem mnuKaydet = null!;
        private ToolStripMenuItem mnuDisaAktar = null!;
        private ToolStripSeparator mnuAyirici1 = null!;
        private ToolStripMenuItem mnuYenidenYukle = null!;
        private ToolStripMenuItem mnuKonumuAc = null!;
        private ToolStripSeparator mnuAyirici2 = null!;
        private ToolStripMenuItem mnuCikis = null!;
        private StatusStrip durumCubugu = null!;
        private ToolStripStatusLabel lblNotSayisi = null!;
        private ToolStripStatusLabel lblKayitDurumu = null!;
        private ToolStripStatusLabel lblDosya = null!;
        private Label lblAra = null!;
        private TextBox txtAra = null!;
        private Label lblFiltre = null!;
        private ComboBox cmbFiltre = null!;
        private ListBox lstNotlar = null!;
        private Label lblBaslik = null!;
        private TextBox txtBaslik = null!;
        private Label lblKategori = null!;
        private ComboBox cmbKategori = null!;
        private Label lblTarihler = null!;
        private RichTextBox rtbIcerik = null!;
        private Button btnYeni = null!;
        private Button btnKaydet = null!;
        private Button btnSil = null!;
        private Button btnDisaAktar = null!;
    }
}
