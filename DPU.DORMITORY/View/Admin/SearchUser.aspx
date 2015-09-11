<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SearchUser.aspx.cs" Inherits="DPU.DORMITORY.Web.View.Admin.SearchUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="Form2" method="post" runat="server" class="form-horizontal">
        <div class="row">
            <div class="col-md-12">
                <!-- BEGIN EXAMPLE TABLE PORTLET-->
                <div class="portlet box blue-dark">
                    <div class="portlet-title">
                        <div class="caption">
                            <i class="fa fa-cogs"></i>Search Result
                        </div>
                        <div class="actions">
                            <asp:LinkButton ID="btnAdd" runat="server" class="btn btn-default btn-sm" OnClick="lbAdd_Click"><i class="icon-pencil"></i> Add</asp:LinkButton>
                        </div>
                    </div>
                    <div class="portlet-body">
                        <!-- BEGIN FORM-->
                        <asp:GridView ID="gvResult" runat="server" AutoGenerateColumns="False"
                            CssClass="table table-striped table-hover table-bordered" ShowHeaderWhenEmpty="True" DataKeyNames="USER_ID" OnRowCommand="gvResult_RowCommand" OnRowDeleting="gvResult_RowDeleting">
                            <Columns>
                                <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Role" DataField="ROLE_NAME" ItemStyle-HorizontalAlign="Left" SortExpression="ROLE_NAME" />
                                <asp:BoundField HeaderText="คำนำหน้า" DataField="TITLE_NAME" ItemStyle-HorizontalAlign="Left" SortExpression="TITLE_NAME" />
                                <asp:BoundField HeaderText="ชื่อผู้ใช้" DataField="USER_ID" ItemStyle-HorizontalAlign="Left" SortExpression="USER_ID" />
                                <asp:BoundField HeaderText="ชื่อ" DataField="FIRST_NAME" ItemStyle-HorizontalAlign="Left" SortExpression="FIRST_NAME" />
                                <asp:BoundField HeaderText="นามสกุล" DataField="LAST_NAME" ItemStyle-HorizontalAlign="Left" SortExpression="LAST_NAME" />
                                <asp:BoundField HeaderText="อีเมล์" DataField="EMAIL_ADDRESS" ItemStyle-HorizontalAlign="Left" SortExpression="EMAIL_ADDRESS" />
                                <asp:BoundField HeaderText="เบอร์ติดต่อ" DataField="PHONE_NO" ItemStyle-HorizontalAlign="Left" SortExpression="PHONE_NO" />
                                <asp:BoundField HeaderText="ล็อคอินครั้งสุดท้าย" DataField="LAST_SIGN_IN_DATE" ItemStyle-HorizontalAlign="Left" SortExpression="LAST_SIGN_IN_DATE" DataFormatString="{0:dd-MM-yyyy HH:mm:ss}" />
                                <asp:BoundField HeaderText="วันที่สร้าง" DataField="CREATE_DATE" ItemStyle-HorizontalAlign="Left" SortExpression="CREATE_DATE" DataFormatString="{0:dd-MM-yyyy}" />

                                <asp:BoundField HeaderText="สถานะ" DataField="IS_ACTIVE" ItemStyle-HorizontalAlign="Center" SortExpression="IS_ACTIVE" />

                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" ToolTip="Edit" CommandName="Edit" CommandArgument='<%# Eval("USER_ID")%>'><i class="fa fa-edit"></i></asp:LinkButton>
                                        <asp:LinkButton ID="btnDelete" runat="server" ToolTip="Delete" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                            CommandArgument='<%# Eval("USER_ID")%>'><i class="fa fa-trash"></i></asp:LinkButton>
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
