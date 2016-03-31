<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SearchRates.aspx.cs" Inherits="DPU.DORMITORY.Web.View.Master.SearchRates" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="Form1" method="post" runat="server" class="form-horizontal">

        <!-- BEGIN PAGE CONTENT-->
        <div class="row">
            <div class="col-md-12">
                <!-- BEGIN EXAMPLE TABLE PORTLET-->
                <div class="portlet box blue-madison">
                    <div class="portlet-title">
                        <div class="caption">
                            <i class="fa fa-cogs"></i><asp:Literal ID="litPageTitle" runat="server" />
                        </div>
                        <div class="actions">
                            <asp:LinkButton ID="lbAdd" runat="server" class="btn btn-default btn-sm" OnClick="lbAdd_Click"><i class="icon-pencil"></i> Add</asp:LinkButton>
                        </div>
                    </div>
                    <div class="portlet-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">อาคาร:</label>
                                <div class="col-md-6">
                                    <div class="form-group" style="text-align: left">
                                        <asp:DropDownList ID="ddlBuild" runat="server" class="select2_category form-control" DataTextField="NAME" DataValueField="ID"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">ชื่อ:</label>
                                <div class="col-md-6">

                                    <div class="form-group" style="text-align: left">
                                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="" MaxLength="50"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="form-actions">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:LinkButton ID="btnSearch" runat="server" class="btn green" OnClick="btnSearch_Click"><i class="icon-search"></i> Search</asp:LinkButton>
                                        <asp:LinkButton ID="btnCancel" runat="server" class="btn default" OnClick="btnCancel_Click"><i class="icon-refresh"></i> Cancel</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                            </div>
                        </div>
                    </div>

                        <asp:GridView ID="gvResult" runat="server" AutoGenerateColumns="False"
                            CssClass="table table-striped table-hover table-bordered" ShowHeaderWhenEmpty="True" DataKeyNames="ID" OnRowCommand="gvResult_RowCommand" OnRowDeleting="gvResult_RowDeleting" OnPageIndexChanging="gvResult_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="ลำดับ" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--                                <asp:BoundField HeaderText="ID" DataField="ID" ItemStyle-HorizontalAlign="Left" SortExpression="ID" />--%>
                                <asp:BoundField HeaderText="อาคาร" DataField="BUILD" ItemStyle-HorizontalAlign="Left" SortExpression="BUILD" />
                                <asp:BoundField HeaderText="ชื่อ" DataField="NAME" ItemStyle-HorizontalAlign="Left" SortExpression="NAME" />
                                <asp:BoundField HeaderText="คำอธิบาย" DataField="DESCRIPTION" ItemStyle-HorizontalAlign="Left" SortExpression="DESCRIPTION" />
                                <asp:BoundField HeaderText="เงินค่าประกัน" DataField="INSURANCE_AMOUNT" ItemStyle-HorizontalAlign="Left" SortExpression="INSURANCE_AMOUNT" />
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
<%--                                        <asp:LinkButton ID="btnView" runat="server" ToolTip="View" CommandName="View" CommandArgument='<%# Eval("ID")%>'><i class="fa  fa-search"></i></asp:LinkButton>--%>
                                        <asp:LinkButton ID="btnEdit" runat="server" ToolTip="Edit" CommandName="Edit" CommandArgument='<%# Eval("ID")%>'><i class="fa fa-edit"></i></asp:LinkButton>
                                        <asp:LinkButton ID="btnDelete" runat="server" ToolTip="Delete" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                            CommandArgument='<%# Eval("ID")%>'><i class="fa fa-trash"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                            <EmptyDataTemplate>
                                <div class="data-not-found">
                                    <asp:Literal ID="libDataNotFound" runat="server" Text="Data Not found" />
                                </div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
                <!-- END EXAMPLE TABLE PORTLET-->

            </div>
        </div>

    </form>

    <!-- BEGIN PAGE LEVEL SCRIPTS -->
    <script src="<%= ResolveUrl("~/assets/global/plugins/jquery.min.js") %>" type="text/javascript"></script>
    <!-- END PAGE LEVEL SCRIPTS -->
    <script>
        jQuery(document).ready(function () {

            var table = $('#ContentPlaceHolder1_gvResult');
            // begin: third table
            table.dataTable({
                // set the initial value
                "pageLength": 50,
                "searching": false,
                "paging": true,
                "aoColumns": [
     null,
     null,
     null,
     null,
     null,
     { "bSortable": false } // <-- disable sorting for column 3
                ]
            });

            $('.select2_category').select2({
                placeholder: "Select an option",
                allowClear: true
            });
        });
    </script>
    <!-- END JAVASCRIPTS -->
</asp:Content>
