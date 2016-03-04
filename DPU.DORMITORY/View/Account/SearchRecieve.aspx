<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SearchRecieve.aspx.cs" Inherits="DPU.DORMITORY.View.Account.SearchRecieve" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="Form1" method="post" runat="server" class="form-horizontal">
        <asp:ToolkitScriptManager ID="ToolkitScript1" runat="server" />

        <!-- BEGIN PAGE CONTENT-->
        <div class="row">
            <div class="col-md-12">
                <!-- BEGIN EXAMPLE TABLE PORTLET-->
                <div class="portlet box blue-madison">
                    <div class="portlet-title">
                        <div class="caption">
                            <i class="fa fa-cogs"></i>รับชำระ
                        </div>
                        <div class="actions">
                        </div>
                    </div>
                    <div class="portlet-body">
                        <div class="portlet-body">

                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label col-md-3">อาคาร:</label>
                                        <div class="col-md-6">
                                            <div class="form-group" style="text-align: left">
                                                <asp:DropDownList ID="ddlBuild" runat="server" class="select2_category form-control" DataTextField="NAME" DataValueField="ID" AutoPostBack="True"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label col-md-3">ห้อง:</label>
                                        <div class="col-md-6">
                                            <div class="form-group" style="text-align: left">
                                                <asp:TextBox ID="txtRoomNum" runat="server" Text='<%# Eval("AMOUNT")%>' CssClass="form-control"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtenderReceiptinAc" TargetControlID="txtRoomNum"
                                                    FilterType="Numbers" ValidChars=".," runat="server" />
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
                                                <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" placeholder="" MaxLength="50"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label col-md-3">นามสกุล:</label>
                                        <div class="col-md-6">
                                            <div class="form-group" style="text-align: left">
                                                <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" placeholder="" MaxLength="50"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label col-md-3">เลขที่ใบแจ้งหนี้:</label>
                                        <div class="col-md-6">
                                            <div class="form-group" style="text-align: left">
                                                <asp:TextBox ID="txtSapDocNo" runat="server" CssClass="form-control" placeholder="" MaxLength="50"></asp:TextBox>
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
                            <asp:GridView ID="gvResult" runat="server" AutoGenerateColumns="False" AllowPaging="False"
                                CssClass="table table-striped table-hover table-bordered" ShowHeaderWhenEmpty="True" DataKeyNames="ID" OnRowCommand="gvResult_RowCommand" OnRowDataBound="gvResult_RowDataBound">
                                <Columns>
                                    <asp:BoundField HeaderText="อาคาร" DataField="BUILD_NAME" ItemStyle-HorizontalAlign="Left" SortExpression="BUILD_NAME" />
                                    <asp:BoundField HeaderText="ห้อง" DataField="ROOM_NUMBER" ItemStyle-HorizontalAlign="Left" SortExpression="ROOM_NUMBER" />

                                    <asp:BoundField HeaderText="ประจำเดือน" DataField="POSTING_DATE" ItemStyle-HorizontalAlign="Left" SortExpression="POSTING_DATE" DataFormatString="{0:MM/yyyy}" />
                                    <asp:TemplateField HeaderText="เลขที่ใบแจ้งหนี้" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Literal ID="litSapDocNo" runat="server" Text='<%# Eval("SAP_DOCNO")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="รหัส นศ./เลขห้อง" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Literal ID="litRoomNumber" runat="server" Text='<%# Eval("REF_DESC")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="จำนวนเงิน" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Literal ID="litAmount" runat="server" Text='<%# Eval("AMOUNT")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="ชื่อ" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Literal ID="litFirstName" runat="server" Text='<%# Eval("FIRSTNAME")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="นามสกุล" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Literal ID="litSurname" runat="server" Text='<%# Eval("SURNAME")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <%--                         <asp:TemplateField HeaderText="เบอร์ติดต่อ" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Literal ID="litPhone" runat="server" Text='<%# Eval("PHONE")%>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="สถานะ" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Literal ID="litPaymentStatus" runat="server" Text='<%# Eval("PAYMENT_STATUS")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnPay" runat="server" ToolTip="ชำระเงิน" CommandName="Payment" OnClientClick="return confirm('เปลี่ยนรายการที่เลือกให้เป็น ชำระเงินแล้ว?');" CommandArgument='<%# Eval("ID")%>'><i class="fa  fa-credit-card"></i></asp:LinkButton>
                                            <asp:LinkButton ID="btnPrint" runat="server" ToolTip="พิมพ์ใบแจ้งหนี้" CommandName="PrintInvoice" OnClientClick="return confirm('ต้องการพิมพ์ใบแจ้งหนี้?');" CommandArgument='<%# Eval("ID")%>'><i class="fa  fa-credit-card"></i></asp:LinkButton>

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
        </div>
        <!-- END PAGE CONTENT-->

        
        <div class="modal-wide" id="pnlModalDemo" style="display: none;">
            <div class="modal-content">
                <div class="modal-header">
                    <div class="modal-title">
                        ยืนยัน
                    </div>
                </div>
                <div class="modal-body" style="width: 600px; height: 400px; overflow-x: hidden; overflow-y: scroll; padding-bottom: 10px;">
                    <h3>คุณต้องการพิมพ์ใบแจ้งหนี้</h3>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="OK" runat="server" CssClass="btn purple" Style="margin-top: 10px; margin-left: 20px;" Text="พิมพ์ใบแจ้งหนี้" OnClick="OK_Click" />
                    <asp:Button ID="btnClose" CssClass="btn default" Style="margin-top: 10px;" runat="server" Text="ปิด" />
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
                "pageLength": 50,
                "searching": false,
                "paging": true
            });

            $('.select2_category').select2({
                placeholder: "Select an option",
                allowClear: true
            });

        });
    </script>
    <!-- END JAVASCRIPTS -->
</asp:Content>
