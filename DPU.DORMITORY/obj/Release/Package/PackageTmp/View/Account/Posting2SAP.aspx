<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Posting2SAP.aspx.cs" Inherits="DPU.DORMITORY.Web.View.Account.Posting2SAP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="Form1" method="post" runat="server" class="form-horizontal">

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

                                    <asp:BoundField HeaderText="ประจำเดือน" DataField="POSTING_DATE" ItemStyle-HorizontalAlign="Left" SortExpression="POSTING_DATE" DataFormatString="{0:MM/yyyy}" />
                                    <asp:BoundField HeaderText="Company" DataField="COMPANY" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="BA" DataField="BA" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Profit Ctr." DataField="PROFIT_CTR" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Main trans" DataField="MAIN_TRANS" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Sub trans" DataField="SUB_TRANS" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Service" DataField="SERVICE" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="ผู้ชำระ" DataField="PAY_BY" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="BP No." DataField="BP_NO" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Amout" DataField="AMOUT" ItemStyle-HorizontalAlign="Left" />
                                </Columns>

                                <EmptyDataTemplate>
                                    <div class="data-not-found">
                                        <asp:Literal ID="libDataNotFound" runat="server" Text="Data Not found" />
                                    </div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                            <%--<asp:GridView ID="gvResult" runat="server" AutoGenerateColumns="False" AllowPaging="False"
                            CssClass="table table-striped table-hover table-bordered" ShowHeaderWhenEmpty="True" DataKeyNames="ID" OnRowCommand="gvResult_RowCommand" OnRowDataBound="gvResult_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="หอพัก" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Literal ID="litBuild" runat="server" Text='<%# Eval("BUILD")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="ประจำเดือน" DataField="POSTING_DATE" ItemStyle-HorizontalAlign="Left" SortExpression="POSTING_DATE" DataFormatString="{0:MM-yyyy}" />
                                <asp:TemplateField HeaderText="จำนวนข้อมูลทั้งหมด (Records)" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Literal ID="litTotalRecord" runat="server" Text='<%# Eval("TOTAL_ROW")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="โอนสำเร็จ (Records)" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Literal ID="litSuccessRecord" runat="server" Text='<%# Eval("COMPLETE_ROW")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="โอนไม่สำเร็จ (Records)" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Literal ID="litFailRecord" runat="server" Text='<%# Eval("ERROR_ROW")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Result" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Literal ID="litResult" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnInfo" runat="server" ToolTip="Info" CommandName="View" CommandArgument='<%# Eval("ID")%>'><i class="fa fa-search-plus"></i></asp:LinkButton>
                                        <asp:LinkButton ID="btnSend2SAP" runat="server" ToolTip="Info" CommandName="Send2SAP" CommandArgument='<%# Eval("ID")%>'><i class="fa  fa-cogs"></i></asp:LinkButton>
                                        <asp:LinkButton ID="btnLog" runat="server" ToolTip="Info" CommandName="ViewLogs" CommandArgument='<%# Eval("ID")%>'><i class="fa  fa-code"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <div class="data-not-found">
                                    <asp:Literal ID="libDataNotFound" runat="server" Text="Data Not found" />
                                </div>
                            </EmptyDataTemplate>
                        </asp:GridView>--%>
                        </div>
                    </div>
                    <!-- END EXAMPLE TABLE PORTLET-->
                </asp:Panel>
                <asp:Panel ID="pTransferResult" runat="server">
                </asp:Panel>
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
