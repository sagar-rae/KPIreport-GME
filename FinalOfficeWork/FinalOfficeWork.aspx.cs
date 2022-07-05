using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Text;

namespace FinalOfficeWork
{
    public partial class FinalOfficeWork : System.Web.UI.Page
    {
        StringBuilder table = new StringBuilder();

        string strcon = ConfigurationManager.ConnectionStrings["HTMLDB"].ConnectionString;
        void Load_data()
        {

            SqlConnection con = new SqlConnection(strcon);
            con.Open();




            DateTime StartDate = Convert.ToDateTime(txtBoxTwo.Text);
            DateTime EndDate = Convert.ToDateTime(txtBoxThree.Text);
            if (drpList.SelectedValue == "0")
            {
                int[] droplistNumbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
                string[] dropListNames = { "random", "Mobile Remit", "Songu-ri", "Hyehwa", "DDM CIS", "Mongol Town", "Gwangju", "Suwon", "GME Online", "Dongdaemun", "Ansan", "Hwaseong", "Gimhae" };

                foreach (int i in droplistNumbers)
                {
                    foreach (DateTime day in EachCalendarDay(StartDate, EndDate))
                    {

                        placeHolderId.Controls.Clear();

                        SqlCommand comm = new SqlCommand("HTMLTableSP", con);
                        comm.CommandType = CommandType.StoredProcedure;
                        comm.Parameters.AddWithValue("@Flag", "Show");
                        comm.Parameters.AddWithValue("@TableValue", i);
                        comm.Parameters.AddWithValue("@DateSelected", string.Format("{0:yyyy/MM/dd}", day.ToString()));
                        // comm.Parameters.AddWithValue("@DateSelectedLast", string.Format("{0:yyyy/MM/dd}", txtBoxThree.Text));
                        comm.ExecuteNonQuery();


                        SqlDataReader rd = comm.ExecuteReader();

                        int idForTable = 1;
                        table.Append("<div class='details' style='float:left'><label>Branch KPI Reports</label><br><label>From Date: '" + day.ToString() + "'</label> <label> </label> <label>To Date: '" + day.ToString() + "'</label><br><label>Branch: '" + dropListNames[i] + "'</label></div>");
                        table.Append("<div style='position: relative;'><button class='exportID' style='background-color:transparent; border-color:transparent; float:right; margin-right:35px; position:relative; top:50px;'><img src='/excel logo/excel logo.png' height='20'/></button></div>");
                        table.Append("<table border='1' class='table table-hover tableId' style='margin-left:20px; width:95%'>");
                        table.Append("<tr id='HeadingId'><th class='hiddenHeader'>HiddenId</th><th class='hiddenHeader'>BranchId</th><th class='hiddenHeader'>HiddenDate</th><th>Sn</th><th>Username</th><th>Name</th><th>Nationality</th><th>Registration</th><th>GMELoan</th><th>SimCard</th><th>GMEPass</th><th>IssueSolved</th><th>Other(Share, Bond, Tax Refund)</th><th>Staff Efficiency</th><th>Branch Efficiency</th><th>Edit</th></tr>");

                        if (rd.HasRows)
                        {
                            while (rd.Read())
                            {
                                table.Append("<tr>");
                                table.Append("<td class='hiddenColumnId' id='hiddenColumnId'>" + rd[0] + "</td>");
                                table.Append("<td class='hiddenColumnId' >" + rd[1] + "</td>");
                                table.Append("<td class='hiddenColumnId'>" + rd[2] + "</td>");
                                table.Append("<td id='rowId'>" + idForTable + "</td>");
                                table.Append("<td>" + rd[3] + "</td>");
                                table.Append("<td>" + rd[4] + "</td>");
                                table.Append("<td>" + rd[5] + "</td>");
                                table.Append("<td><div class='row_data' id='RegId' col_name='Registration'>" + rd[6] + "</div></td>");
                                table.Append("<td><div class='row_data' id='GmeLoanId' col_name='GmeLoan'>" + rd[7] + "</div></td>");
                                table.Append("<td><div class='row_data' id='SimCardId' col_name='SimCard'>" + rd[8] + "</div></td>");
                                table.Append("<td><div class='row_data' id='GmePassId' col_name='GmePass'>" + rd[9] + "</div></td>");
                                table.Append("<td><div class='row_data' id='IssueId' col_name='Issue'>" + rd[10] + "</div></td>");
                                table.Append("<td><div class='row_data' id='OtherId' col_name='Other'>" + rd[11] + "</div></td>");
                                table.Append("<td>" + rd[12] + "</td>");
                                table.Append("<td>" + rd[13] + "</td>");
                                table.Append("<td><div><input type='button' value='Update' class='btn btn-danger UpdateId' id='UpdateId'/></div></td>");
                                table.Append("</tr>");

                                idForTable++;
                            }
                        }
                        else
                        {
                            table.Append("<tr><td colspan='13' style='text-align:center;font-weight: bold;'>No data found.</td></tr>");
                        }
                        table.Append("</table>");
                        placeHolderId.Controls.Add(new Literal { Text = table.ToString() });
                        rd.Close();
                    }
                }

            }
            else
            {
                foreach (DateTime day in EachCalendarDay(StartDate, EndDate))
                {

                    placeHolderId.Controls.Clear();

                    SqlCommand comm = new SqlCommand("HTMLTableSP", con);
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Parameters.AddWithValue("@Flag", "Show");
                    comm.Parameters.AddWithValue("@TableValue", drpList.SelectedValue);
                    comm.Parameters.AddWithValue("@DateSelected", string.Format("{0:yyyy/MM/dd}", day.ToString()));
                    // comm.Parameters.AddWithValue("@DateSelectedLast", string.Format("{0:yyyy/MM/dd}", txtBoxThree.Text));
                    comm.ExecuteNonQuery();


                    SqlDataReader rd = comm.ExecuteReader();

                    int idForTable = 1;
                    table.Append("<div class='details' style='float:left'><label>Branch KPI Reports</label><br><label>From Date: '" + day.ToString() + "'</label> <label> </label> <label>To Date: '" + day.ToString() + "'</label><br><label>Branch: '" + drpList.SelectedItem.Text + "'</label></div>");
                    table.Append("<div style='position: relative;'><button class='exportID' style='background-color:transparent; border-color:transparent; float:right; margin-right:35px; position:relative; top:50px;'><img src='/excel img/excel2.jpg' height='20'/></button></div>");
                    table.Append("<table border='1' class='table table-hover tableId ' style='margin-left:20px; width:95%'>");
                    table.Append("<tr id='HeadingId'><th class='hiddenHeader'>HiddenId</th><th class='hiddenHeader'>BranchId</th><th class='hiddenHeader'>HiddenDate</th><th>Sn</th><th>Username</th><th>Name</th><th>Nationality</th><th>Registration</th><th>GMELoan</th><th>SimCard</th><th>GMEPass</th><th>IssueSolved</th><th>Other(Share, Bond, Tax Refund)</th><th>Staff Efficiency</th><th>Branch Efficiency</th><th>Edit</th></tr>");

                    if (rd.HasRows)
                    {
                        while (rd.Read())
                        {
                            table.Append("<tr>");
                            table.Append("<td class='hiddenColumnId' id='hiddenColumnId'>" + rd[0] + "</td>");
                            table.Append("<td class='hiddenColumnId' >" + rd[1] + "</td>");
                            table.Append("<td class='hiddenColumnId'>" + rd[2] + "</td>");
                            table.Append("<td id='rowId'>" + idForTable + "</td>");
                            table.Append("<td>" + rd[3] + "</td>");
                            table.Append("<td>" + rd[4] + "</td>");
                            table.Append("<td>" + rd[5] + "</td>");
                            table.Append("<td><div class='row_data' id='RegId' col_name='Registration'>" + rd[6] + "</div></td>");
                            table.Append("<td><div class='row_data' id='GmeLoanId' col_name='GmeLoan'>" + rd[7] + "</div></td>");
                            table.Append("<td><div class='row_data' id='SimCardId' col_name='SimCard'>" + rd[8] + "</div></td>");
                            table.Append("<td><div class='row_data' id='GmePassId' col_name='GmePass'>" + rd[9] + "</div></td>");
                            table.Append("<td><div class='row_data' id='IssueId' col_name='Issue'>" + rd[10] + "</div></td>");
                            table.Append("<td><div class='row_data' id='OtherId' col_name='Other'>" + rd[11] + "</div></td>");
                            table.Append("<td>" + rd[12] + "</td>");
                            table.Append("<td>" + rd[13] + "</td>");
                            table.Append("<td><div><input type='button' value='Update' class='btn btn-danger UpdateId' id='UpdateId'/></div></td>");
                            table.Append("</tr>");

                            idForTable++;
                        }
                    }
                    else
                    {
                        table.Append("<tr><td colspan='13' style='text-align:center;font-weight: bold;'>No data found.</td></tr>");
                    }
                    table.Append("</table>");
                    placeHolderId.Controls.Add(new Literal { Text = table.ToString() });
                    rd.Close();
                }

                con.Close();

            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Unnamed_Click(object sender, EventArgs e)
        {
            Load_data();

        }
        public static IEnumerable<DateTime> EachCalendarDay(DateTime startDate, DateTime endDate)
        {
            for (var date = startDate.Date; date.Date <= endDate.Date; date = date.AddDays(1)) yield
            return date;
        }
        [System.Web.Services.WebMethod]

        public static void Up_data(int Id, int Reg, int GmeLoan, int SimCard, int GmePass, int Issue, int Other)
        {

            string strcon = ConfigurationManager.ConnectionStrings["HTMLDB"].ConnectionString;
            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            SqlCommand comm = new SqlCommand("HTMLTableSP", con);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@Flag", "Update");
            comm.Parameters.AddWithValue("@Id", Id);
            comm.Parameters.AddWithValue("@Registration", Reg);
            comm.Parameters.AddWithValue("@GMELoan", GmeLoan);
            comm.Parameters.AddWithValue("@SimCard", SimCard);
            comm.Parameters.AddWithValue("@GmePass", GmePass);
            comm.Parameters.AddWithValue("@IssueSolved", Issue);
            comm.Parameters.AddWithValue("@Other", Other);

            comm.ExecuteNonQuery();
            con.Close();

            //var thisPage = new FinalTable();
            //thisPage.Load_data();

            //FinalTable obj = new FinalTable();
            //obj.Load_data();

        }
    }
}