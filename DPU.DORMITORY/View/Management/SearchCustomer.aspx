<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SearchCustomer.aspx.cs" Inherits="DPU.DORMITORY.Web.View.Management.SearchCustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="Form1" method="post" runat="server" class="form-horizontal">

        <!-- BEGIN PAGE CONTENT-->
        <div class="row">
            <div class="col-md-12">
                <!-- BEGIN EXAMPLE TABLE PORTLET-->
                <div class="portlet box blue-madison">
                    <div class="portlet-title">
                        <div class="caption">
                            <i class="fa fa-cogs"></i>ค้นหาลูกค้าหอพัก
                        </div>
                        <div class="actions">
                        </div>
                    </div>
                    <div class="portlet-body">
                        <asp:GridView ID="gvResult" runat="server" AutoGenerateColumns="False" AllowPaging="False"
                            CssClass="table table-striped table-hover table-bordered" ShowHeaderWhenEmpty="True" DataKeyNames="ID" OnRowCommand="gvResult_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Customer ID" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Literal ID="litCusID" runat="server" Text='<%# Eval("CUSTOMER_NUMBER")%>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ชื่อ" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Literal ID="litName" runat="server" Text='<%# Eval("FIRSTNAME")%>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="นามสกุล" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Literal ID="litSurname" runat="server" Text='<%# Eval("SURNAME")%>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ประเภทลูกค้า" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Literal ID="litCustomerType" runat="server" Text='<%# Eval("CUSTOMER_TYPE")%>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="เลขบัตรประชาชน/เลขที่หนังสือเดินทาง" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Literal ID="litPersonalID" runat="server" Text='<%# Eval("PERSONALID")%>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="วันที่เข้าพัก" DataField="CHECKIN_DATE" DataFormatString="{0:dd/MM/yyyy}"
                                    ItemStyle-HorizontalAlign="Center" />

<%--                                <asp:TemplateField HeaderText="วันที่เข้าพัก" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Literal ID="litCheckInDate" runat="server" Text='<%# Eval("CHECKIN_DATE")%>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>--%>
                                <%--                                    <asp:TemplateField HeaderText="วันที่จอง" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Literal ID="litReservDate" runat="server" Text='<%# Eval("RESERV_DATE")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="เบอร์ติดต่อ" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Literal ID="litPhone" runat="server" Text='<%# Eval("PHONE")%>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnInfo" runat="server" ToolTip="Edit" CommandName="Edit" CommandArgument='<%# Eval("ID")%>'><i class="fa  fa-edit"></i></asp:LinkButton>
                                        <asp:LinkButton ID="btnCheckOut" runat="server" ToolTip="Check Out" CommandName="CheckOut" CommandArgument='<%# Eval("ID")%>'><i class="fa fa-sign-out"></i></asp:LinkButton>
                                        <asp:LinkButton ID="btnChangeRoom" runat="server" ToolTip="Change Room" CommandName="MoveRoom" CommandArgument='<%# Eval("ID")%>'><i class="fa fa-retweet"></i></asp:LinkButton>
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
    <!-- END JAVASCRIPTS -->
</asp:Content>
