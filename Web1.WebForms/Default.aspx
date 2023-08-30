<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Web1.WebForms._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>ASP.NET</h1>
        <p class="lead">ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS, and JavaScript.</p>
        <p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>
    </div>
    <div class ="row">
    
        <div class ="col-md -4">
            <table class ="table">
                <thead>
                    <tr>
                    <th>Mã sinh viên</th>
                    <th>Tên Sinh viên</th>
                    <th>Địa chỉ</th>
                    <th>Tuổi</th>
                </tr>
                </thead>
                <tbody>
                     <%
                XElement xElementStudent = XElement.Load(@"\XML\Student.xml");
                var results
                    = from list in xElementStudent.Elements("Student") select list;
              foreach (var item in results)
              {
                   %>
                    <tr>
                        <td><% = item.Element("StudentId").Value %></td>
                        <td><% = item.Element("StudentName").Value %></td>
                        <td><% = item.Element("StudentAddress").Value %></td>
                        <td><% = item.Element("StudentAge").Value %></td>
                    </tr>
                    <%
              }
              %>
                </tbody>
      
            </table>
        </div>
          <%--<div class="col-md-4">
            <h2>Example xml</h2>
            <%
                XElement xElementStudent = XElement.Load(@"D:\Private\4.Edu\4.Subjects\1.Web\sem3_web\Web1.WebForms\XML\Student.xml");
                var results
                    = from list in xElementStudent.Elements("Student") where (string)list.Element("StudentId") == "1000" select list;
                foreach (XElement element in results)
                {
            %>
            <% =element %>
            <%
                }

            %>--%>
        <%--</div>--%>
    </div>
    <div class="row">
        <div class="col-md-4">
            <h2>Getting started</h2>
            <p>
                ASP.NET Web Forms lets you build dynamic websites using a familiar drag-and-drop, event-driven model.
            A design surface and hundreds of controls and components let you rapidly build sophisticated, powerful UI-driven sites with data access.
            </p>
            <p>
                <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301948">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Get more libraries</h2>
            <p>
                NuGet is a free Visual Studio extension that makes it easy to add, remove, and update libraries and tools in Visual Studio projects.
            </p>
            <p>
                <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301949">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Web Hosting</h2>
            <p>
                You can easily find a web hosting company that offers the right mix of features and price for your applications.
            </p>
            <p>
                <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301950">Learn more &raquo;</a>
            </p>
        </div>
    </div>

</asp:Content>
