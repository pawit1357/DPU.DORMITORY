<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="RoomType.aspx.cs" Inherits="DPU.DORMITORY.Web.View.Master.RoomType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <form runat="server" id="Form1" method="POST" enctype="multipart/form-data" class="form-horizontal">
        <%--   <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="updResult" runat="server">

            <ContentTemplate>--%>
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
                    <span class="caption-subject font-red-sunglo bold uppercase">[<asp:Label ID="lbCommandName" runat="server" Text=""></asp:Label>]&nbsp;
                        <asp:Literal ID="litPageTitle" runat="server" /></span>
                </div>
                <%--               <div class="tools">
                    <a href="#" class="collapse"></a>
                </div>--%>
            </div>
            <div class="portlet-body form">
                <!-- BEGIN FORM-->


                <div class="form-body">
                    <%--<h3 class="form-section">บันทึกข้อมูลบริการ</h3>--%>
                    <%--                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">รหัส</label>
                                <div class="col-md-9">
                                    <asp:TextBox ID="txtID" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>--%>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">ชื่อ</label>
                                <div class="col-md-9">
                                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
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
                </div>

                <!-- END FORM-->
            </div>
        </div>


        <%--        </ContentTemplate>
        </asp:UpdatePanel>--%>
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
                    ctl00$ContentPlaceHolder1$txtID: {
                        required: true,
                        number: true
                    },
                    ctl00$ContentPlaceHolder1$txtName: {
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
