﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SearchRole.aspx.cs" Inherits="DPU.DORMITORY.Web.View.Admin.SearchRole" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <form id="Form1" method="post" runat="server" class="form-horizontal">

        <div class="row">
            <div class="col-md-12">
                <!-- BEGIN EXAMPLE TABLE PORTLET-->
                <div class="portlet box blue-dark">
                    <div class="portlet-title">
                        <div class="caption">
                            <i class="fa fa-cogs"></i>Search Role
                        </div>
                        <div class="actions">
                            <asp:LinkButton ID="lbAdd" runat="server" class="btn btn-default btn-sm" OnClick="lbAdd_Click"><i class="icon-pencil"></i> Add</asp:LinkButton>

                        </div>
                    </div>
                    <div class="portlet-body">
                        <!-- BEGIN FORM-->
                        <asp:Label ID="lbTotalRecords" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:GridView ID="gvResult" runat="server" AutoGenerateColumns="False"
                            CssClass="table table-striped table-hover table-bordered" ShowHeaderWhenEmpty="True" DataKeyNames="ROLE_ID" OnRowCommand="gvResult_RowCommand" OnRowDeleting="gvResult_RowDeleting" OnPageIndexChanging="gvResult_PageIndexChanging">
                            <Columns>
                                <asp:BoundField HeaderText="ID" DataField="ROLE_ID" ItemStyle-HorizontalAlign="Left" SortExpression="ROLE_ID" />
                                <asp:BoundField HeaderText="Name" DataField="NAME" ItemStyle-HorizontalAlign="Left" SortExpression="NAME" />

                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" ToolTip="Edit" CommandName="Edit" CommandArgument='<%# Eval("ROLE_ID")%>'><i class="fa fa-edit"></i></asp:LinkButton>
                                        <asp:LinkButton ID="btnDelete" runat="server" ToolTip="Delete" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                            CommandArgument='<%# Eval("ROLE_ID")%>'><i class="fa fa-trash"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                            <EmptyDataTemplate>
                                <div class="data-not-found">
                                    <asp:Literal ID="libDataNotFound" runat="server" Text="Data Not found" />
                                </div>
                            </EmptyDataTemplate>
                        </asp:GridView>


                        <!-- END FORM-->
                    </div>
                </div>
            </div>
        </div>
    </form>

    <!-- BEGIN PAGE LEVEL SCRIPTS -->
    <script src="<%= ResolveUrl("~/assets/global/plugins/jquery.min.js") %>" type="text/javascript"></script>
    <!-- END PAGE LEVEL SCRIPTS -->
    <script>
        jQuery(document).ready(function () {

            var table = $('#ContentPlaceHolder2_gvResult');
            // begin: third table
            table.dataTable({
                // set the initial value
                "pageLength": 10,
            });

        });
    </script>
    <!-- END JAVASCRIPTS -->
</asp:Content>
