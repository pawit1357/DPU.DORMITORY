<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SearchRoomForRent.aspx.cs" Inherits="DPU.DORMITORY.Web.View.Management.SearchRoomForRent" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="Form1" method="post" runat="server" class="form-horizontal">
        <asp:ToolkitScriptManager ID="ToolkitScript1" runat="server" />

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
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">อาคาร:</label>
                                <div class="col-md-6">
                                    <div class="form-group" style="text-align: left">
                                        <asp:DropDownList ID="ddlBuild" runat="server" class="select2_category form-control" DataTextField="NAME" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="ddlBuild_SelectedIndexChanged"></asp:DropDownList>
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
                                        <%--                                        <asp:DropDownList ID="ddlRoom" runat="server" class="select2_category form-control" DataTextField="NUMBER" DataValueField="ID"></asp:DropDownList>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">สถานะห้อง:</label>
                                <div class="col-md-6">
                                    <div class="form-group" style="text-align: left">
                                        <asp:DropDownList ID="ddlStatus" runat="server" class="select2_category form-control" DataTextField="NAME" DataValueField="ID"></asp:DropDownList>
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
        <!-- BEGIN PAGE CONTENT-->
        <asp:Panel ID="pSearchResult" runat="server">
            <div class="row">
                <div class="col-md-12">
                    <!-- BEGIN EXAMPLE TABLE PORTLET-->
                    <div class="portlet box blue-madison">
                        <div class="portlet-title">
                            <div class="caption">
                                <i class="fa fa-cogs"></i>ค้นหาห้องพัก
                            </div>
                            <div class="actions">
                                <%--<asp:LinkButton ID="lbAdd" runat="server" class="btn btn-default btn-sm" OnClick="lbAdd_Click"><i class="icon-pencil"></i> Add</asp:LinkButton>--%>
                            </div>
                        </div>
                        <div class="portlet-body">
                            <asp:GridView ID="gvResult" runat="server" AutoGenerateColumns="False"
                                CssClass="table table-striped table-hover table-bordered" ShowHeaderWhenEmpty="True" DataKeyNames="ID" OnRowCommand="gvResult_RowCommand" OnRowDataBound="gvResult_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="ลำดับ" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField HeaderText="ID" DataField="ID" ItemStyle-HorizontalAlign="Left" SortExpression="ID" />--%>
                                    <asp:BoundField HeaderText="อาคาร" DataField="BUILD" ItemStyle-HorizontalAlign="Left" SortExpression="BUILD" />
                                    <asp:BoundField HeaderText="ชั้น" DataField="FLOOR" ItemStyle-HorizontalAlign="Left" SortExpression="FLOOR" />
                                    <asp:BoundField HeaderText="เลขห้อง" DataField="NUMBER" ItemStyle-HorizontalAlign="Left" SortExpression="NUMBER" />
                                    <asp:BoundField HeaderText="ประเภทห้อง" DataField="ROOM_TYPE" ItemStyle-HorizontalAlign="Left" SortExpression="ROOM_TYPE" />
                                    <asp:BoundField HeaderText="เงินประกัน" DataField="INSURANCE_AMOUNT" ItemStyle-HorizontalAlign="Left" SortExpression="INSURANCE_AMOUNT" />

                                    <asp:BoundField HeaderText="ค่าเช่าห้อง" DataField="AMOUNT" ItemStyle-HorizontalAlign="Left" SortExpression="AMOUNT" />
                                    <%--<asp:BoundField HeaderText="วันที่เข้าพัก" DataField="RATE_GROUP" ItemStyle-HorizontalAlign="Left" SortExpression="RATE_GROUP" />--%>
                                    <asp:BoundField HeaderText="จำนวนผู้พัก" DataField="CNT" ItemStyle-HorizontalAlign="Center" SortExpression="CNT" />

                                    <asp:TemplateField HeaderText="สถานะห้อง" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Literal ID="litRoomStatus" runat="server" Text='<%# Eval("STATUS")%>' />
                                            <asp:HiddenField ID="hCnt" runat="server" Value='<%# Eval("CNT")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="จำนวนคนต่อห้อง" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Literal ID="litCustomerLimit" runat="server" Text='<%# Eval("CUSTOMER_LIMIT")%>' />
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnView" runat="server" ToolTip="View" CommandName="View" CommandArgument='<%# String.Concat(Eval("ID"),DPU.DORMITORY.Biz.Constants.CHAR_COMMA,Eval("CNT"))%>'><i class="fa  fa-search-plus"></i></asp:LinkButton>

                                            <asp:LinkButton ID="btnCheckIn" runat="server" ToolTip="Check In" CommandName="CheckIn" CommandArgument='<%# String.Concat(Eval("ID"),DPU.DORMITORY.Biz.Constants.CHAR_COMMA,Eval("CNT"))%>'><i class="fa fa-suitcase"></i></asp:LinkButton>

                                            <%--<asp:LinkButton ID="btnReserv" runat="server" ToolTip="Reserv" CommandName="Reserv" CommandArgument='<%# String.Concat(Eval("ID"),DPU.DORMITORY.Biz.Constants.CHAR_COMMA,Eval("CNT"))%>' OnClientClick="return confirm('ทำรายการจอง');"><i class="fa fa-ticket"></i></asp:LinkButton>--%>

                                            <asp:LinkButton ID="btnRepairRoom" runat="server" ToolTip="ทำความสะอาด" CommandName="RepairRoom" CommandArgument='<%# String.Concat(Eval("ID"),DPU.DORMITORY.Biz.Constants.CHAR_COMMA,Eval("CNT"))%>' OnClientClick="return confirm('เปลี่ยนสถานะห้องเป็นทำความสะอาด');"><i class="fa fa-trash-o"></i></asp:LinkButton>

                                            <asp:LinkButton ID="btnUndo" runat="server" ToolTip="ยกเลิก" CommandName="UndoRepair" CommandArgument='<%# String.Concat(Eval("ID"),DPU.DORMITORY.Biz.Constants.CHAR_COMMA,Eval("CNT"))%>' OnClientClick="return confirm('ยกเลิกสถานะห้อง');"><i class="fa fa-undo"></i></asp:LinkButton>
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
        </asp:Panel>
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

</asp:Content>
