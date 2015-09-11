<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DPU.DORMITORY.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row">
        <div class="col-md-6 col-sm-6">
            <!-- BEGIN PORTLET-->
            <div class="portlet light ">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="icon-bar-chart font-green-sharp hide"></i>
                        <span class="caption-subject font-green-sharp bold uppercase">Site Visits</span>
                        <span class="caption-helper">weekly stats...</span>
                    </div>
                    <div class="actions">
                        <div class="btn-group btn-group-devided" data-toggle="buttons">
                            <label class="btn btn-transparent grey-salsa btn-circle btn-sm active">
                                <input type="radio" name="options" class="toggle" id="option1">New</label>
                            <label class="btn btn-transparent grey-salsa btn-circle btn-sm">
                                <input type="radio" name="options" class="toggle" id="option2">Returning</label>
                        </div>
                    </div>
                </div>
                <div class="portlet-body">
                    <div id="container1" style="min-width: 400px; height: 400px; margin: 0 auto"></div>

                    <%--                    <div id="site_statistics_loading">
                        <img src="../../assets/admin/layout/img/loading.gif" alt="loading" />
                    </div>
                    <div id="site_statistics_content" class="display-none">
                        <div id="site_statistics" class="chart">
                        </div>
                    </div>--%>
                </div>
            </div>
            <!-- END PORTLET-->
        </div>
    </div>



    <script src="<%= ResolveUrl("~/assets/global/plugins/jquery.min.js") %>" type="text/javascript"></script>


    <!-- END PAGE LEVEL SCRIPTS -->
    <script>
        jQuery(document).ready(function () {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "Services/DormDashboardService.asmx/FruitAnalysis",
                data: "{}",
                dataType: "json",
                success: function (Result) {
                    Result = Result.d;
                    var data = [];

                    for (var i in Result) {
                        var serie = new Array(Result[i].Name, Result[i].Value);
                        data.push(serie);
                    }

                    DreawChart(data);
                },
                error: function (Result) {
                    alert("Error");
                }
            });

        });

        function DreawChart(series) {

            chart = new Highcharts.Chart({
                chart: {
                    plotBackgroundColor: null,
                    plotBorderWidth: null,
                    plotShadow: false,
                    renderTo: 'container1',
                    //backgroundColor: {
                    //    linearGradient: [0, 0, 500, 500],
                    //    stops: [
                    //[0, 'rgb(255, 255, 255)'],
                    //[1, 'rgb(200, 200, 255)']
                    //    ]
                    //}
                },
                title: {
                    text: 'จำนวนห้องพัก'
                },
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: true,
                            format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                            style: {
                                color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                            }
                        }
                    }
                },
                series: [{
                    type: 'bar',
                    name: 'หอพัก',
                    data: series
                }]
            });
        }

    </script>
    <!-- END JAVASCRIPTS -->


</asp:Content>
