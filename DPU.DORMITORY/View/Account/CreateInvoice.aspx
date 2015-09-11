<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CreateInvoice.aspx.cs" Inherits="DPU.DORMITORY.Web.View.Account.CreateInvoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="Form1" method="post" runat="server" class="form-horizontal">

        <asp:HiddenField ID="hCost01_1" runat="server" Value="0" />
        <asp:HiddenField ID="hCost02_1" runat="server" Value="0" />
        <asp:HiddenField ID="hCost03_1" runat="server" Value="0" />
        <asp:HiddenField ID="hCost04_1" runat="server" Value="0" />
        <asp:HiddenField ID="hCost05_1" runat="server" Value="0" />
        <asp:HiddenField ID="hCost06_1" runat="server" Value="0" />
        <asp:HiddenField ID="hCost07_1" runat="server" Value="0" />
        <asp:HiddenField ID="hCost08_1" runat="server" Value="0" />
        <asp:HiddenField ID="hCost09_1" runat="server" Value="0" />

        <asp:HiddenField ID="hCost01_2" runat="server" Value="0" />
        <asp:HiddenField ID="hCost02_2" runat="server" Value="0" />
        <asp:HiddenField ID="hCost03_2" runat="server" Value="0" />
        <asp:HiddenField ID="hCost04_2" runat="server" Value="0" />
        <asp:HiddenField ID="hCost05_2" runat="server" Value="0" />
        <asp:HiddenField ID="hCost06_2" runat="server" Value="0" />
        <asp:HiddenField ID="hCost07_2" runat="server" Value="0" />
        <asp:HiddenField ID="hCost08_2" runat="server" Value="0" />
        <asp:HiddenField ID="hCost09_2" runat="server" Value="0" />

        <div class="portlet light bordered">
            <div class="portlet-title">
                <div class="caption">
                    <i class="icon-equalizer font-red-sunglo"></i>
                    <span class="caption-subject font-red-sunglo bold uppercase">บันทึกข้อมูลสำหรับออกใบแจ้งหนี้</span>
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
                <div class="form-body">
                    <h4 class="form-section">ห้องเช่า</h4>

                    <!--/row-->
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
                                <label class="control-label col-md-3">ห้อง.<span class="required" aria-required="true">* </span></label>
                                <div class="col-md-9">
                                    <div class="input-group" style="text-align: left">
                                        <asp:TextBox ID="txtRoom" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                                        <span class="input-group-btn">
                                            <%--<asp:Button ID="btnCheckRoom" runat="server" Text="Check" CssClass="btn green" OnClick="btnCheckRoom_Click" />--%>
                                            <asp:LinkButton ID="btnCheckRoom" runat="server" CssClass="btn green" OnClick="btnCheckRoom_Click"> Check</asp:LinkButton>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:Panel ID="pPaymentInfo" runat="server">
                        <h4 class="form-section">รายการใบแจ้งหนี้ที่ยังไม่ชำระเงิน</h4>
                        <div class="row">
                            <div class="col-md-6">
                                <asp:GridView ID="gvPaymentHistory" runat="server" AutoGenerateColumns="False" AllowPaging="False"
                                    CssClass="table table-striped table-hover table-bordered" ShowHeaderWhenEmpty="True" DataKeyNames="ID" OnRowCommand="gvPaymentHistory_RowCommand" OnRowDeleting="gvPaymentHistory_RowDeleting" OnRowDataBound="gvPaymentHistory_RowDataBound">
                                    <Columns>
                                        <asp:BoundField HeaderText="ประจำเดือน" DataField="POSTING_DATE" ItemStyle-HorizontalAlign="Left" SortExpression="POSTING_DATE" DataFormatString="{0:MM-yyyy}" />
                                        <asp:TemplateField HeaderText="เลขทะเบียนนศ./เลขห้อง" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="litRefDesc" runat="server" Text='<%# Eval("REF_DESC")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="เลขที่ใบแจ้งหนี้" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="litSapDocNo" runat="server" Text='<%# Eval("SAP_DOCNO")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="จำนวนเงิน" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="litAmount" runat="server" Text='<%# Eval("AMOUNT")%>' />
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
                                                <asp:LinkButton ID="btnDelete" runat="server" ToolTip="Delete" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                                    CommandArgument='<%# Eval("ID")%>'><i class="fa fa-trash"></i></asp:LinkButton>
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

                        <h4 class="form-section">ข้อมูลค่าใช้จ่าย</h4>
                        <div class="row">
                            <!--/span-->
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label col-md-3">ประจำเดือน</label>
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
                                            <th>หน่วยละ</th>
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
                        </div>
                        <div class="form-actions">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="row">
                                        <div class="col-md-offset-3 col-md-9">
                                            <asp:Button ID="btnCalculate" runat="server" class="btn green" Text="Calculate" OnClick="btnCalculate_Click" />
                                            <asp:LinkButton ID="btnClear" runat="server" class="btn default" OnClick="btnClear_Click"> Clear</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-11">
                                <table class="table table-striped table-hover table-bordered">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>รายการ</th>
                                            <th>ค่าบริการ</th>
                                            <th id="th_cus_1" runat="server">
                                                <asp:Label ID="lbCus01" runat="server"></asp:Label>
                                            </th>
                                            <th id="th_cus_1_rate" runat="server">Rate</th>
                                            <th id="th_cus_2" runat="server">
                                                <asp:Label ID="lbCus02" runat="server"></asp:Label>
                                            </th>
                                            <th id="th_cus_2_rate" runat="server">Rates</th>

                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>1</td>
                                            <td>ค่าเช่าหอพัก</td>
                                            <td>
                                                <asp:TextBox ID="txtCost01" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                                            <td id="td_cus_1_1_pay" runat="server">
                                                <asp:DropDownList ID="ddlPay01_1" runat="server" DataTextField="Name" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="ddlPay01_1_SelectedIndexChanged"></asp:DropDownList></td>
                                            <td id="td_cus_1_1" runat="server">
                                                <asp:TextBox ID="txtCost01_1" runat="server" CssClass="form-control"></asp:TextBox>
                                            </td>
                                            <td id="td_cus_2_1_pay" runat="server">
                                                <asp:DropDownList ID="ddlPay01_2" runat="server" DataTextField="Name" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="ddlPay01_1_SelectedIndexChanged"></asp:DropDownList></td>
                                            <td id="td_cus_2_1" runat="server">
                                                <asp:TextBox ID="txtCost01_2" runat="server" CssClass="form-control"></asp:TextBox></td>

                                        </tr>
                                        <tr>
                                            <td>2</td>
                                            <td>รายได้ค่าเช่าฟอร์นิเจอร์-หอพัก</td>
                                            <td>
                                                <asp:TextBox ID="txtCost02" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                                            <td id="td_cus_1_2_pay" runat="server">
                                                <asp:DropDownList ID="ddlPay02_1" runat="server" DataTextField="Name" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="ddlPay01_1_SelectedIndexChanged"></asp:DropDownList></td>
                                            <td id="td_cus_1_2" runat="server">
                                                <asp:TextBox ID="txtCost02_1" runat="server" CssClass="form-control"></asp:TextBox></td>
                                            <td id="td_cus_2_2_pay" runat="server">
                                                <asp:DropDownList ID="ddlPay02_2" runat="server" DataTextField="Name" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="ddlPay01_1_SelectedIndexChanged"></asp:DropDownList></td>
                                            <td id="td_cus_2_2" runat="server">
                                                <asp:TextBox ID="txtCost02_2" runat="server" CssClass="form-control"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>3</td>
                                            <td>ค่าน้ำ</td>
                                            <td>
                                                <asp:TextBox ID="txtCost03" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                                            <td id="td_cus_1_3_pay" runat="server">
                                                <asp:DropDownList ID="ddlPay03_1" runat="server" DataTextField="Name" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="ddlPay01_1_SelectedIndexChanged"></asp:DropDownList></td>

                                            <td id="td_cus_1_3" runat="server">
                                                <asp:TextBox ID="txtCost03_1" runat="server" CssClass="form-control"></asp:TextBox></td>
                                            <td id="td_cus_2_3_pay" runat="server">
                                                <asp:DropDownList ID="ddlPay03_2" runat="server" DataTextField="Name" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="ddlPay01_1_SelectedIndexChanged"></asp:DropDownList></td>

                                            <td id="td_cus_2_3" runat="server">
                                                <asp:TextBox ID="txtCost03_2" runat="server" CssClass="form-control"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>4</td>
                                            <td>ค่าไฟ</td>
                                            <td>
                                                <asp:TextBox ID="txtCost04" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                                            <td id="td_cus_1_4_pay" runat="server">
                                                <asp:DropDownList ID="ddlPay04_1" runat="server" DataTextField="Name" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="ddlPay01_1_SelectedIndexChanged"></asp:DropDownList></td>

                                            <td id="td_cus_1_4" runat="server">
                                                <asp:TextBox ID="txtCost04_1" runat="server" CssClass="form-control"></asp:TextBox></td>
                                            <td id="td_cus_2_4_pay" runat="server">
                                                <asp:DropDownList ID="ddlPay04_2" runat="server" DataTextField="Name" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="ddlPay01_1_SelectedIndexChanged"></asp:DropDownList></td>

                                            <td id="td_cus_2_4" runat="server">
                                                <asp:TextBox ID="txtCost04_2" runat="server" CssClass="form-control"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>5</td>
                                            <td>ค่าโทรศัพท์</td>
                                            <td>
                                                <asp:TextBox ID="txtCost05" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                                            <td id="td_cus_1_5_pay" runat="server">
                                                <asp:DropDownList ID="ddlPay05_1" runat="server" DataTextField="Name" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="ddlPay01_1_SelectedIndexChanged"></asp:DropDownList></td>

                                            <td id="td_cus_1_5" runat="server">
                                                <asp:TextBox ID="txtCost05_1" runat="server" CssClass="form-control"></asp:TextBox></td>
                                            <td id="td_cus_2_5_pay" runat="server">
                                                <asp:DropDownList ID="ddlPay05_2" runat="server" DataTextField="Name" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="ddlPay01_1_SelectedIndexChanged"></asp:DropDownList></td>

                                            <td id="td_cus_2_5" runat="server">
                                                <asp:TextBox ID="txtCost05_2" runat="server" CssClass="form-control"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>6</td>
                                            <td>ค่าอินเตอร์เนต</td>
                                            <td>
                                                <asp:TextBox ID="txtCost06" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                                            <td id="td_cus_1_6_pay" runat="server">
                                                <asp:DropDownList ID="ddlPay06_1" runat="server" DataTextField="Name" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="ddlPay01_1_SelectedIndexChanged"></asp:DropDownList></td>

                                            <td id="td_cus_1_6" runat="server">
                                                <asp:TextBox ID="txtCost06_1" runat="server" CssClass="form-control"></asp:TextBox></td>
                                            <td id="td_cus_2_6_pay" runat="server">
                                                <asp:DropDownList ID="ddlPay06_2" runat="server" DataTextField="Name" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="ddlPay01_1_SelectedIndexChanged"></asp:DropDownList></td>

                                            <td id="td_cus_2_6" runat="server">
                                                <asp:TextBox ID="txtCost06_2" runat="server" CssClass="form-control"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>7</td>
                                            <td>ค่าซ่อมแซม</td>
                                            <td>
                                                <asp:TextBox ID="txtCost07" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                                            <td id="td_cus_1_7_pay" runat="server">
                                                <asp:DropDownList ID="ddlPay07_1" runat="server" DataTextField="Name" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="ddlPay01_1_SelectedIndexChanged"></asp:DropDownList></td>

                                            <td id="td_cus_1_7" runat="server">
                                                <asp:TextBox ID="txtCost07_1" runat="server" CssClass="form-control"></asp:TextBox></td>
                                            <td id="td_cus_2_7_pay" runat="server">
                                                <asp:DropDownList ID="ddlPay07_2" runat="server" DataTextField="Name" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="ddlPay01_1_SelectedIndexChanged"></asp:DropDownList></td>

                                            <td id="td_cus_2_7" runat="server">
                                                <asp:TextBox ID="txtCost07_2" runat="server" CssClass="form-control"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>8</td>
                                            <td>ค่าปรับ</td>
                                            <td>
                                                <asp:TextBox ID="txtCost08" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                                            <td id="td_cus_1_8_pay" runat="server">
                                                <asp:DropDownList ID="ddlPay08_1" runat="server" DataTextField="Name" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="ddlPay01_1_SelectedIndexChanged"></asp:DropDownList></td>

                                            <td id="td_cus_1_8" runat="server">
                                                <asp:TextBox ID="txtCost08_1" runat="server" CssClass="form-control"></asp:TextBox></td>
                                            <td id="td_cus_2_8_pay" runat="server">
                                                <asp:DropDownList ID="ddlPay08_2" runat="server" DataTextField="Name" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="ddlPay01_1_SelectedIndexChanged"></asp:DropDownList></td>

                                            <td id="td_cus_2_8" runat="server">
                                                <asp:TextBox ID="txtCost08_2" runat="server" CssClass="form-control"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>9</td>
                                            <td>อื่น ๆ ระบุ &nbsp;<asp:TextBox ID="txtOther" runat="server" CssClass="form-control"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtCost09" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                                            <td id="td_cus_1_9_pay" runat="server">
                                                <asp:DropDownList ID="ddlPay09_1" runat="server" DataTextField="Name" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="ddlPay01_1_SelectedIndexChanged"></asp:DropDownList></td>

                                            <td id="td_cus_1_9" runat="server">
                                                <asp:TextBox ID="txtCost09_1" runat="server" CssClass="form-control"></asp:TextBox></td>
                                            <td id="td_cus_2_9_pay" runat="server">
                                                <asp:DropDownList ID="ddlPay09_2" runat="server" DataTextField="Name" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="ddlPay01_1_SelectedIndexChanged"></asp:DropDownList></td>

                                            <td id="td_cus_2_9" runat="server">
                                                <asp:TextBox ID="txtCost09_2" runat="server" CssClass="form-control"></asp:TextBox></td>
                                        </tr>

                                                                                <tr>
                                            <td></td>
                                            <td>รวมเป็นเงิน</td>
                                            <td id="td_amout_1" runat="server">
                                                <asp:TextBox ID="txtAmout1" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                                            <td id="td_amout_bank_1" runat="server">
                                                &nbsp;</td>

                                            <td id="td_amout_2" runat="server">
                                                <asp:TextBox ID="txtAmout2" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                                            <td id="td_amout_bank_2" runat="server">
                                                &nbsp;</td>

                                            <td id="td_amout_3" runat="server">
                                                <asp:TextBox ID="txtAmout3" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>

                        <h4 class="form-section">สรุปค่าใช้จ่าย</h4>

                        <div class="row">
                            <!--/span-->
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label col-md-3">จำนวนวันที่เข้าพัก</label>
                                    <div class="col-md-9">
                                        <asp:TextBox ID="txtStayDay" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <!--/span-->
<%--                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label col-md-3">รวมเป็นเงิน</label>
                                    <div class="col-md-9">
                                        <asp:TextBox ID="txtAmout" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>--%>
                        </div>

                        <!--/row-->
                        <div class="form-actions">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="row">
                                        <div class="col-md-offset-3 col-md-9">
                                            <asp:Button ID="btnSave" runat="server" class="btn green" Text="Submit" OnClick="btnSave_Click" />
                                            <asp:Button ID="btnPrintInvoice" runat="server" class="btn green" Text="พิมพ์ใบแจ้งหนี้" OnClick="btnPrintInvoice_Click" />
                                            <asp:LinkButton ID="btnCancel" runat="server" class="btn default" OnClick="btnCancel_Click"> Cancel</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <!-- END FORM-->
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

                    ctl00$ContentPlaceHolder1$txtRoom: {
                        required: true,
                        number: true
                    },
                    //ctl00$ContentPlaceHolder1$txtCost01: {
                    //    required: true,
                    //    number: true
                    //},
                    //ctl00$ContentPlaceHolder1$txtCost02: {
                    //    required: true,
                    //    number: true
                    //},
                    //ctl00$ContentPlaceHolder1$txtCost03: {
                    //    required: true,
                    //    number: true
                    //},
                    //ctl00$ContentPlaceHolder1$txtCost04: {
                    //    required: true,
                    //    number: true
                    //},
                    //ctl00$ContentPlaceHolder1$txtCost05: {
                    //    required: true,
                    //    number: true
                    //},
                    //ctl00$ContentPlaceHolder1$txtCost06: {
                    //    required: true,
                    //    number: true
                    //},
                    //ctl00$ContentPlaceHolder1$txtCost07: {
                    //    required: true,
                    //    number: true
                    //},
                    //ctl00$ContentPlaceHolder1$txtCost08: {
                    //    required: true,
                    //    number: true
                    //},
                    //ctl00$ContentPlaceHolder1$txtCost09: {
                    //    required: true,
                    //    number: true
                    //},

                    ctl00$ContentPlaceHolder1$txtStayDay: {
                        required: true,
                        number: true
                    },

                    ctl00$ContentPlaceHolder1$txtElecMeterStart: {
                        required: true,
                        number: true
                    },
                    ctl00$ContentPlaceHolder1$txtElecMeterEnd: {
                        required: true,
                        number: true
                    },

                    ctl00$ContentPlaceHolder1$txtWaterMeterStart: {
                        required: true,
                        number: true
                    },
                    ctl00$ContentPlaceHolder1$txtWaterMeterEnd: {
                        required: true,
                        number: true
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
