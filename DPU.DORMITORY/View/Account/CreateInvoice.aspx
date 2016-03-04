<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CreateInvoice.aspx.cs" Inherits="DPU.DORMITORY.Web.View.Account.CreateInvoice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="Form1" method="post" runat="server" class="form-horizontal">
        <asp:ToolkitScriptManager ID="ToolkitScript1" runat="server" />
        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>
        <div class="portlet light bordered">
            <div class="portlet-title">
                <div class="caption">
                    <i class="icon-equalizer font-red-sunglo"></i>
                    <span class="caption-subject font-red-sunglo bold uppercase">บันทึกข้อมูลตั้งหนี้</span>
                    <span class="caption-helper"></span>
                </div>
                <div class="tools">
                </div>
            </div>
            <div class="portlet-body form">
                <!-- BEGIN FORM-->
                <div class="alert alert-danger display-hide">
                    <button class="close" data-close="alert"></button>
                    You have some form errors. Please check below.
                </div>
                <div class="alert alert-success display-hide">
                    <button class="close" data-close="alert"></button>
                    Your form validation is successful!
                </div>
                <div class="tabbable-line boxless tabbable-reversed">
                    <ul class="nav nav-tabs">
                        <li class="<%=pActive01 %>">
                            <asp:LinkButton ID="btnMainData" runat="server" OnClick="btnMainData_Click">ป้อนข้อมูลหน่วยค่าน้ำค่าไฟ</asp:LinkButton>
                        </li>
                        <li class="<%=pActive02 %>">
                            <asp:LinkButton ID="btnPamentDetail" runat="server" OnClick="btnPamentDetail_Click">ตั้งหนี้</asp:LinkButton>
                        </li>
                        <%--               <li class="<%=pActive03 %>">
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="btnOwner_Click">รายการใบแจ้งหนี้ที่ยังไม่ชำระเงิน</asp:LinkButton>
                        </li>--%>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane <%=pActive01 %>" id="tab_0">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label col-md-3">อาคาร<span class="required" aria-required="true">* </span></label>
                                        <div class="col-md-9">
                                            <asp:DropDownList ID="ddlBuild" runat="server" class="select2_category form-control" DataTextField="Name" DataValueField="ID"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <!--/span-->
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label col-md-3">ห้อง<span class="required" aria-required="true">* </span></label>
                                        <div class="col-md-9">
                                            <div class="input-group" style="text-align: left">
                                                <asp:TextBox ID="txtRoom" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                         <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" TargetControlID="txtRoom"
                                                    FilterType="Numbers" ValidChars=".," runat="server" />
                                                <span class="input-group-btn">
                                                    <%--<asp:Button ID="btnCheckRoom" runat="server" Text="Check" CssClass="btn green" OnClick="btnCheckRoom_Click" />--%>
                                                    <asp:LinkButton ID="btnCheckRoom" runat="server" CssClass="btn green" OnClick="btnCheckRoom_Click"> Check</asp:LinkButton>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <!--/span-->
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label col-md-3">เดือน<span class="required" aria-required="true">* </span></label>
                                        <div class="col-md-9">
                                            <div class="input-group input-medium date date-picker" data-date-format="mm/yyyy" data-date-viewmode="years" data-date-minviewmode="months">
                                                <asp:TextBox ID="txtPostingDate" runat="server" CssClass="form-control" OnTextChanged="txtPostingDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                <span class="input-group-btn">
                                                    <button class="btn default" type="button"><i class="fa fa-calendar"></i></button>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <table class="table table-striped table-hover table-bordered">
                                        <thead>
                                            <tr>
                                                <th>รายการ</th>
                                                <th>มิเตอร์ต้น(หน่วย)</th>
                                                <th>มิเตอร์ปลาย(หน่วย)</th>
                                                <th>ใช้ไปทั้งหมด(หน่วย)</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>ค่าไฟฟ้า</td>
                                                <td>
                                                    <asp:TextBox ID="txtElecMeterStart" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtenderReceiptinAc" TargetControlID="txtElecMeterStart"
                                                        FilterType="Numbers" ValidChars=".," runat="server" />

                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtElecMeterEnd" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtElecMeterEnd"
                                                        FilterType="Numbers" ValidChars=".," runat="server" />

                                                </td>
                                                <td>
                                                    <asp:Label ID="lbElecUnit" runat="server" Text="0"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>ค่าน้ำ</td>
                                                <td>
                                                    <asp:TextBox ID="txtWaterMeterStart" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtWaterMeterStart"
                                                        FilterType="Numbers" ValidChars=".," runat="server" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtWaterMeterEnd" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" TargetControlID="txtWaterMeterEnd"
                                                        FilterType="Numbers" ValidChars=".," runat="server" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbWaterUnit" runat="server" Text="0"></asp:Label>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="row">
                                            <div class="col-md-offset-3 col-md-9">
                                                <asp:Button ID="btnCalculate" runat="server" class="btn green" Text="คำนวณมิเตอร์" OnClick="btnCalculate_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane <%=pActive02 %>" id="tab_1">
                            <div class="row">
                                <div class="col-md-9">

                                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-hover table-bordered" OnRowCommand="GridView1_RowCommand">
                                        <Columns>
                                            <asp:CommandField ShowSelectButton="True" />
                                        </Columns>
                                    </asp:GridView>
                                    <br />
                                </div>
                            </div>
                            <div class="row">
                                <!--/span-->
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label col-md-3">จำนวนวันที่เข้าพัก</label>
                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtStayDay" runat="server" CssClass="form-control"></asp:TextBox>

                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" TargetControlID="txtStayDay"
                                                FilterType="Numbers" ValidChars=".," runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="modal-wide" id="pnlModalDemo" style="display: none;">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h class="modal-title">
                                            แก้ไขรายละเอียดค่าใช้จ่าย</h>
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
                                                <asp:TemplateField HeaderText="ผู้ชำระ" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litPAYER_NAME" runat="server" Text='<%# Eval("PAYER_NAME")%>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <%--                                               <asp:TemplateField HeaderText="หน่วย" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Literal ID="litRATES_GROUP_DETAIL_UNIT" runat="server" Text='<%# Eval("RATE_UNIT")%>' />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ราคาต่อหน่วย" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Literal ID="litRATE_AMOUNT" runat="server" Text='<%# Eval("RATE_AMOUNT","{0:N2}")%>' />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
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
                                                        <%--      <asp:LinkButton ID="btnDelete" runat="server" ToolTip="Delete" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                                        CommandArgument='<%# Eval("ID")%>'><i class="fa fa-trash"></i></asp:LinkButton>--%>
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


                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="row">
                                            <div class="col-md-offset-3 col-md-9">
                                                <asp:Button ID="btnSave" runat="server" class="btn green" Text="Submit" OnClick="btnSave_Click" />
                                                <%--                                                <asp:Button ID="btnPrintInvoice" runat="server" class="btn green" Text="พิมพ์ใบแจ้งหนี้" OnClick="btnPrintInvoice_Click" />--%>
                                                <asp:LinkButton ID="btnCancel" runat="server" class="btn default" OnClick="btnCancel_Click"> Cancel</asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>

                    </div>
                    <!--/row-->


                </div>
                <!-- END FORM-->
            </div>
        </div>

        <%--</ContentTemplate>
        </asp:UpdatePanel>--%>
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

        });
    </script>
    <!-- END JAVASCRIPTS -->
</asp:Content>
