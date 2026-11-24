using System.Linq;
using System.Windows.Forms;
using EFDemoApp.Data;
using Microsoft.EntityFrameworkCore;

namespace EFDemoApp
{
    public class ListViewSimpleForm : Form
    {
        public ListViewSimpleForm()
        {
            Text = "ListView — одна таблица (Service)";
            Size = new System.Drawing.Size(900, 550);
            StartPosition = FormStartPosition.CenterScreen;

            var lv = new ListView
            {
                Dock = DockStyle.Fill,
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                Font = new System.Drawing.Font("Segoe UI", 9)
            };

            lv.Columns.Add("ID", 50);
            lv.Columns.Add("Название", 220);
            lv.Columns.Add("Стоимость", 100);
            lv.Columns.Add("Скидка %", 80);
            lv.Columns.Add("Описание", 400);

            using var db = new AppDbContext();
            var services = db.Services.ToList();

            foreach (var s in services)
            {
                var item = new ListViewItem(s.ID.ToString());
                item.SubItems.Add(s.Title);
                item.SubItems.Add(s.Cost.ToString("N2"));
                item.SubItems.Add((s.Discount * 100).ToString("0") + "%");
                item.SubItems.Add(s.Description ?? "");
                lv.Items.Add(item);
            }

            Controls.Add(lv);
        }
    }

    public class ListViewIncludeForm : Form
    {
        public ListViewIncludeForm()
        {
            Text = "ListView — Include (Client + Gender)";
            Size = new System.Drawing.Size(1000, 550);
            StartPosition = FormStartPosition.CenterScreen;

            var lv = new ListView
            {
                Dock = DockStyle.Fill,
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                Font = new System.Drawing.Font("Segoe UI", 9)
            };

            lv.Columns.Add("ID", 50);
            lv.Columns.Add("Фамилия", 150);
            lv.Columns.Add("Имя", 120);
            lv.Columns.Add("Отчество", 150);
            lv.Columns.Add("Телефон", 130);
            lv.Columns.Add("Email", 200);
            lv.Columns.Add("Пол", 80);

            using var db = new AppDbContext();
            var clients = db.Clients
                .Include(c => c.Gender)
                .ToList();

            foreach (var c in clients)
            {
                var item = new ListViewItem(c.ID.ToString());
                item.SubItems.Add(c.LastName);
                item.SubItems.Add(c.FirstName);
                item.SubItems.Add(c.Patronymic ?? "");
                item.SubItems.Add(c.Phone);
                item.SubItems.Add(c.Email ?? "");
                item.SubItems.Add(c.Gender.Name);
                lv.Items.Add(item);
            }

            Controls.Add(lv);
        }
    }

    public class ListViewThenIncludeForm : Form
    {
        public ListViewThenIncludeForm()
        {
            Text = "ListView — ThenInclude (Service → ClientService → Client)";
            Size = new System.Drawing.Size(1000, 550);
            StartPosition = FormStartPosition.CenterScreen;

            var lv = new ListView
            {
                Dock = DockStyle.Fill,
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                Font = new System.Drawing.Font("Segoe UI", 9)
            };

            lv.Columns.Add("ID", 50);
            lv.Columns.Add("Услуга", 220);
            lv.Columns.Add("Стоимость", 100);
            lv.Columns.Add("Скидка %", 80);
            lv.Columns.Add("Клиенты", 500);

            using var db = new AppDbContext();
            var services = db.Services
                .Include(s => s.ClientServices)
                    .ThenInclude(cs => cs.Client)
                .ToList();

            foreach (var s in services)
            {
                var clientNames = string.Join(", ", s.ClientServices.Select(cs => cs.Client.LastName + " " + cs.Client.FirstName));
                var item = new ListViewItem(s.ID.ToString());
                item.SubItems.Add(s.Title);
                item.SubItems.Add(s.Cost.ToString("N2"));
                item.SubItems.Add((s.Discount * 100).ToString("0") + "%");
                item.SubItems.Add(clientNames);
                lv.Items.Add(item);
            }

            Controls.Add(lv);
        }
    }
}
