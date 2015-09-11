<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SearchRpt06.aspx.cs" Inherits="DPU.DORMITORY.View.Report.SearchRpt06" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server" id="Form1" method="POST" enctype="multipart/form-data" class="form-horizontal">
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
                                    <div class="input-group" style="text-align: left">
                                        <asp:DropDownList ID="ddlBuild" runat="server" class="select2_category form-control" DataTextField="NAME" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="ddlBuild_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">ห้อง:</label>
                                <div class="col-md-6">
                                    <div class="input-group" style="text-align: left">
                                        <asp:DropDownList ID="ddlRoom" runat="server" class="select2_category form-control" DataTextField="NUMBER" DataValueField="ID"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">เดือน/ปี:</label>
                                <div class="col-md-6">
                                    <div class="input-group input-medium date date-picker" data-date-format="mm/yyyy" data-date-viewmode="years" data-date-minviewmode="months">
                                        <asp:TextBox ID="txtDate" runat="server" class="form-control"></asp:TextBox>
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
                    <div class="row">
                        <div class="col-md-6">
                            <div class="row">
                                <asp:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True" ToolPanelWidth="200px" Height="100%" Width="100%" EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" HasCrystalLogo="False" HasDrillUpButton="False" HasPageNavigationButtons="False" HasSearchButton="False" ToolPanelView="None" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </form>
</asp:Content>
