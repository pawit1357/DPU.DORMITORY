<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Posting2SAP.aspx.cs" Inherits="DPU.DORMITORY.Web.View.Account.Posting2SAP" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="Form1" method="post" runat="server" class="form-horizontal">
        <asp:ToolkitScriptManager ID="ToolkitScript1" runat="server" />

        <!-- BEGIN PAGE CONTENT-->
        <div class="row">
            <div class="col-md-12">
                <asp:Panel ID="pMainPage" runat="server">
                    <div class="portlet light bordered">
                        <div class="portlet-title">
                            <div class="caption">
                                <i class="icon-equalizer font-red-sunglo"></i>
                                <span class="caption-subject font-red-sunglo bold uppercase">Search Condition</span>
                                <span class="caption-helper"></span>
                            </div>
                            <div class="tools">
                                <%--<a href="#" class="collapse"></a>--%>
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <div class="form-body">
                                <!-- BEGIN FORM-->
                                <div class="row">
                                    <div class="col-md-9">
                                        <div class="form-group">
                                            <label class="control-label col-md-3">อาคาร:</label>
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="ddlBuild" runat="server" class="select2_category form-control" DataTextField="NAME" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="ddlBuild_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-9">
                                        <div class="form-group">
                                            <label class="control-label col-md-3">ห้อง:</label>
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="ddlRoom" runat="server" class="select2_category form-control" DataTextField="NUMBER" DataValueField="ID"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-9">
                                        <div class="form-group">
                                            <asp:Label ID="Label1" runat="server" CssClass="control-label col-md-3">เดือน:</asp:Label>
                                            <div class="col-md-6">
                                                <div class="input-group input-medium date date-picker" data-date="" data-date-format="mm/yyyy" data-date-viewmode="years" data-date-minviewmode="months">
                                                    <asp:TextBox ID="txtPostingDate" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <span class="input-group-btn">
                                                        <button class="btn default" type="button"><i class="fa fa-calendar"></i></button>
                                                    </span>
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
                                <!-- END FORM-->
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <div class="form-body">
                                <%--<CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />--%>
                            </div>
                        </div>
                    </div>
                    <!-- BEGIN EXAMPLE TABLE PORTLET-->
                    <div class="portlet box blue-madison">
                        <div class="portlet-title">
                            <div class="caption">
                                <i class="fa fa-cogs"></i>โอนข้อมูลไป SAP
                            </div>
                            <div class="actions">
                                <asp:Button ID="btnTransfer" runat="server" Text="POST" CssClass="btn btn-default btn-sm" OnClick="btnTransfer_Click" />
                            </div>
                        </div>
                        <div class="portlet-body">
                            <asp:GridView ID="gvResult" runat="server" AutoGenerateColumns="False" AllowPaging="False"
                                CssClass="table table-striped table-hover table-bordered" ShowHeaderWhenEmpty="True" DataKeyNames="ID" OnRowCommand="gvResult_RowCommand" OnRowDataBound="gvResult_RowDataBound">
                                <Columns>
                                    <asp:BoundField HeaderText="POST ID" DataField="ID" ItemStyle-HorizontalAlign="Left" SortExpression="ID" />
                                    <asp:BoundField HeaderText="BUILDING ID" DataField="BID" ItemStyle-HorizontalAlign="Left" SortExpression="BID" />
                                    <asp:BoundField HeaderText="เดือน" DataField="POSTING_DATE" ItemStyle-HorizontalAlign="Left" SortExpression="POSTING_DATE" DataFormatString="{0:MM/yyyy}" />
                                    <asp:BoundField HeaderText="อาคาร" DataField="NAME" ItemStyle-HorizontalAlign="Left" SortExpression="NAME" />
                                    <asp:BoundField HeaderText="ห้อง" DataField="NUMBER" ItemStyle-HorizontalAlign="Left" SortExpression="NUMBER" />
                                    <asp:BoundField HeaderText="เลขทะเบียน" DataField="CUSTOMER_NUMBER" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="ชื่อ" DataField="PAYER_NAME" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="BA" DataField="BA" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Amout" DataField="AMOUNT" ItemStyle-HorizontalAlign="Left" />
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEdit" runat="server" ToolTip="EDIT_INVOICE" CommandName="EDIT_INVOICE" CommandArgument='<%# Eval("ID")%>'><i class="fa fa-edit"></i></asp:LinkButton>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="btnUpdate" runat="server" ToolTip="Update" ValidationGroup="CreditLineGrid"
                                                CommandName="Update"><i class="fa fa-save"></i></asp:LinkButton>
                                            <asp:LinkButton ID="LinkCancel" runat="server" ToolTip="Cancel" CausesValidation="false"
                                                CommandName="Cancel"><i class="fa fa-remove"></i></asp:LinkButton>
                                        </EditItemTemplate>
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
                </asp:Panel>
                <asp:Panel ID="pTransferResult" runat="server">
                </asp:Panel>
            </div>
        </div>
        <!-- END PAGE CONTENT-->

        <div class="modal-wide" id="pnlModalDemo" style="display: none;">
            <div class="modal-content">
                <div class="modal-header">
                    <div class="modal-title">
                        แก้ไขรายละเอียดค่าใช้จ่าย
                    </div>
                </div>
                <div class="modal-body" style="width: 600px; height: 400px; overflow-x: hidden; overflow-y: scroll; padding-bottom: 10px;">
                    <asp:GridView ID="gvInvoiceDetail" runat="server" AutoGenerateColumns="False" Width="550px" CssClass="table table-striped table-hover table-bordered"
                        ShowHeaderWhenEmpty="True" DataKeyNames="ID,row_type" OnRowDataBound="gvInvoiceDetail_RowDataBound" OnRowEditing="gvInvoiceDetail_RowEditing" OnRowUpdating="gvInvoiceDetail_RowUpdating" OnRowCancelingEdit="gvInvoiceDetail_RowCancelingEdit" OnRowDeleting="gvInvoiceDetail_RowDeleting">
                        <Columns>
                            <asp:TemplateField HeaderText="ลำดับ" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="รายการค่าใช้จ่าย" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Literal ID="litM_SERVICE_NAME" runat="server" Text='<%# Eval("M_SERVICE_NAME")%>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <%--                     <asp:TemplateField HeaderText="ผู้ชำระ" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Literal ID="litPAYER_NAME" runat="server" Text='<%# Eval("PAYER_NAME")%>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>--%>

                            <asp:TemplateField HeaderText="จำนวนเงินที่ชำระ" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Literal ID="litPAY_AMOUNT" runat="server" Text='<%# Eval("PAYMENT_AMOUNT","{0:N2}")%>' />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtPAY_AMOUNT" runat="server" Text='<%# Eval("PAYMENT_AMOUNT","{0:N2}")%>' class="form-control"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtenderReceiptinAc" TargetControlID="txtPAY_AMOUNT"
                                        FilterType="Numbers" ValidChars=".," runat="server" />
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="หมายเหตุ" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Literal ID="litREMARK" runat="server" Text='<%# Eval("REMARK")%>' />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtREMARKT" runat="server" Text='<%# Eval("REMARK")%>' class="form-control"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEdit" runat="server" ToolTip="Edit" CommandName="Edit" CommandArgument='<%# Eval("ID")%>'><i class="fa fa-edit"></i></asp:LinkButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="btnUpdate" runat="server" ToolTip="Update" ValidationGroup="CreditLineGrid"
                                        CommandName="Update"><i class="fa fa-save"></i></asp:LinkButton>
                                    <asp:LinkButton ID="LinkCancel" runat="server" ToolTip="Cancel" CausesValidation="false"
                                        CommandName="Cancel"><i class="fa fa-remove"></i></asp:LinkButton>
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                        <EmptyDataTemplate>
                            <div class="data-not-found">
                                <asp:Literal ID="libDataNotFound" runat="server" Text="Data Not found" />
                            </div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="OK" runat="server" CssClass="btn purple" Style="margin-top: 10px; margin-left: 20px;" Text="บันทึก" OnClick="OK_Click" />

                    <asp:Button ID="btnClose" CssClass="btn default" Style="margin-top: 10px;" runat="server" Text="ปิด" OnClick="btnCancel_Click" />
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->

        <asp:LinkButton ID="lnkFake" runat="server">
        </asp:LinkButton>
        <asp:ModalPopupExtender ID="ModolPopupExtender" runat="server" PopupControlID="pnlModalDemo"
            TargetControlID="lnkFake" BackgroundCssClass="modal-backdrop modal-print-form fade in" BehaviorID="mpModalDemo"
            CancelControlID="btnClose">
        </asp:ModalPopupExtender>

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
