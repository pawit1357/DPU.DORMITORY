<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DPU.DORMITORY.Web.Login" %>

<!DOCTYPE html>

<!-- 
Template Name: Metronic - Responsive Admin Dashboard Template build with Twitter Bootstrap 3.3.4
Version: 3.3.0
Author: KeenThemes
Website: http://www.keenthemes.com/
Contact: support@keenthemes.com
Follow: www.twitter.com/keenthemes
Like: www.facebook.com/keenthemes
Purchase: http://themeforest.net/item/metronic-responsive-admin-dashboard-template/4021469?ref=keenthemes
License: You must have a valid license purchased only from themeforest(the above link) in order to legally use the theme for your project.
-->
<!--[if IE 8]> <html lang="en" class="ie8 no-js"> <![endif]-->
<!--[if IE 9]> <html lang="en" class="ie9 no-js"> <![endif]-->
<!--[if !IE]><!-->
<html lang="en">
<!--<![endif]-->
<!-- BEGIN HEAD -->
<head>
    <meta charset="utf-8" />
    <title>DPU | dormitory</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
    <meta http-equiv="Content-type" content="text/html; charset=utf-8">
    <meta content="" name="description" />
    <meta content="" name="author" />
    <!-- BEGIN GLOBAL MANDATORY STYLES -->
<%--    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css" />--%>
    <link href="<%= ResolveUrl("~/assets/global/plugins/font-awesome/css/font-awesome.min.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= ResolveUrl("~/assets/global/plugins/simple-line-icons/simple-line-icons.min.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= ResolveUrl("~/assets/global/plugins/bootstrap/css/bootstrap.min.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= ResolveUrl("~/assets/global/plugins/uniform/css/uniform.default.css") %>" rel="stylesheet" type="text/css" />
    <!-- END GLOBAL MANDATORY STYLES -->
    <!-- BEGIN PAGE LEVEL STYLES -->
    <link href="<%= ResolveUrl("~/assets/admin/pages/css/login.css") %>" rel="stylesheet" type="text/css" />
    <!-- END PAGE LEVEL SCRIPTS -->
    <!-- BEGIN THEME STYLES -->
    <link href="<%= ResolveUrl("~/assets/global/css/components.css") %>" id="style_components" rel="stylesheet" type="text/css" />
    <link href="<%= ResolveUrl("~/assets/global/css/plugins.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= ResolveUrl("~/assets/admin/layout/css/layout.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= ResolveUrl("~/assets/admin/layout/css/themes/darkblue.css") %>" rel="stylesheet" type="text/css" id="style_color" />
    <link href="<%= ResolveUrl("~/assets/admin/layout/css/custom.css") %>" rel="stylesheet" type="text/css" />
    <!-- END THEME STYLES -->
    <link rel="shortcut icon" href="favicon.ico" />
</head>
<!-- END HEAD -->
<!-- BEGIN BODY -->
<body class="login">
    <!-- BEGIN SIDEBAR TOGGLER BUTTON -->
    <div class="menu-toggler sidebar-toggler">
    </div>
    <!-- END SIDEBAR TOGGLER BUTTON -->
    <!-- BEGIN LOGO -->
    <div class="logo">
        <a href="index.html">
            <%--<img src="/dorm/assets/admin/layout/img/logo-big.png" alt=""/>--%>
        </a>
    </div>
    <!-- END LOGO -->
    <!-- BEGIN LOGIN -->
    <div class="content">
        <!-- BEGIN LOGIN FORM -->
        <form class="login-form" runat="server">
            <h3 class="form-title">Sign In</h3>
            <%=Message %>

            <div class="form-group">
                <!--ie8, ie9 does not support html5 placeholder, so we just show field title for that-->
                <label class="control-label visible-ie8 visible-ie9">Username</label>
                <asp:TextBox ID="txtUserName" CssClass="form-control form-control-solid placeholder-no-fix" runat="server" placeholder="Username"></asp:TextBox>
            </div>
            <div class="form-group">
                <label class="control-label visible-ie8 visible-ie9">Password</label>
                <asp:TextBox ID="txtPassword" CssClass="form-control form-control-solid placeholder-no-fix" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>
            </div>
            <div class="form-actions">
                <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-success uppercase" OnClick="btnLogin_Click" />
                <label class="rememberme check">
                    <input type="checkbox" name="remember" value="1" />Remember
                </label>
                <%--<a href="/dorm/ForgotPassword.aspx" id="forget-password" class="forget-password">Forgot Password?</a>--%>
            </div>
            <div class="login-options">
                <h4>Or login with</h4>
                <ul class="social-icons">
                    <li>
                        <a class="social-icon-color facebook" data-original-title="facebook" href="javascript:;"></a>
                    </li>
                    <li>
                        <a class="social-icon-color twitter" data-original-title="Twitter" href="javascript:;"></a>
                    </li>
                    <li>
                        <a class="social-icon-color googleplus" data-original-title="Goole Plus" href="javascript:;"></a>
                    </li>
                    <li>
                        <a class="social-icon-color linkedin" data-original-title="Linkedin" href="javascript:;"></a>
                    </li>
                </ul>
            </div>
            <div class="create-account">
                <p>
                    <a href="javascript:;" id="register-btn" class="uppercase">Create an account</a>
                </p>
            </div>
        </form>
        <!-- END LOGIN FORM -->

    </div>
    <div class="copyright">
        2015 &copy; <a href="http://www.dpu.ac.th" title="Dhurakij Pundit University" target="_blank">Dhurakij Pundit University</a>
    </div>
    <!-- END LOGIN -->
    <!-- BEGIN JAVASCRIPTS(Load javascripts at bottom, this will reduce page load time) -->
    <!-- BEGIN CORE PLUGINS -->
    <!--[if lt IE 9]>
<script src="../../assets/global/plugins/respond.min.js"></script>
<script src="../../assets/global/plugins/excanvas.min.js"></script> 
<![endif]-->
    <script src="<%= ResolveUrl("~/assets/global/plugins/jquery.min.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveUrl("~/assets/global/plugins/jquery-migrate.min.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveUrl("~/assets/global/plugins/bootstrap/js/bootstrap.min.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveUrl("~/assets/global/plugins/jquery.blockui.min.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveUrl("~/assets/global/plugins/jquery.cokie.min.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveUrl("~/assets/global/plugins/uniform/jquery.uniform.min.js") %>" type="text/javascript"></script>
    <!-- END CORE PLUGINS -->
    <!-- BEGIN PAGE LEVEL PLUGINS -->
    <script src="<%= ResolveUrl("~/assets/global/plugins/jquery-validation/js/jquery.validate.min.js") %>" type="text/javascript"></script>
    <!-- END PAGE LEVEL PLUGINS -->
    <!-- BEGIN PAGE LEVEL SCRIPTS -->
    <script src="<%= ResolveUrl("~/assets/global/scripts/metronic.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveUrl("~/assets/admin/layout/scripts/layout.js") %>" type="text/javascript"></script>
    <!-- END PAGE LEVEL SCRIPTS -->
    <script>
        jQuery(document).ready(function () {
            Metronic.init(); // init metronic core components
            Layout.init(); // init current layout

        });
    </script>
    <!-- END JAVASCRIPTS -->
</body>
<!-- END BODY -->
</html>
