using System;
using System.Windows.Forms;

namespace EFDemoApp
{
    public class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            Text = "EF Core — Демонстрация";
            Size = new System.Drawing.Size(500, 420);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = System.Drawing.Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            var title = new Label
            {
                Text = "Выберите режим отображения данных",
                Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold),
                Location = new System.Drawing.Point(30, 20),
                AutoSize = true
            };

            var dgLabel = new Label
            {
                Text = "DataGrid:",
                Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold),
                Location = new System.Drawing.Point(30, 65),
                AutoSize = true,
                ForeColor = System.Drawing.Color.FromArgb(0, 100, 180)
            };

            var btn1 = MakeButton("1. DataGrid — одна таблица (Service)", 95, () => new DataGridSimpleForm().Show());
            var btn2 = MakeButton("2. DataGrid — Include (Client + Gender)", 145, () => new DataGridIncludeForm().Show());
            var btn3 = MakeButton("3. DataGrid — ThenInclude (Service → Clients)", 195, () => new DataGridThenIncludeForm().Show());

            var lvLabel = new Label
            {
                Text = "ListView:",
                Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold),
                Location = new System.Drawing.Point(30, 245),
                AutoSize = true,
                ForeColor = System.Drawing.Color.FromArgb(0, 140, 70)
            };

            var btn4 = MakeButton("4. ListView — одна таблица (Service)", 275, () => new ListViewSimpleForm().Show());
            var btn5 = MakeButton("5. ListView — Include (Client + Gender)", 325, () => new ListViewIncludeForm().Show());
            var btn6 = MakeButton("6. ListView — ThenInclude (Service → Clients)", 375, () => new ListViewThenIncludeForm().Show());

            Controls.AddRange(new Control[] { title, dgLabel, btn1, btn2, btn3, lvLabel, btn4, btn5, btn6 });
        }

        private Button MakeButton(string text, int y, Action onClick)
        {
            var btn = new Button
            {
                Text = text,
                Location = new System.Drawing.Point(30, y),
                Size = new System.Drawing.Size(420, 40),
                Font = new System.Drawing.Font("Segoe UI", 10),
                FlatStyle = FlatStyle.Flat,
                BackColor = System.Drawing.Color.FromArgb(245, 248, 255),
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0)
            };
            btn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(200, 210, 230);
            btn.Click += (s, e) => { try { onClick(); } catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); } };
            return btn;
        }
    }
}
