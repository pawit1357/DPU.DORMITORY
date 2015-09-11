<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SearchRoom.aspx.cs" Inherits="DPU.DORMITORY.Web.View.Master.SearchRoom" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="Form1" method="post" runat="server" class="form-horizontal">

        <!-- BEGIN PAGE CONTENT-->
        <div class="row">
            <div class="col-md-12">
                <!-- BEGIN EXAMPLE TABLE PORTLET-->
                <div class="portlet box blue-madison">
                    <div class="portlet-title">
                        <div class="caption">
                            <i class="fa fa-cogs"></i>ข้อมูลห้องพัก
                        </div>
                        <div class="actions">
                            <asp:LinkButton ID="lbAdd" runat="server" class="btn btn-default btn-sm" OnClick="lbAdd_Click"><i class="icon-pencil"></i> Add</asp:LinkButton>
                        </div>
                    </div>
                    <div class="portlet-body">
                        <asp:GridView ID="gvResult" runat="server" AutoGenerateColumns="False"
                            CssClass="table table-striped table-hover table-bordered" ShowHeaderWhenEmpty="True" DataKeyNames="ID" OnRowCommand="gvResult_RowCommand" OnRowDeleting="gvResult_RowDeleting" OnPageIndexChanging="gvResult_PageIndexChanging">
                            <Columns>
                                <asp:BoundField HeaderText="ID" DataField="ID" ItemStyle-HorizontalAlign="Left" SortExpression="ID" />
                                <asp:BoundField HeaderText="อาคาร" DataField="BUILD" ItemStyle-HorizontalAlign="Left" SortExpression="BUILD" />
                                <asp:BoundField HeaderText="ประเภทห้อง" DataField="ROOM_TYPE" ItemStyle-HorizontalAlign="Left" SortExpression="ROOM_TYPE" />
                                <asp:BoundField HeaderText="กลุ่มอัตราค่าห้อง" DataField="RATE_GROUP" ItemStyle-HorizontalAlign="Left" SortExpression="RATE_GROUP" />
                                <asp:BoundField HeaderText="ชั้น" DataField="FLOOR" ItemStyle-HorizontalAlign="Left" SortExpression="FLOOR" />
                                <asp:BoundField HeaderText="เลขห้อง" DataField="NUMBER" ItemStyle-HorizontalAlign="Left" SortExpression="NUMBER" />
                                <asp:BoundField HeaderText="จำนวนคนต่อห้อง" DataField="CUSTOMER_LIMIT" ItemStyle-HorizontalAlign="Center" SortExpression="CUSTOMER_LIMIT" />
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnView" runat="server" ToolTip="View" CommandName="View" CommandArgument='<%# Eval("ID")%>'><i class="fa  fa-search"></i></asp:LinkButton>
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
        <!-- END PAGE CONTENT-->

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
                "pageLength": 10,
            });

            $('.select2_category').select2({
                placeholder: "Select an option",
                allowClear: true
            });

        });
    </script>
</asp:Content>
