<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="RoomDetail.aspx.cs" Inherits="DPU.DORMITORY.Web.View.Management.RoomDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="Form1" method="post" runat="server" class="form-horizontal">
        <div class="portlet light bordered">
            <div class="portlet-title">
                <div class="caption">
                    <i class="icon-equalizer font-red-sunglo"></i>
                    <span class="caption-subject font-red-sunglo bold uppercase">รายละเอียดห้องพัก</span>
                    <span class="caption-helper"></span>
                </div>
                <div class="tools">
                </div>
            </div>
            <div class="portlet-body form">
                <!-- BEGIN FORM-->

                <div class="form-body">
                    <%--<h3 class="form-section">ข้อมูลหอพัก</h3>--%>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">หอพัก</label>
                                <div class="col-md-9">
                                    <div class="input-group" style="text-align: left">
                                        <asp:TextBox ID="txtBuild" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <!--/span-->
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">เลขห้อง</label>
                                <div class="col-md-9">
                                    <div class="input-group" style="text-align: left">
                                        <asp:TextBox ID="txtRoom" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--/row-->
                    <h4 class="form-section">รายการค่าใช้จ่าย</h4>
                    <div class="row">
                        <div class="col-md-9">
                            <asp:GridView ID="gvResult" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                CssClass="table table-striped table-hover table-bordered" ShowHeaderWhenEmpty="True" DataKeyNames="ID">
                                <Columns>
                                    <asp:TemplateField HeaderText="รายการค่าใช้จ่าย" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Literal ID="litServiceID" runat="server" Text='<%# Eval("SERVICE_NAME")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Subtran" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Literal ID="litSubtran" runat="server" Text='<%# Eval("SUBTRAN")%>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtSubtran" runat="server" Text='<%# Eval("SUBTRAN")%>' CssClass="form-control"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="GL" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Literal ID="litGL" runat="server" Text='<%# Eval("GL")%>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtGL" runat="server" Text='<%# Eval("GL")%>' CssClass="form-control"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="AMOUNT" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Literal ID="litAMOUNT" runat="server" Text='<%# Eval("AMOUNT")%>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtAMOUNT" runat="server" Text='<%# Eval("AMOUNT")%>' CssClass="form-control"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="VAT" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Literal ID="litVAT" runat="server" Text='<%# Eval("VAT")%>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtVAT" runat="server" Text='<%# Eval("VAT")%>' CssClass="form-control"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
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

                    <h4 class="form-section">รายชื่อผู้พัก</h4>
                    <div class="row">
                        <div class="col-md-9">
                            <asp:GridView ID="gvCustomer" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                CssClass="table table-striped table-hover table-bordered" ShowHeaderWhenEmpty="True" DataKeyNames="ID">
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

                                    <asp:TemplateField HeaderText="เลขบัตรประชาชน" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Literal ID="litPersonalID" runat="server" Text='<%# Eval("PERSONALID")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="วันที่เข้าพัก" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Literal ID="litCheckInDate" runat="server" Text='<%# Eval("CHECKIN_DATE")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
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
                                </Columns>

                                <EmptyDataTemplate>
                                    <div class="data-not-found">
                                        <asp:Literal ID="libDataNotFound" runat="server" Text="Data Not found" />
                                    </div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </div>

                    <!--/row-->
                    <div class="form-actions">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">

                                        <asp:LinkButton ID="btnCancel" runat="server" class="btn default" OnClick="btnCancel_Click"> Cancel</asp:LinkButton>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                    <!-- END FORM-->
                </div>

            </div>
        </div>
    </form>

    <!-- BEGIN PAGE LEVEL SCRIPTS -->
        <script src="<%= ResolveUrl("~/assets/global/plugins/jquery.min.js") %>" type="text/javascript"></script>
    <!-- END PAGE LEVEL SCRIPTS -->
    <script>
        jQuery(document).ready(function () {

            $('.select2_category').select2({
                placeholder: "Select an option",
                allowClear: true
            });


            var table = $('#ContentPlaceHolder1_gvResult');
            // begin: third table
            table.dataTable({
                // set the initial value
                "pageLength": 10,
            });
            var table1 = $('#ContentPlaceHolder1_gvCustomer');
            // begin: third table
            table1.dataTable({
                // set the initial value
                "pageLength": 10,
            });
        });
    </script>
    <!-- END JAVASCRIPTS -->
</asp:Content>
