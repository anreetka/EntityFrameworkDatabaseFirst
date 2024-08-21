using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EFDatabaseFirst
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SchoolDBEntities db;
        public MainWindow()
        {
            InitializeComponent();
            db = new SchoolDBEntities();
            StandardLoad();
        }

        void StandardLoad()
        {
            cmbStandard.ItemsSource = db.Standards.ToList();

            cmbStandard.DisplayMemberPath="StandardName"; //display
            cmbStandard.SelectedValuePath = "StandardId"; //value
        }

        private void btnLoadData_Click(object sender, RoutedEventArgs e)
        {
            grdStudents.ItemsSource = db.Students.ToList();
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            Student st1 = new Student(); //new student object as we are dealing with objects

            st1.StudentName = txtName.Text;
            st1.StandardId = (int) cmbStandard.SelectedValue;   //combo box will always provide an object that we need to parse it in int

            db.Students.Add(st1);  // add object to student dbSet
            db.SaveChanges();  //update changes in the actual database

            grdStudents.ItemsSource = db.Students.ToList();

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(txtId.Text);

            Student st=db.Students.Find(id); //find student by id

            if(st != null)
            {
                db.Students.Remove(st);   //remove students from the table
                db.SaveChanges();
            }
            else
            {
                MessageBox.Show("Id doesn't exist");
            }


            grdStudents.ItemsSource = db.Students.ToList();
        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(txtId.Text);

            Student st = db.Students.Find(id); //find student by id

            if(st!= null)
            {
                txtName.Text = st.StudentName;
                cmbStandard.SelectedValue = st.StandardId;
            }
            else
            {
                MessageBox.Show("Id doesn't exist");
            }

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text;

            var result = from s in db.Students
                        where s.StudentName.Equals(name)
                        select s;

            grdStudents.ItemsSource= result.ToList();
            
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            //int id = int.Parse(txtId.Text);
            //Student st = db.Students.Find(id);

            //if (st != null)
            //{
            //    txtName.Text = st.StudentName;
            //    cmbStandard.SelectedValue = st.StandardId;
            //}
            //else
            //{
            //    MessageBox.Show("Id doesn't exist");
            //}

            //st.StudentName = txtName.Text;
            //st.StandardId = (int) cmbStandard.SelectedValue;

            //db.Students.AddOrUpdate(st);

            //db.SaveChanges();
            

        }
    }
}
