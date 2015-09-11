<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="MyProfile.aspx.cs" Inherits="DPU.DORMITORY.View.Admin.MyProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <form id="Form1" method="post" runat="server" class="form-horizontal">

        <div class="alert alert-danger display-hide">
            <button class="close" data-close="alert"></button>
            You have some form errors. Please check below.
        </div>
        <div class="alert alert-success display-hide">
            <button class="close" data-close="alert"></button>
            Your form validation is successful!
        </div>

        <div class="portlet light bordered">
            <div class="portlet-title">
                <div class="caption">
                    <i class="icon-equalizer font-red-sunglo"></i>
                    <span class="caption-subject font-red-sunglo bold uppercase">[<asp:Label ID="lbCommandName" runat="server" Text=""></asp:Label>]&nbsp;Profile</span>
                    <span class="caption-helper"></span>
                </div>
                <div class="tools">
                    <%--<a href="#" class="collapse"></a>--%>
                </div>
            </div>
            <div class="portlet-body form">
                <div class="form-body">
                    <!-- BEGIN FORM-->
                    <h4 class="form-section">&nbsp;User Accout</h4>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Role:<span class="required">*</span></label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="ddlRole" runat="server" CssClass="select2_category form-control" DataTextField="NAME" DataValueField="ROLE_ID"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">User:<span class="required">*</span></label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtUser" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Password:<span class="required">*</span></label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtPassword" runat="server" class="form-control" TextMode="Password"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                     <asp:Panel ID="pBuildingOwner" runat="server" >
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">BuildingOwner:<span class="required">*</span></label>
                                <div class="col-md-6">
                                    <asp:ListBox ID="lstBuild" runat="server" DataTextField="Name" DataValueField="ID" SelectionMode="Multiple" class="bs-select form-control disabled"></asp:ListBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    </asp:Panel>
                    <h4 class="form-section">&nbsp;Personal Information</h4>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Title:<span class="required">*</span></label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="ddlTitle" runat="server" CssClass="select2_category form-control" DataTextField="name" DataValueField="ID"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">First Name:<span class="required">*</span></label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtFirstName" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Last Name:<span class="required">*</span></label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtLastName" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Email:<span class="required">*</span></label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtEmail" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Phone:<span class="required">*</span></label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtPhone" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">
                                    Status:<span class="required">
										* </span>
                                </label>
                                <div class="radio-list">
                                    <label class="radio-inline">
                                        <asp:RadioButton ID="rdStatusA" GroupName="Status" runat="server" Checked="true" />Active
                                    </label>
                                    <label class="radio-inline">
                                        <asp:RadioButton ID="rdStatusI" GroupName="Status" runat="server" />InAvtive
                                    </label>

                                </div>
                            </div>
                        </div>
                    </div>
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
                    ctl00$ContentPlaceHolder1$ddlRole: {
                        required: true,
                    },
                    ctl00$ContentPlaceHolder1$txtUser: {
                        minlength: 2,
                        required: true,
                    },
                    ctl00$ContentPlaceHolder1$txtEmail: {
                        required: true,
                        email:true
                    },
                    ctl00$ContentPlaceHolder1$txtPassword: {
                        minlength: 2,
                        required: true,
                    },
                    ctl00$ContentPlaceHolder1$ddlTitle: {
                        required: true,
                    },
                    ctl00$ContentPlaceHolder1$txtFirstName: {
                        minlength: 2,
                        required: true,
                    },
                    ctl00$ContentPlaceHolder1$txtLastName: {
                        minlength: 2,
                        required: true,
                    },
                    ctl00$ContentPlaceHolder1$txtPhone: {
                        minlength: 2,
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
