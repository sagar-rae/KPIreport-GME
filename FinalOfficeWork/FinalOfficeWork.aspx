<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FinalOfficeWork.aspx.cs" Inherits="FinalOfficeWork.FinalOfficeWork" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css"/>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="js/jquery.table2excel.js"></script>

    <style type="text/css">
        #HeadingId, #searchID{
            background-color:red;
            color:white;
           
        }
        .details{
            margin-left:20px;
        }
    </style>
    <script type="text/javascript">
         
        function Valid_form() {
            var firstDate = document.getElementById("txtBoxTwo").value;
            var secondDate = document.getElementById("txtBoxThree").value;
            if (firstDate == "") {
                alert("Plese select the date.");
                return false;
            }
            if (secondDate == "") {
                alert("Please select the date.");
                return false;
            }
            return true;
        }

        


        $(document).ready(function () {

            //------------hide section---------
            $('.hiddenHeader').hide();
            $('.hiddenColumnId').hide();
            //---------------------------
          
            //--------------makeEditable------
            $(document).on('click', '.row_data', function () {
                $(this).closest('div').attr('contenteditable', 'true')
                $(this).addClass('bg-warning').css('padding', '5px')
                $(this).focus()
            });

            $(document).on('focusout', '.row_data', function () {
                var row_div = $(this)
                .removeAttr('contenteditable')
                .removeClass('bg-warning')
                .css('padding', '')
            });
            //----------------------------------

            $('.UpdateId').click(function () {
                var currentRow = $(this).closest('tr');
                var col1 = currentRow.find('td:eq(0)').text();
                var col2 = currentRow.find('td:eq(7)').text();
                var col3 = currentRow.find('td:eq(8)').text();
                var col4 = currentRow.find('td:eq(9)').text();
                var col5 = currentRow.find('td:eq(10)').text();
                var col6 = currentRow.find('td:eq(11)').text();
                var col7 = currentRow.find('td:eq(12)').text();
                
                
                var jsondata = {
                    Id: col1,
                    Reg: col2,
                    GmeLoan: col3,
                    SimCard: col4,
                    GmePass: col5,
                    Issue: col6,
                    Other: col7
                };

                     $.ajax({
                    url: 'FinalOfficeWork.aspx/Up_data',
                         type: 'Post',
                         data: JSON.stringify(jsondata),
                    contentType: 'application/json; charset=utf - 8',
                    dataType: 'json',
                    success: function () {
                        alert('Success');
                    },      
                    failure: function() {
                         alert('Failed');
                    }
                    });          
            });


            $(".exportID").click(function () {

                $('.tableId').table2excel();
            });

        });     
     
            
    </script> 
    <title></title>
</head>
<body>
    <label style="background-color:red; color:white; margin-left:20px">KPI Report Entry</label>
   <form id="form1" runat="server">
         <div class="panel panel-default col-md-10" style="background-color:ghostwhite; padding-top:20px; padding-bottom:20px; margin-left:20px">         
            <div class="panel-body">
                <div class="form-group">
                    <asp:Label CssClass="col-md-2" Text="From Date:" runat="server" />
                    <asp:TextBox ID="txtBoxTwo" CssClass=" col-md-6 input-sm" TextMode="Date" runat="server" />               
                </div>
            </div>


            <div class="panel-body">
                <div class="form-group">
                    <asp:Label CssClass="col-md-2" Text="To Date:" runat="server" />
                    <asp:TextBox class="col-md-8" ID="txtBoxThree" CssClass=" col-md-6 input-sm" TextMode="Date" runat="server" />                   
                </div>
            </div>

            <div class="panel-body">
                <div class="form-group">
                    <asp:Label CssClass="col-md-2" Text="Branch Name:" runat="server" />
                    <asp:DropDownList class="col-md-8" ID="drpList" CssClass=" col-md-6 input-sm" runat="server">
                        <asp:ListItem Text="All Branch" Value="0" />
                        <asp:ListItem Text="Mobile Remit" Value="1" />
                        <asp:ListItem Text="Songu-ri" Value="2" />
                        <asp:ListItem Text="Hyehwa" Value="3" />
                        <asp:ListItem Text="DDM CIS" Value="4" />
                        <asp:ListItem Text="Mongol Town" Value="5" />
                        <asp:ListItem Text="Gwangju" Value="6" />
                        <asp:ListItem Text="Suwon" Value="7" />
                        <asp:ListItem Text="GME Online" Value="8" />
                        <asp:ListItem Text="Dongdaemun" Value="9" />
                        <asp:ListItem Text="Ansan" Value="10" />
                        <asp:ListItem Text="Hwaseong" Value="11" />
                        <asp:ListItem Text="Gimhae" Value="12" />
                    </asp:DropDownList>                   
                </div>
            </div>
            

            <div class="panel-body">
                <asp:Button Text="Search" ID="searchID" CssClass="btn btn-danger col-md-1" runat="server" OnClientClick="return Valid_form()" OnClick="Unnamed_Click"/>              
            </div>
        </div>  
    </form>

   
   

     <asp:PlaceHolder ID="placeHolderId" runat="server"></asp:PlaceHolder>
</body>
</html>
