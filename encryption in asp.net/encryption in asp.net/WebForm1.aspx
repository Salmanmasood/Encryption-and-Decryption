<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="encryption_in_asp.net.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h1>File Upload with Encryption</h1>


        <table>
            <tr> <td><b>Upload :</b></td> <td>
                <asp:FileUpload ID="FileUpload1" runat="server" />   </td> </tr>

            
            <tr> 
                <td></td>
                <td><asp:Button ID="Button1" runat="server" Text="Encrypt! " OnClick="Button1_Click" /></td> 

            </tr>

            <tr> <td></td> <td></td> </tr>


            
        </table>

        <asp:DataList ID="DataList1" runat="server" RepeatColumns="4"  RepeatDirection="Horizontal" OnItemCommand="DataList1_ItemCommand">

            <ItemTemplate>
                <table>
                    <tr>
                        <td>
                            <img src='<%#Eval("icon")%>'  />
                        </td>

                    </tr>

                    <tr>
                        <td>
                    <%#Eval("filename")%>
                        </td>

                    </tr>

                    <tr>
                        <td>
                    <%#Eval("size", "{0} kb")%>
                        </td>

                    </tr>

                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton1"  CommandName="Download" CommandArgument='<%#Eval("FilePath") %>' runat="server">Download!</asp:LinkButton>

                        </td>
                        

                    </tr>




                </table>

            </ItemTemplate>

        </asp:DataList>
    </div>
    </form>
</body>
</html>
