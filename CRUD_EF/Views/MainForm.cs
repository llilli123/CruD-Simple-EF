using CRUD_EF.Interfaces;
using CRUD_EF.Models;
using CRUD_EF.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_EF
{
    public partial class MainForm : Form, IRefresh
    {
        public MainForm()
        {
            InitializeComponent();
            Refresh();
        }
       

        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            AddStudent f = new AddStudent();
            f.ShowDialog();
            Refresh();
        }
        public void Refresh()
        {
            using (EscuelaDBEntities db = new EscuelaDBEntities())
            {

                var estudiante = from e in db.Estudiantes
                                 select e;

                dgvStudents.DataSource = estudiante.ToList();

            }
        }

        private void btnEditStudent_Click(object sender, EventArgs e)
        {
            if (dgvStudents.CurrentRow != null)
            {
                int? id = Convert.ToInt32(dgvStudents.CurrentRow.Cells["ID"].Value);
                using (EscuelaDBEntities db = new EscuelaDBEntities())
                {

                    if (id != null)
                    {
                        AddStudent f = new AddStudent(id);
                        f.ShowDialog();
                        Refresh();

                    }
                }
            }
        }

        private void btnDeleteStudent_Click(object sender, EventArgs e)
        {
            if (dgvStudents.CurrentRow != null)
            {
                int id = Convert.ToInt32(dgvStudents.CurrentRow.Cells["ID"].Value);
                using (EscuelaDBEntities db = new EscuelaDBEntities())
                {
                    var estudiante = db.Estudiantes.FirstOrDefault(x => x.ID == id);

                    if (estudiante != null)
                    {
                        // Eliminar de la BD
                        db.Estudiantes.Remove(estudiante);
                        db.SaveChanges();

                        MessageBox.Show("Estudiante eliminado correctamente.");

                        Refresh();
                    }
                    else
                    {
                        MessageBox.Show("No se encontró el estudiante.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione un estudiante de la lista.");
            }
        }
    }
}