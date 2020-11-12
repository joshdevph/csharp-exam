using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System;
using System.Collections.Generic;
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
using System.Data.SQLite;
using System.Globalization;

namespace ExamApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string cs =@"Data Source=C:\Users\jdignadice24\Desktop\Hybrain\ExamApp\bin\Debug\User.db;Version=3;";
        int min_char = 6;
        public MainWindow()
        {
            InitializeComponent();
            fetchData();
        }

        // When Button is Clicked DO THIS:
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string id_num =uid.Text;
            

            if (Int32.TryParse(id_num, out var outParse))
                {
                    int num = Convert.ToInt32(id_num);
                    //VALIDATE TEXTBOX INPUTS
                    if (id_num.Length < min_char){
                        MessageBox.Show("ID Number is too short. Minimum Number is (6) Digit");
                    }else if (id_num.Length > min_char)
                    {
                        MessageBox.Show("ID Number is too long. Maximum Number is (6) Digit ");
                    }else
                    {
                        checkUser(num);
                    }
                }
            else
                {
                   MessageBox.Show("Not a valid ID Number");
                } 
        
        }

        public void checkUser(int idNum){

            //Get Current Date for Login Time
            DateTime date_now = DateTime.Now;

            //Create Connection on the DATABASE from the User.db
            using var con = new SQLiteConnection(cs, true);
            con.Open();
            string id_num =uid.Text;
            int num = Convert.ToInt32(id_num);

            // Query to check if the Typed ID Number is Already Saved
            string stm = "SELECT * FROM Users WHERE id_num= '"+ idNum  + "'";
            using var cmd = new SQLiteCommand(stm, con);
            using SQLiteDataReader rdr = cmd.ExecuteReader();

            //VARIABLE FOR STORING FETCHED DATA FROM THE DATABASE
            int count = 0;
            int id = 0;
            int id_number = 0;

            while(rdr.Read())
            {
                 //ASSIGNED FETCHED DATA IN TO THE VARIABLE
                count++;
                id = rdr.GetInt32(0);
                id_number = rdr.GetInt32(1);

            }
            con.Close();
                           
            if(count == 0)
            { 
                MessageBox.Show("ID Number is not Saved ");
            }
            else
            {   
                //CALL THE SAVE HISTORY AND FETCH DATA FUNCTION IF USER EXIST IN THE DATABASE
                saveHistory(id_number);
                fetchData();
            } 
            
        }

        // SAVE ACTIVITY LOGS OF USER
        public void saveHistory(int idNum){
            DateTime date_now = DateTime.Now;
            using var con = new SQLiteConnection(cs, true);
            con.Open();

            string stm = "SELECT * FROM Users WHERE id_num= '"+ idNum  + "'";
            using var cmd = new SQLiteCommand(stm, con);
            using SQLiteDataReader rdr = cmd.ExecuteReader();


            //VARIABLE FOR STORING FETCHED DATA FROM THE DATABASE
            int count = 0;
            int id = 0;
            int id_number = 0;
            string status = "";

            while(rdr.Read())
            {
                count++;

                //ASSIGNED FETCHED DATA IN TO THE VARIABLE
                id = rdr.GetInt32(0);
                id_number = rdr.GetInt32(1);
                status= rdr.GetString(3).ToString();

            }
            //CLOSE THE CONNECTION
            con.Close();

            //CHECK USER STATUS
            if(status == null){
                using var conn = new SQLiteConnection(cs, true);
                conn.Open();
                using var updtcmd = new SQLiteCommand(conn);

                //INSERT DATA IN ACTIVITY LOGS
                updtcmd.CommandText = "INSERT INTO activity_logs(id_num, date, status) VALUES('"+ idNum + "',datetime('now', 'localtime'), 'IN' )";
                updtcmd.ExecuteNonQuery();
                //UPDATE USER STATUS WHEN HE/SGE LOGGED IN
                updtcmd.CommandText = "UPDATE Users SET status='IN' WHERE id_num='"+ idNum +"'";
                updtcmd.ExecuteNonQuery();
                conn.Close();
                //CHANGE THE BUTTON LABEL IF THE STATUS IS LOGGED IN
                btnLogin.Content = "Logout";
                MessageBox.Show("You're Logged IN");

            }
            else if (status=="IN"){
                
                using var conn = new SQLiteConnection(cs, true);
                conn.Open();
                using var updtcmd = new SQLiteCommand(conn);
                //INSERT DATA IN ACTIVITY LOGS
                updtcmd.CommandText = "INSERT INTO activity_logs(id_num, date, status) VALUES('"+ idNum + "',datetime('now', 'localtime'), 'OUT' )";
                updtcmd.ExecuteNonQuery();
                //UPDATE USER STATUS WHEN HE/SGE LOGGED OUT
                updtcmd.CommandText = "UPDATE Users SET status='OUT' WHERE id_num='"+ idNum +"'";
                updtcmd.ExecuteNonQuery();
                conn.Close();
                //CHANGE THE BUTTON LABEL IF THE STATUS IS LOGGED IN
                btnLogin.Content = "Login";
                MessageBox.Show("You're Logged OUT");

            }else if (status=="OUT"){
               
                using var conn = new SQLiteConnection(cs, true);
                conn.Open();
                using var updtcmd = new SQLiteCommand(conn);
                //INSERT DATA IN ACTIVITY LOGS
                updtcmd.CommandText = "INSERT INTO activity_logs(id_num, date, status) VALUES('"+ idNum + "',datetime('now', 'localtime'), 'IN' )";
                updtcmd.ExecuteNonQuery();
                 //UPDATE USER STATUS WHEN HE/SGE LOGGED IN
                updtcmd.CommandText = "UPDATE Users SET status='IN' WHERE id_num='"+ idNum +"'";
                updtcmd.ExecuteNonQuery();
                conn.Close();
                //CHANGE THE BUTTON LABEL IF THE STATUS IS LOGGED IN
                btnLogin.Content = "Logout";
                MessageBox.Show("You're Logged IN");
            }

            
        }


        //FETCH DATA FOR DATAGRID VIEW
        public void fetchData(){
            
            using var con = new SQLiteConnection(cs, true);
            con.Open();

            string stm = "SELECT id, id_num, date, status FROM activity_logs";
            SQLiteCommand cmd = new SQLiteCommand(stm, con);
            cmd.ExecuteNonQuery();

            SQLiteDataAdapter adptr =  new SQLiteDataAdapter(cmd);  
            DataTable data_tb = new DataTable("activity_logs");  
            adptr.Fill(data_tb);

            //INSERT DATA INTO THE DATAGRID
            dataGrid.ItemsSource= data_tb.DefaultView;
            
            con.Close();

        }
    
    }
}
