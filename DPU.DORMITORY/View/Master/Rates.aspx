<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Rates.aspx.cs" Inherits="DPU.DORMITORY.Web.View.Master.Rates" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%--      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <form id="Form1" method="post" runat="server" class="form-horizontal">
<%--        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>

        <%--<asp:HiddenField ID="hPKID" Value="0" runat="server" />--%>
        <div class="portlet light bordered">
            <div class="portlet-title">
                <div class="caption">
                    <i class="icon-equalizer font-red-sunglo"></i>
                    <span class="caption-subject font-red-sunglo bold uppercase">[<asp:Label ID="lbCommandName" runat="server" Text=""></asp:Label>]&nbsp;
                        <asp:Literal ID="litPageTitle" runat="server" /></span>
                    <%--<span class="caption-helper">กรอกข้อมูลให้ครบถ้วนในช่อง *</span>--%>
                </div>
                <div class="tools">
                    <div class="btn-group">
                        <%--                        <asp:LinkButton ID="btnAdd" class="btn red-sunglo btn-sm" runat="server" OnClick="btnAdd_Click"> Add <i class="icon-plus"></i></asp:LinkButton>--%>
                    </div>
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
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">อาคาร:<span class="required" aria-required="true">*</span></label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="ddlBuildId" runat="server" class="select2_category form-control" DataTextField="Name" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="ddlBuildId_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">ประเภทห้อง:<span class="required" aria-required="true">*</span></label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="ddlRoomType" runat="server" class="select2_category form-control" DataTextField="Name" DataValueField="ID"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">ชื่อ<span class="required" aria-required="true">*</span></label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtName" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">รายละเอียด<span class="required" aria-required="true">*</span></label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtDescription" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">อัตราค่าประกัน<span class="required" aria-required="true">*</span></label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtAmout" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="lbDate" runat="server" CssClass="control-label col-md-3">ระยะเวลาเริ่มต้น<span class="required" aria-required="true">* </span></asp:Label>
                                <div class="col-md-9">
                                    <div class="input-group input-medium date date-picker" data-date="10/2012" data-date-format="dd/mm/yyyy" data-date-viewmode="years" data-date-minviewmode="months">
                                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control"></asp:TextBox>
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
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="control-label col-md-3">ระยะเวลาสิ้นสุด<span class="required" aria-required="true">* </span></asp:Label>
                                <div class="col-md-9">
                                    <div class="input-group input-medium date date-picker" data-date="10/2012" data-date-format="dd/mm/yyyy" data-date-viewmode="years" data-date-minviewmode="months">
                                        <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control"></asp:TextBox>
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
                            <div class="form-group">
                                <label class="control-label col-md-3">คิดค่าห้องตาม:<span class="required" aria-required="true">*</span></label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="ddlCalType" runat="server" class="select2_category form-control" DataTextField="Name" DataValueField="ID">
                                        <asp:ListItem Value="1">คิดรายบุคคล</asp:ListItem>
                                        <asp:ListItem Value="2">คิดตามห้อง</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- BEGIN PAGE CONTENT-->
                    <h4 class="form-section">กลุ่มค่าใช้จ่าย</h4>
                    <div class="row">
                        <div class="col-md-9">
                            <asp:GridView ID="gvResult" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                CssClass="table table-striped table-hover table-bordered" ShowHeaderWhenEmpty="True" DataKeyNames="ID" OnRowCancelingEdit="gvResult_RowCancelingEdit" OnRowDataBound="gvResult_RowDataBound" OnRowDeleting="gvResult_RowDeleting" OnRowEditing="gvResult_RowEditing" OnRowUpdating="gvResult_RowUpdating">
                                <Columns>
                                                                    <asp:TemplateField HeaderText="ลำดับ" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                    <asp:TemplateField HeaderText="รายการค่าใช้จ่าย" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Literal ID="litServiceName" runat="server" Text='<%# Eval("SERVICE_NAME")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="จำนวน" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Literal ID="litUnit" runat="server" Text='<%# Eval("UNIT")%>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtUnit" runat="server" Text='<%# Eval("UNIT")%>' CssClass="form-control" TextMode="Number"></asp:TextBox>
                      <%--                      <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtUnit" TargetControlID="txtUnit"
                                                FilterType="Custom, Numbers" ValidChars=".,-" runat="server" />--%>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="จำนวนเงิน" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Literal ID="litAMOUNT" runat="server" Text='<%# Eval("AMOUNT")%>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtAMOUNT" runat="server" Text='<%# Eval("AMOUNT")%>' CssClass="form-control" TextMode="Number"></asp:TextBox>
                                           <%-- <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtAMOUNT" TargetControlID="txtAMOUNT"
                                                FilterType="Custom, Numbers" ValidChars=".,-" runat="server" />--%>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="อัตตราภาษี" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Literal ID="litVAT" runat="server" Text='<%# Eval("VAT")%>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtVAT" runat="server" Text='<%# Eval("VAT")%>' CssClass="form-control" TextMode="Number"></asp:TextBox>
                            <%--                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtVAT" TargetControlID="txtVAT"
                                                FilterType="Custom, Numbers" ValidChars=".,-" runat="server" />--%>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Main Trans." ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Literal ID="litMainTrans" runat="server" Text='<%# Eval("MAIN_TRANS")%>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtMainTrans" runat="server" Text='<%# Eval("MAIN_TRANS")%>' CssClass="form-control" TextMode="Number"></asp:TextBox>
                            <%--                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtVAT" TargetControlID="txtVAT"
                                                FilterType="Custom, Numbers" ValidChars=".,-" runat="server" />--%>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Sub Trans." ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Literal ID="litSubTrans" runat="server" Text='<%# Eval("SUB_TRANS")%>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtSubTrans" runat="server" Text='<%# Eval("SUB_TRANS")%>' CssClass="form-control" TextMode="Number"></asp:TextBox>
                            <%--                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtVAT" TargetControlID="txtVAT"
                                                FilterType="Custom, Numbers" ValidChars=".,-" runat="server" />--%>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
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
                                <PagerTemplate>
                                    <div class="pagination">
                                        <ul>
                                            <li>
                                                <asp:LinkButton ID="btnFirst" runat="server" CommandName="Page" CommandArgument="First"
                                                    CausesValidation="false" ToolTip="First Page"><i class="icon-fast-backward"></i></asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="btnPrev" runat="server" CommandName="Page" CommandArgument="Prev"
                                                    CausesValidation="false" ToolTip="Previous Page"><i class="icon-backward"></i> Prev</asp:LinkButton>
                                            </li>
                                            <asp:PlaceHolder ID="pHolderNumberPage" runat="server" />
                                            <li>
                                                <asp:LinkButton ID="btnNext" runat="server" CommandName="Page" CommandArgument="Next"
                                                    CausesValidation="false" ToolTip="Next Page">Next <i class="icon-forward"></i></asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="btnLast" runat="server" CommandName="Page" CommandArgument="Last"
                                                    CausesValidation="false" ToolTip="Last Page"><i class="icon-fast-forward"></i></asp:LinkButton>
                                            </li>
                                        </ul>
                                    </div>
                                </PagerTemplate>
                                <EmptyDataTemplate>
                                    <div class="data-not-found">
                                        <asp:Literal ID="libDataNotFound" runat="server" Text="Data Not found" />
                                    </div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </div>
                    <!-- END PAGE CONTENT-->
                    <div class="form-actions">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="btnSave" runat="server" class="btn green" Text="Save" OnClick="btnSave_Click" />
                                        <asp:LinkButton ID="btnCancel" runat="server" class="btn default" OnClick="btnCancel_Click"> Cancel</asp:LinkButton>
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
        </div>
    </form>

   <%--         </ContentTemplate>
          </asp:UpdatePanel>--%>
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
                    ctl00$ContentPlaceHolder1$ddlBuildId: {
                        required: true
                    },
                    ctl00$ContentPlaceHolder1$txtName: {
                        required: true
                    },
                    ctl00$ContentPlaceHolder1$txtDescription: {
                        required: true
                    },
                    ctl00$ContentPlaceHolder1$txtAmout: {
                        required: true,
                        number: true
                    },
                    ctl00$ContentPlaceHolder1$txtStartDate: {
                        required: true,
                    },
                    ctl00$ContentPlaceHolder1$txtEndDate: {
                        required: true,
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
