<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MealCheckIn.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cafeteria Check In</title>
    <script type="text/javascript">

        //Function to create textbox based on regular expressions
        function AcceptRegExOnly(event, regex) {
            var keyCode = event.which ? event.which : event.keyCode;

            var keyPressed = String.fromCharCode(keyCode);
            return regex.test(keyPressed);
        }; 

        //Function to create numeric text box only - using keycodes
        function AcceptNumericOnly(event, allowPeriod) {
            var keyCode = event.which ? event.which : event.keyCode;

            if ((keyCode >= 48 && keyCode <= 57) ||         //lets allow only numerics 
                ((allowPeriod == true) && (keyCode == 46))  //allow period conditionally based on the control's choice
            ) {
                return true;
            }

            return false;
        };

        //Function to create numeric text box only - using regex
        function AcceptNumericOnlyEx(event, allowPeriod) {
            if (allowPeriod == true) {
                return AcceptRegExOnly(event, /^[0-9.]$/);
            }
            return AcceptRegExOnly(event, /^[0-9]$/);

        };

    </script>
    <style>
        body {
            background-color: lightblue;
            font-family: sans-serif;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        
        <br /><br /><br />
        <center>

        <h1>Please enter your wristband number.</h1>
        <asp:TextBox Font-Size="40px" ID="txtWristbandIDNumber" Height="50px" Width="200px" runat="server" MaxLength="4" onkeypress="return AcceptNumericOnlyEx(event, false);"></asp:TextBox><br /><br />
        <asp:Label ID="lblConfirmation" runat="server" Font-Bold="true" Font-Size="20px" ForeColor="Red"></asp:Label><br /><br />


        <input type="submit" runat="server" id="btnSubmit" name="btnSubmit" value="Submit" style="height: 100px; width: 400px; font-size: 40px"/>

        <br /><br /><br />

        <asp:Label runat="server" ID="lblMessage" ForeColor="Red" Font-Size="20px"></asp:Label>

        </center>
    </div>
    </form>
</body>
</html>
