<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CheckIn.aspx.cs" Inherits="DPU.DORMITORY.Web.View.Management.CheckIn" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Reference Page="~/View/Management/SearchCustomer.aspx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <form id="Form1" method="post" runat="server" class="form-horizontal">
        <asp:ToolkitScriptManager ID="ToolkitScript1" runat="server" />

        <div class="portlet light bordered">
            <div class="portlet-title">
                <div class="caption">
                    <i class="icon-equalizer font-red-sunglo"></i>
                    <span class="caption-subject font-red-sunglo bold uppercase">ดำเนินการ&nbsp;[<asp:Label ID="lbCommandName" runat="server" Text=""></asp:Label>]&nbsp;</span>
                    <span class="caption-helper">กรอกข้อมูลให้ครบถ้วนในช่อง *</span>
                </div>
                <div class="tools">
                </div>
            </div>
            <div class="portlet-body form">
                <!-- BEGIN FORM-->
                <div class="form-body">
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
                            <li class="<%=pActive01 %>" runat="server" id="idAc01">
                                <asp:LinkButton ID="btnMainData" runat="server" OnClick="btnMainData_Click">ข้อมูลผู้พัก</asp:LinkButton>
                            </li>
                            <li class="<%=pActive02 %>" runat="server" id="idAc02">
                                <asp:LinkButton ID="btnOwner" runat="server" OnClick="btnMainData_Click">ผู้สนับสนุน ( Sponsor )</asp:LinkButton>
                            </li>
                            <li class="<%=pActive03 %>" runat="server" id="idAc03">
                                <asp:LinkButton ID="btnPaymentHistory" runat="server" OnClick="btnMainData_Click">ยอดค้างชำระ</asp:LinkButton>
                            </li>
                            <li class="<%=pActive04 %>" runat="server" id="idAc04">
                                <asp:LinkButton ID="btnInvoice" runat="server" OnClick="btnMainData_Click">ตั้งหนี้</asp:LinkButton>
                            </li>
                        </ul>
                        <div class="tab-content">
                            <div class="tab-pane <%=pActive01 %>" id="tab_0">

                                <asp:Panel ID="pCustomerInfo" runat="server">
                                    <%--                                    <h4 class="form-section">ข้อมูลผู้เช่า</h4>--%>
                                    <!--/row-->
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="control-label col-md-3">ประเภทลูกค้า<span class="required" aria-required="true">* </span></label>
                                                <div class="col-md-9">
                                                    <asp:DropDownList ID="ddlCustomerType" runat="server" class="select2_category form-control" DataTextField="Name" DataValueField="ID"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <!--/span-->
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="control-label col-md-3">
                                                </label>
                                                <div class="radio-list">
                                                    <label class="radio-inline">
                                                        <asp:RadioButton ID="rdHasStdNum" GroupName="Status" runat="server" Checked="true" OnSelectedIndexChanged="ddlCustomerType_SelectedIndexChanged" AutoPostBack="true" OnCheckedChanged="rdHasNotStdNum_CheckedChanged" />มีเลขทะเบียน นศ.
                                                    </label>
                                                    <label class="radio-inline">
                                                        <asp:RadioButton ID="rdHasNotStdNum" GroupName="Status" runat="server" OnSelectedIndexChanged="ddlCustomerType_SelectedIndexChanged" AutoPostBack="true" OnCheckedChanged="rdHasNotStdNum_CheckedChanged" />ไม่มีเลขทะเบียน นศ.
                                                    </label>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="control-label col-md-3">เลขประจำตัว นศ.<span class="required" aria-required="true">* </span></label>
                                                <div class="col-md-9">
                                                    <div class="input-group" style="text-align: left">
                                                        <asp:TextBox ID="txtCustomerID" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                                                        <span class="input-group-btn">
                                                            <asp:LinkButton ID="btnCheckCustomer" runat="server" CssClass="btn green" OnClick="btnCheckCustomer_Click"> Check</asp:LinkButton>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!--/span-->
                                    </div>

                                    <!--/row-->
                                    <div class="row">
                                        <!--/span-->
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="control-label col-md-3">ชื่อ<span class="required" aria-required="true">* </span></label>
                                                <div class="col-md-9">
                                                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="ชื่อ" MaxLength="50"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="control-label col-md-3">นามสกุล<span class="required" aria-required="true">* </span></label>
                                                <div class="col-md-9">
                                                    <asp:TextBox ID="txtSurname" runat="server" CssClass="form-control" placeholder="สกุล" MaxLength="50"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="control-label col-md-3">
                                                </label>
                                                <div class="radio-list">
                                                    <label class="radio-inline">
                                                        <asp:CheckBox ID="cbPayer" runat="server" Checked="true" />ใช้ชื่อเพื่อตั้งหนี้ในระบบ
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="control-label col-md-3">
                                                </label>
                                                <div class="radio-list">
                                                    <label class="radio-inline">
                                                        <asp:CheckBox ID="cbSTayAlone" runat="server" Checked="false" />ต้องการพักคนเดียว
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="control-label col-md-3">เลขที่บัตรประชาชน/เลขที่หนังสือเดินทาง<span class="required" aria-required="true">* </span></label>
                                                <div class="col-md-9">
                                                    <asp:TextBox ID="txtIDCard" runat="server" CssClass="form-control" placeholder="เลขที่บัตรประชาชน/เลขที่หนังสือเดินทาง" MaxLength="13"></asp:TextBox>
                                                    <%--<span class="help-block">เลขที่บัตรประชาชน/เลขที่หนังสือเดินทาง </span>--%>
                                                </div>
                                            </div>
                                        </div>
                                        <!--/span-->
                                    </div>
                                    <asp:Panel ID="pStdInfo" runat="server">
                                        <div class="row">
                                            <!--/span-->
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label class="control-label col-md-3">คณะ</label>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtFaculty" runat="server" CssClass="form-control" placeholder="" MaxLength="50"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label class="control-label col-md-3">สาขา</label>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtMajor" runat="server" CssClass="form-control" placeholder="" MaxLength="50"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <!--/span-->
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label class="control-label col-md-3">หลักสูตร</label>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtProTypeName" runat="server" CssClass="form-control" placeholder="" MaxLength="50"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label class="control-label col-md-3">สถานะ</label>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtStatus" runat="server" CssClass="form-control" placeholder="" MaxLength="50"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <!--/row-->

                                    <h4 class="form-section">ข้อมูลที่อยู่ (สามารถติดต่อได้กรณีฉุกเฉิน)</h4>
                                    <!--/row-->
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="control-label col-md-3">ที่อยู่</label>
                                                <div class="col-md-9">
                                                    <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="control-label col-md-3">ถนน</label>
                                                <div class="col-md-9">
                                                    <asp:TextBox ID="txtRoad" runat="server" CssClass="form-control" placeholder="" MaxLength="50"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!--/row-->
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="control-label col-md-3">ซอย</label>
                                                <div class="col-md-9">
                                                    <asp:TextBox ID="txtSoi" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <!--/span-->
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="control-label col-md-3">ตำบล/แขวง</label>
                                                <div class="col-md-9">
                                                    <asp:TextBox ID="txtTambon" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>

                                                    <%--<asp:DropDownList ID="ddlTambon" runat="server" class="select2_category form-control" DataTextField="Name" DataValueField="ID"></asp:DropDownList>--%>
                                                </div>
                                            </div>
                                        </div>


                                        <!--/span-->
                                    </div>
                                    <!--/span-->

                                    <!--/row-->
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="control-label col-md-3">อำเภอ/เขต</label>
                                                <div class="col-md-9">
                                                    <asp:TextBox ID="txtAmphur" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>

                                                    <%--<asp:DropDownList ID="ddlAmphur" runat="server" class="select2_category form-control" DataTextField="Name" DataValueField="ID"></asp:DropDownList>--%>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="control-label col-md-3">จังหวัด</label>
                                                <div class="col-md-9">
                                                    <asp:TextBox ID="txtPronvice" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>

                                                    <%--<asp:DropDownList ID="ddlProvince" runat="server" class="select2_category form-control" DataTextField="Name" DataValueField="ID"></asp:DropDownList>--%>
                                                </div>
                                            </div>
                                        </div>
                                        <!--/span-->
                                    </div>
                                    <div class="row">
                                    </div>
                                    <!--/row-->
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="control-label col-md-3">รหัสไปรษณีย์</label>
                                                <div class="col-md-9">
                                                    <asp:TextBox ID="txtZipcode" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="control-label col-md-3">เบอร์ติดต่อ<span class="required" aria-required="true">* </span></label>
                                                <div class="col-md-9">
                                                    <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <!--/span-->
                                    </div>
                                    <!--/row-->
                                    <div class="row">

                                        <!--/span-->
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="control-label col-md-3">เชื้อชาติ<span class="required" aria-required="true">* </span></label>
                                                <div class="col-md-9">
                                                    <asp:DropDownList ID="ddlNation" runat="server" class="select2_category form-control" DataTextField="Name" DataValueField="ID"></asp:DropDownList>

                                                </div>
                                            </div>
                                        </div>
                                        <!--/span-->
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pRoomInfo" runat="server">
                                    <!--/row-->
                                    <h4 class="form-section">ข้อมูลห้องพัก</h4>
                                    <!--/row-->
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:Label ID="lbDate" runat="server" CssClass="control-label col-md-3" Text="วันที่เข้าพัก"><span class="required" aria-required="true">* </span></asp:Label>
                                                <div class="col-md-9">
                                                    <div class="input-group input-medium date date-picker" data-date="10/2012" data-date-format="dd/mm/yyyy" data-date-viewmode="years" data-date-minviewmode="months">
                                                        <asp:TextBox ID="txtChckInDate" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <span class="input-group-btn">
                                                            <button class="btn default" type="button"><i class="fa fa-calendar"></i></button>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!--/row-->
                                    <div class="row">
                                        <!--/span-->
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="control-label col-md-3">ห้อง<span class="required" aria-required="true">* </span></label>
                                                <div class="col-md-9">
                                                    <div class="input-group" style="text-align: left">
                                                        <asp:TextBox ID="txtRoom" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:HiddenField ID="hRoomID" runat="server" Value="0" />
                                                        <span class="input-group-btn" runat="server" id="spanPoup">
                                                            <a class="btn green" href="#PopupRoom" data-toggle="modal" runat="server" id="aPopupRoom">เลือกห้อง</a>
                                                            <a class="btn red" href="#PopupRoomInfo" data-toggle="modal" runat="server" id="a1">แสดงรายละเอียดของห้อง</a>

                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!--/span-->
                                    </div>
                                    <!--/row-->
                                </asp:Panel>
                                <asp:Panel ID="pMoveRoom" runat="server">
                                    <div class="row">
                                        <!--/span-->
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="control-label col-md-3">ย้ายไปห้อง<span class="required" aria-required="true">* </span></label>
                                                <div class="col-md-9">
                                                    <div class="input-group" style="text-align: left">
                                                        <asp:TextBox ID="txtMoveTo" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:HiddenField ID="hRoomMoveRoomID" runat="server" Value="0" />
                                                        <span class="input-group-btn">
                                                            <asp:LinkButton ID="btnRoom" runat="server" OnClick="btnRoom_Click" CssClass="btn green">เลือกห้อง</asp:LinkButton>
                                                            <%--                                                            <a class="btn green" href="#PopupRoom" data-toggle="modal">เลือกห้อง</a>--%>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!--/span-->
                                    </div>
                                </asp:Panel>
                            </div>
                            <div class="tab-pane <%=pActive02 %>" id="tab_1">
                                <asp:GridView ID="gvRespPayment" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-striped table-hover table-bordered" ShowHeaderWhenEmpty="True" DataKeyNames="ID" OnRowCancelingEdit="gvRespPayment_RowCancelingEdit" OnRowCommand="gvResult_RowCommand" OnRowDataBound="gvRespPayment_RowDataBound" OnRowDeleting="gvRespPayment_RowDeleting" OnRowEditing="gvRespPayment_RowEditing" OnRowUpdating="gvRespPayment_RowUpdating" ShowFooter="True" OnDataBound="gvRespPayment_DataBound" OnRowCreated="gvRespPayment_RowCreated">
                                    <Columns>
                                        <asp:TemplateField HeaderText="ลำดับ" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lable1" runat="server" Text="เพิ่มรายการ"></asp:Label>
                                            </FooterTemplate>

                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="รายการ" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Literal ID="litSerivce" runat="server" Text='<%# Eval("SERVICE_NAME")%>' />
                                                <asp:HiddenField ID="hSERVICE_ID" Value='<%# Eval("COST_TYPE_ID")%>' runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlService" runat="server" CssClass="select2_category form-control" DataTextField="NAME" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="ddlService_SelectedIndexChanged"></asp:DropDownList>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ค่าใช้จ่าย" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Literal ID="litRoomRate" runat="server" Text='<%# Eval("ROOM_RATE")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ผู้ชำระ" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="litSponsor" runat="server" Text='<%# Eval("SPONSOR_NAME")%>' />
                                                <asp:HiddenField ID="hSPONSOR_ID" Value='<%# Eval("SPONSOR_ID")%>' runat="server" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlSponsor" runat="server" CssClass="select2_category form-control" DataTextField="NAME" DataValueField="ID"></asp:DropDownList>
                                                <asp:HiddenField ID="hSPONSOR_ID" Value='<%# Eval("SPONSOR_ID")%>' runat="server" />

                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlSponsor" runat="server" CssClass="select2_category form-control" DataTextField="NAME" DataValueField="ID"></asp:DropDownList>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="เงื่อนไข" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="litTermOfPayment" runat="server" Text='<%# Eval("TERM_OF_PAYMENT_NAME")%>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:HiddenField ID="hTEAM_OF_PAYMENT_ID" Value='<%# Eval("TERM_OF_PAYMENT_ID")%>' runat="server" />
                                                <asp:DropDownList ID="ddlTermOfPayment" runat="server" CssClass="select2_category form-control" DataTextField="NAME" DataValueField="ID"></asp:DropDownList>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlTermOfPayment" runat="server" CssClass="select2_category form-control" DataTextField="NAME" DataValueField="ID"></asp:DropDownList>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="จำนวนเงินสนับสุน" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="litAmout" runat="server" Text='<%# Eval("AMOUNT")%>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtAmout" runat="server" Text='<%# Eval("AMOUNT")%>' CssClass="form-control"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtenderReceiptinAc" TargetControlID="txtAmout"
                                                    FilterType="Numbers" ValidChars=".," runat="server" />

                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtAmout" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtenderReceiptinAc" TargetControlID="txtAmout"
                                                    FilterType="Numbers" ValidChars=".," runat="server" />
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" runat="server" ToolTip="Edit" CommandName="Edit" CommandArgument='<%# Eval("ID")%>'><i class="fa fa-edit"></i></asp:LinkButton>
                                                <asp:LinkButton ID="btnDelete" runat="server" ToolTip="Delete" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this record?');" CommandArgument='<%# Eval("ID")%>'><i class="fa fa-trash"></i></asp:LinkButton>

                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="btnUpdate" runat="server" ToolTip="Update" ValidationGroup="CreditLineGrid"
                                                    CommandName="Update"><i class="fa fa-save"></i></asp:LinkButton>
                                                <asp:LinkButton ID="LinkCancel" runat="server" ToolTip="Cancel" CausesValidation="false"
                                                    CommandName="Cancel"><i class="fa fa-remove"></i></asp:LinkButton>
                                            </EditItemTemplate>

                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbAdd" runat="server" class="btn btn-default btn-sm" OnClick="lbAdd_Click" CommandName="Add"><i class="icon-plus"></i> เพิ่มรายการ</asp:LinkButton>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                    </Columns>

                                    <EmptyDataTemplate>
                                        <div class="data-not-found">
                                            <asp:Literal ID="libDataNotFound" runat="server" Text="Data Not found" />
                                        </div>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                            <div class="tab-pane <%=pActive03 %>" id="tab_2">
                                <asp:GridView ID="gvPaymentHistory" runat="server" AutoGenerateColumns="False" AllowPaging="False"
                                    CssClass="table table-striped table-hover table-bordered" ShowHeaderWhenEmpty="True" DataKeyNames="ID" OnRowCommand="gvPaymentHistory_RowCommand" OnRowDataBound="gvPaymentHistory_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="ลำดับ" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="เดือน" DataField="POSTING_DATE" ItemStyle-HorizontalAlign="Left" SortExpression="POSTING_DATE" DataFormatString="{0:MM/yyyy}" />
                                        <asp:TemplateField HeaderText="เลขที่ใบแจ้งหนี้" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="litSapDocNo" runat="server" Text='<%# Eval("SAP_DOCNO")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <%--                            <asp:TemplateField HeaderText="รหัส นศ./เลขห้อง" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="litRoomNumber" runat="server" Text='<%# Eval("REF_DESC")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>--%>
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
                                        <asp:TemplateField HeaderText="สถานะ" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="litPaymentStatus" runat="server" Text='<%# Eval("PAYMENT_STATUS")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnPrintRecieve" runat="server" ToolTip="Print Invoice" CommandName="PrintInvoice" CommandArgument='<%# Eval("ID")%>'><i class="fa  fa-print"></i></asp:LinkButton>
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
                            <div class="tab-pane <%=pActive04 %>" id="tab_3">
                                <%--                            <div class="row">
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
                            </div>--%>
                                <%--<div class="row">
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
                                                    <asp:TextBox ID="txtElecMeterStart" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtElecMeterEnd" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:Label ID="lbElecUnit" runat="server" Text="0"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>ค่าน้ำ</td>
                                                <td>
                                                    <asp:TextBox ID="txtWaterMeterStart" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtWaterMeterEnd" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:Label ID="lbWaterUnit" runat="server" Text="0"></asp:Label>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>--%>
                                <asp:GridView ID="gvInvoiceDetail" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-striped table-hover table-bordered" ShowHeaderWhenEmpty="True" DataKeyNames="ID,row_type" OnRowDataBound="gvInvoiceDetail_RowDataBound" OnRowEditing="gvInvoiceDetail_RowEditing" OnRowUpdating="gvInvoiceDetail_RowUpdating" OnRowCancelingEdit="gvInvoiceDetail_RowCancelingEdit" OnRowDeleting="gvInvoiceDetail_RowDeleting">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No." ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="รายการค่าใช้จ่าย" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="litM_SERVICE_NAME" runat="server" Text='<%# Eval("M_SERVICE_NAME")%>' />
                                                <%--                                                    <asp:HiddenField runat="server" ID="hServiceID" Value='<%# Eval("M_SERVICE_ID")%>'></asp:HiddenField>--%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ผู้ชำระ" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="litPAYER_NAME" runat="server" Text='<%# Eval("PAYER_NAME")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="หน่วย" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="litRATES_GROUP_DETAIL_UNIT" runat="server" Text='<%# Eval("RATE_UNIT")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ราคาต่อหน่วย" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="litRATE_AMOUNT" runat="server" Text='<%# Eval("RATE_AMOUNT","{0:N2}")%>' />
                                            </ItemTemplate>
                                            <%--       <EditItemTemplate>
                                                    <asp:TextBox ID="txtRATE_AMOUNT" runat="server" Text='<%# Eval("RATE_AMOUNT","{0:N2}")%>' class="form-control"></asp:TextBox>
                                                </EditItemTemplate>--%>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="จำนวนเงินที่ชำระ" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="litPAY_AMOUNT" runat="server" Text='<%# Eval("PAYMENT_AMOUNT","{0:N2}")%>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtPAY_AMOUNT" runat="server" Text='<%# Eval("PAYMENT_AMOUNT","{0:N2}")%>' class="form-control"></asp:TextBox>
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
                        </div>
                    </div>


                    <div class="modal-wide" id="pnlModalDemo" style="display: none;">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h class="modal-title">ข้อมูลห้องพัก</h>
                            </div>
                            <div class="modal-body" style="width: 800px; height: 400px; overflow-x: hidden; overflow-y: scroll; padding-bottom: 10px;">


                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="control-label col-md-3">อาคาร:</label>
                                            <div class="col-md-6">
                                                <div class="form-group" style="text-align: left">
                                                    <asp:DropDownList ID="ddlBuild" runat="server" DataTextField="NAME" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="ddlBuild_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="control-label col-md-3">ห้อง:</label>
                                            <div class="col-md-6">
                                                <div class="form-group" style="text-align: left">
                                                    <asp:DropDownList ID="ddlRoom" runat="server" DataTextField="NUMBER" DataValueField="ID"></asp:DropDownList>
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
                                                    <asp:LinkButton ID="btnCancelSearch" runat="server" class="btn default" OnClick="btnCancelSearch_Click"><i class="icon-refresh"></i> Cancel</asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                        </div>
                                    </div>
                                </div>


                                <asp:GridView ID="gvResult" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-striped table-hover table-bordered" ShowHeaderWhenEmpty="True" DataKeyNames="ID" OnRowCommand="gvResult_RowCommand">
                                    <Columns>
                                        <%--<asp:BoundField HeaderText="ID" DataField="ID" ItemStyle-HorizontalAlign="Left" SortExpression="ID" />--%>
                                        <asp:BoundField HeaderText="อาคาร" DataField="BUILD" ItemStyle-HorizontalAlign="Left" SortExpression="BUILD" />
                                        <asp:BoundField HeaderText="ประเภทห้อง" DataField="ROOM_TYPE" ItemStyle-HorizontalAlign="Left" SortExpression="ROOM_TYPE" />
                                        <%--                                <asp:BoundField HeaderText="กลุ่มอัตราค่าห้อง" DataField="RATE_GROUP" ItemStyle-HorizontalAlign="Left" SortExpression="RATE_GROUP" />--%>
                                        <asp:BoundField HeaderText="ชั้น" DataField="FLOOR" ItemStyle-HorizontalAlign="Left" SortExpression="FLOOR" />
                                        <asp:BoundField HeaderText="เลขห้อง" DataField="NUMBER" ItemStyle-HorizontalAlign="Left" SortExpression="NUMBER" />

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnSelect" runat="server" ToolTip="Select" CommandName="Select" CommandArgument='<%# Eval("ID")%>'><i class="fa  fa-check-square-o"></i></asp:LinkButton>
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
                            <div class="modal-footer">
                                <asp:Button ID="btnClose" CssClass="btn default" Style="margin-top: 10px;" runat="server" Text="ปิด" />
                            </div>
                        </div>
                    </div>
                    <asp:LinkButton ID="lnkFake" runat="server"> </asp:LinkButton>
                    <asp:ModalPopupExtender ID="ModolPopupExtender" runat="server" PopupControlID="pnlModalDemo"
                        TargetControlID="lnkFake" BackgroundCssClass="modal-backdrop modal-print-form fade in" BehaviorID="mpModalDemo"
                        CancelControlID="btnClose">
                    </asp:ModalPopupExtender>

                    <!--/row-->
                    <div class="form-actions">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="btnSave" runat="server" class="btn green" Text="Save" OnClick="btnSave_Click" />
                                        <%--<asp:Button ID="btnPrintInvoice" runat="server" class="btn blue" Text="พิมพ์ใบแจ้งหนี้" OnClick="btnPrintInvoice_Click" />--%>
                                        <asp:LinkButton ID="btnCancel" runat="server" class="btn default" OnClick="btnCancel_Click"> Cancel</asp:LinkButton>

                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                            </div>
                        </div>
                    </div>
                </div>
                <!-- END FORM-->
            </div>
        </div>



        <div class="modal fade" id="PopupRoomInfo" tabindex="-1" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                        <h4 class="modal-title">รายการค่าใช้จ่าย</h4>
                    </div>
                    <div class="modal-body">

                        <asp:GridView ID="gvRoomInfo" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                            CssClass="table table-striped table-hover table-bordered" ShowHeaderWhenEmpty="True" DataKeyNames="ID">
                            <Columns>
                                <%--                <asp:TemplateField HeaderText="รายการค่าใช้จ่าย" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Literal ID="litServiceID" runat="server" Text='<%# Eval("SERVICE_NAME")%>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>--%>
                                <%--<asp:TemplateField HeaderText="Subtran" ItemStyle-HorizontalAlign="Center">
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
                                </asp:TemplateField>--%>
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
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" ToolTip="Edit" CommandName="Edit" CommandArgument='<%# Eval("ID")%>'><i class="fa fa-edit"></i></asp:LinkButton>
                                        <asp:LinkButton ID="btnDelete" runat="server" ToolTip="Delete" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                            CommandArgument='<%# Eval("ID")%>'><i class="fa fa-trash-o"></i></asp:LinkButton>
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
                        <%--<button type="button" id="btnSelectCompany" class="btn blue" data-dismiss="modal">OK</button>--%>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
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
                "pageLength": 50,
                "searching": false,
                "paging": false
            });

            //openModal();
            var form1 = $('#Form1');
            var error1 = $('.alert-danger', form1);
            var success1 = $('.alert-success', form1);

            form1.validate({
                errorElement: 'span', //default input error message container
                errorClass: 'help-block help-block-error', // default input error message class
                focusInvalid: false, // do not focus the last invalid input
                ignore: "",  // validate all fields including form hidden input
                messages: {
                    select_multi: {
                        maxlength: jQuery.validator.format("Max {0} items allowed for selection"),
                        minlength: jQuery.validator.format("At least {0} items must be selected")
                    }
                },
                rules: {

                    //ctl00$ContentPlaceHolder1$txtCustomerID: {
                    //required: true,
                    //    number: true
                    //},
                    ctl00$ContentPlaceHolder1$ddlCustomerType: {
                        required: true,
                        number: true
                    },
                    ctl00$ContentPlaceHolder1$txtName: {
                        required: true
                    },
                    ctl00$ContentPlaceHolder1$txtIDCard: {
                        required: true
                        //number: true
                    },
                    ctl00$ContentPlaceHolder1$ddlCustomerType: {
                        required: true
                    },
                    //ctl00$ContentPlaceHolder1$txtAddress: {
                    //    required: true
                    //},
                    //ctl00$ContentPlaceHolder1$txtRoad: {
                    //    required: true
                    //},
                    //ctl00$ContentPlaceHolder1$txtSoi: {
                    //    required: true
                    //},
                    //ctl00$ContentPlaceHolder1$ddlTambon: {
                    //    required: true
                    //},
                    //ctl00$ContentPlaceHolder1$ddlAmphur: {
                    //    required: true
                    //},
                    //ctl00$ContentPlaceHolder1$ddlProvince: {
                    //    required: true
                    //},
                    //ctl00$ContentPlaceHolder1$txtTel: {
                    //    required: true
                    //},
                    //ctl00$ContentPlaceHolder1$txtZipcode: {
                    //    required: true
                    //},
                    ctl00$ContentPlaceHolder1$ddlNation: {
                        required: true
                    },
                    //ctl00$ContentPlaceHolder1$txtRemark: {
                    //    required: true
                    //},
                    ctl00$ContentPlaceHolder1$txtPhone: {
                        required: true
                    },
                    ctl00$ContentPlaceHolder1$txtChckInDate: {
                        required: true
                    },
                    ctl00$ContentPlaceHolder1$txtRoom: {
                        required: true
                    },








                },

                invalidHandler: function (event, validator) { //display error alert on form submit              
                    success1.hide();
                    error1.show();
                    Metronic.scrollTo(error1, -200);
                },

                highlight: function (element) { // hightlight error inputs
                    $(element)
                        .closest('.form-group').addClass('has-error'); // set error class to the control group
                },

                unhighlight: function (element) { // revert the change done by hightlight
                    $(element)
                        .closest('.form-group').removeClass('has-error'); // set error class to the control group
                },

                success: function (label) {
                    label
                        .closest('.form-group').removeClass('has-error'); // set success class to the control group
                },

                submitHandler: function (form) {
                    form.submit();
                }
            });

        });

        function openModal() {
            alert('');
            $('#PopupRoom').modal('show');
        }
    </script>
    <!-- END JAVASCRIPTS -->

</asp:Content>
