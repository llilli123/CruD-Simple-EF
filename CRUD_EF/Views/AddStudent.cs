using CRUD_EF.Interfaces;
using CRUD_EF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_EF.Views
{
    public partial class AddStudent : Form
    {
        public int? id;
        Estudiante estudiante = null;

        public AddStudent(int? id= null)
        {
            InitializeComponent();

            this.id = id;
            ChargeData();
        }
        
        private void ChargeData()
        {
            using (EscuelaDBEntities db = new EscuelaDBEntities())
            {
                if (id != null)
                {
                    estudiante = db.Estudiantes.Find(id);
                    txtNombre.Text = estudiante.Nombre;
                    txtNota.Text = estudiante.Nota.ToString();
                }
            }


        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            using (EscuelaDBEntities db = new EscuelaDBEntities())
            {
                if (id == null)
                { 
                    estudiante = new Estudiante(); 
                }

                    estudiante.Nombre = txtNombre.Text;
                    float nota;
                    if (float.TryParse(txtNota.Text, out nota))
                    {
                    estudiante.Nota = nota;
                    }

                if (id == null)
                {
                    db.Estudiantes.Add(estudiante);
                }
                else
                { 
                db.Entry(estudiante).State = System.Data.Entity.EntityState.Modified;
                }

                db.SaveChanges();
                

            }
        }
    }
}
