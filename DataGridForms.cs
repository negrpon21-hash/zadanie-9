using System.Linq;
using System.Windows.Forms;
using EFDemoApp.Data;
using Microsoft.EntityFrameworkCore;

namespace EFDemoApp
{
    public class DataGridSimpleForm : Form
    {
        public DataGridSimpleForm()
        {
            Text = "DataGrid — одна таблица (Service)";
            Size = new System.Drawing.Size(900, 500);
            StartPosition = FormStartPosition.CenterScreen;

            var grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false
            };

            using var db = new AppDbContext();
            var data = db.Services
                .Select(s => new {
                    s.ID,
                    s.Title,
                    Стоимость = s.Cost,
                    Скидка = s.Discount,
                    s.Description
                })
                .ToList();

            grid.DataSource = data;
            Controls.Add(grid);
        }
    }

    public class DataGridIncludeForm : Form
    {
        public DataGridIncludeForm()
        {
            Text = "DataGrid — Include (Client + Gender)";
            Size = new System.Drawing.Size(1000, 500);
            StartPosition = FormStartPosition.CenterScreen;

            var grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false
            };

            using var db = new AppDbContext();
            var data = db.Clients
                .Include(c => c.Gender)
                .Select(c => new {
                    c.ID,
                    Фамилия = c.LastName,
                    Имя = c.FirstName,
                    Отчество = c.Patronymic,
                    c.Phone,
                    c.Email,
                    Пол = c.Gender.Name
                })
                .ToList();

            grid.DataSource = data;
            Controls.Add(grid);
        }
    }

    public class DataGridThenIncludeForm : Form
    {
        public DataGridThenIncludeForm()
        {
            Text = "DataGrid — ThenInclude (Service → ClientService → Client)";
            Size = new System.Drawing.Size(1000, 500);
            StartPosition = FormStartPosition.CenterScreen;

            var grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false
            };

            using var db = new AppDbContext();
            var data = db.Services
                .Include(s => s.ClientServices)
                    .ThenInclude(cs => cs.Client)
                .ToList()
                .Select(s => new {
                    s.ID,
                    Услуга = s.Title,
                    Стоимость = s.Cost,
                    Клиенты = string.Join(", ", s.ClientServices.Select(cs => cs.Client.LastName + " " + cs.Client.FirstName))
                })
                .ToList();

            grid.DataSource = data;
            Controls.Add(grid);
        }
    }
}
