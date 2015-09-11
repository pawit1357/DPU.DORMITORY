<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CheckIn.aspx.cs" Inherits="DPU.DORMITORY.Web.View.Management.CheckIn" EnableEventValidation="false" %>


<%@ Reference Page="~/View/Management/SearchCustomer.aspx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <form id="Form1" method="post" runat="server" class="form-horizontal">
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

                    <asp:Panel ID="pCustomerInfo" runat="server">
                        <h4 class="form-section">ข้อมูลผู้เช่า</h4>
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
                        <h4 class="form-section">ข้อมูลทุนการศึกษา</h4>
                        <div class="row">
                            <div class="col-md-9">
                                <asp:GridView ID="gvFund" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                    CssClass="table table-striped table-hover table-bordered" ShowHeaderWhenEmpty="True" DataKeyNames="ID">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkRow" runat="server" />
                                                <asp:HiddenField ID="hID" Value='<%# Eval("ID")%>' runat="server" />
                                                <asp:HiddenField ID="hForServiceID" Value='<%# Eval("FOR_SERVICE_ID")%>' runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="เจ้าของทุน" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Literal ID="litFund" runat="server" Text='<%# Eval("NAME")%>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtFund" runat="server" Text='<%# Eval("NAME")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="คำอธิบาย" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Literal ID="litDescription" runat="server" Text='<%# Eval("DESCRIPTION")%>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtDescription" runat="server" Text='<%# Eval("DESCRIPTION")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
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
                                    <label class="control-label col-md-3">จังหวัด</label>
                                    <div class="col-md-9">
                                        <asp:TextBox ID="txtPronvice" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>

                                        <%--<asp:DropDownList ID="ddlProvince" runat="server" class="select2_category form-control" DataTextField="Name" DataValueField="ID"></asp:DropDownList>--%>
                                    </div>
                                </div>
                            </div>
                            <!--/span-->
                        </div>
                        <!--/span-->
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label col-md-3">อำเภอ</label>
                                    <div class="col-md-9">
                                        <asp:TextBox ID="txtAmphur" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>

                                        <%--<asp:DropDownList ID="ddlAmphur" runat="server" class="select2_category form-control" DataTextField="Name" DataValueField="ID"></asp:DropDownList>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!--/row-->
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label col-md-3">ตำบล</label>
                                    <div class="col-md-9">
                                        <asp:TextBox ID="txtTambon" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>

                                        <%--<asp:DropDownList ID="ddlTambon" runat="server" class="select2_category form-control" DataTextField="Name" DataValueField="ID"></asp:DropDownList>--%>
                                    </div>
                                </div>
                            </div>

                            <!--/span-->
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
                            <!--/span-->
                        </div>
                        <!--/row-->
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label col-md-3">เบอร์ติดต่อ<span class="required" aria-required="true">* </span></label>
                                    <div class="col-md-9">
                                        <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
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
                                            <span class="input-group-btn">
                                                <a class="btn green" href="#PopupRoom" data-toggle="modal" runat="server" id="aPopupRoom">Select</a>
                                                <a class="btn red" href="#PopupRoomInfo" data-toggle="modal" runat="server" id="a1">Info</a>

                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--/span-->
                        </div>
                        <!--/row-->
                        <%--                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label col-md-3">อัตราค่าบริการ</label>
                                    <div class="col-md-6">

                                        <asp:DropDownList ID="ddlRateGroupId" runat="server" class="select2_category form-control" DataTextField="Description" DataValueField="ID"></asp:DropDownList>

                                    </div>
                                </div>
                            </div>
                        </div>--%>
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
                                                <a class="btn green" href="#PopupRoom" data-toggle="modal">Select</a>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--/span-->
                        </div>
                    </asp:Panel>
                    <!--/row-->
                    <div class="form-actions">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="btnSave" runat="server" class="btn green" Text="Save" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnPrintInvoice" runat="server" class="btn blue" Text="พิมพ์ใบแจ้งหนี้" OnClick="btnPrintInvoice_Click" />
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
        <div class="modal fade" id="PopupRoom" tabindex="-1" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                        <h4 class="modal-title">Select Room</h4>
                    </div>
                    <div class="modal-body">
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
                        <%--<button type="button" id="btnSelectCompany" class="btn blue" data-dismiss="modal">OK</button>--%>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
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
                        required: true,
                        number: true
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
    </script>
    <!-- END JAVASCRIPTS -->

</asp:Content>
