<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Dashboard.aspx.vb" Inherits="Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Dashboard
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://canvasjs.com/assets/script/canvasjs.min.js"></script>

    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>

    <style>
        
        .chart-legend ul {
            list-style: none;
            padding: 0;
        }

        .chart-legend li {
            margin-right: 40px;
            display: flex;
            align-items: center;
        }

        .chart-legend span {
            display: inline-block;
            width: 200px;
            height: 8px;
            margin-right: 10px;
        }

        #line-chart, #sales-line-chart {
            height: 250px !important;
            width: 100%;
        }

        .page-content {
            padding: 5px 20px 50px 20px
        }
    </style>




    <script>
        //$(document).ready(function () {

        //    var data = {
        //        labels: ["Food", "Drinks", "Beverage"],
        //        datasets: [{
        //            data: [30, 45, 25],
        //            backgroundColor: ["rgba(255, 99, 132, 1)", "rgba(54, 162, 235, 1)", "rgba(255, 206, 86, 1)"],
        //        }],
        //    };

        //    var ctxPieChart = document.getElementById("pie-chart").getContext("2d");

        //    var pieChart = new Chart(ctxPieChart, {
        //        type: "pie",
        //        data: data,
        //        options: {
        //            responsive: true,


        //        },

        //    });



        //});

        $(document).ready(function () {
            var ctxPieChart = document.getElementById("pie-chart").getContext("2d");

            var pieChart = new Chart(ctxPieChart, {
                type: "pie",
                data: pieChartData, 
                options: {
                    responsive: true,
                },
            });
        });

        //$(document).ready(function () {
        //    var data = {
        //        labels: ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"],
        //        datasets: [
        //            {
        //                label: "Food",
        //                data: [10, 15, 20, 18, 25, 12, 8],
        //                borderColor: "rgba(255, 99, 132, 1)",
        //                fill: false,
        //            },
        //            {
        //                label: "Drinks",
        //                data: [8, 12, 16, 14, 20, 10, 5],
        //                borderColor: "rgba(54, 162, 235, 1)",
        //                fill: false,
        //            },
        //            {
        //                label: "Beverage",
        //                data: [5, 10, 15, 12, 18, 7, 3],
        //                borderColor: "rgba(255, 206, 86, 1)",
        //                fill: false,
        //            },
        //            {
        //                label: "Flanerys",
        //                data: [6, 11, 14, 13, 17, 9, 4],
        //                borderColor: "green",
        //                borderDash: [5, 5],
        //                fill: false,
        //            },
        //            {
        //                label: "Falnnerys Outsdide",
        //                data: [7, 9, 12, 10, 15, 8, 5],
        //                borderColor: "black",
        //                borderDash: [10, 5],
        //                fill: false,
        //            },
        //        ],
        //    };

        //    var ctx = document.getElementById("line-chart").getContext("2d");

        //    var chart = new Chart(ctx, {
        //        type: "line",
        //        data: data,
        //        options: {
        //            responsive: true,
        //            scales: {
        //                x: {
        //                    title: {
        //                        display: true,
        //                        text: "Day",
        //                    },
        //                },
        //                y: {
        //                    title: {
        //                        display: true,
        //                        text: "Value",
        //                    },
        //                },
        //            },
        //            plugins: {
        //                legend: {
        //                    display: true,
        //                    labels: {
        //                        usePointStyle: true,
        //                        pointStyle: "line",
        //                    },
        //                },
        //            },
        //        },
        //    });
        //});

        $(document).ready(function () {
            $find("<%= radDuration.ClientID %>").add_selectedIndexChanged(function (sender, eventArgs) {
         fetchData();
     });


     $find("<%= radVenue.ClientID %>").add_selectedIndexChanged(function (sender, eventArgs) {
                fetchData();
            });

         
            fetchData();
           
            function fetchData() {
                //var date1 = new Date('2024-01-01');
                //var date2 = new Date('2024-01-07');
                var date1 = new Date();
                var date2 = new Date();
                var venue_id = $find("<%= radVenue.ClientID %>").get_value();  
         var duration = $find("<%= radDuration.ClientID %>").get_value();

         $.ajax({
             type: 'POST',
             url: 'Dashboard.aspx/GetDepartmentChartData',
             data: JSON.stringify({ venue_id: venue_id, date1: date1, date2: date2, duration: duration }),
             contentType: 'application/json; charset=utf-8',
             dataType: 'json',
             success: function (response) {

                 renderChart(response.d);

             },
             error: function (xhr, status, error) {


                 console.error(xhr.responseText);
             }
         });
     }

     function renderChart(chartData) {
         var labels = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
         var datasets = [];

         var departmentData = {};


         chartData.forEach(function (data) {

             if (!departmentData[data.label]) {
                 departmentData[data.label] = {
                     'Sunday': 0,
                     'Monday': 0,
                     'Tuesday': 0,
                     'Wednesday': 0,
                     'Thursday': 0,
                     'Friday': 0,
                     'Saturday': 0
                 };
             }
         });


         chartData.forEach(function (data) {
             departmentData[data.label][data.data[0]] += data.data[1];
         });


         Object.keys(departmentData).forEach(function (departmentName) {
             datasets.push({
                 label: departmentName,
                 data: Object.values(departmentData[departmentName])
             });
         });
         //chartData.forEach(function (data) {
         //    var values = [];
         //    labels.forEach(function (day) {
         //        var value = 0;
         //        data.forEach(function (entry) {
         //            if (entry.label === day) {
         //                value = entry.data[1];
         //            }
         //        });
         //        values.push(value);
         //    });

         //    datasets.push({
         //        label: data[0].label,
         //        data: values
         //    });
         //});
         //chartData.forEach(function (data) {
         //    labels.push(data.label);

         //    datasets.push({
         //        label: data.label,
         //        data: [data.data[1]] // Extracting only the value for the line chart
         //    });
         //});

         var ctx = document.getElementById('line-chart').getContext('2d');
         var chart = new Chart(ctx, {
             type: 'line',
             data: {
                 labels: labels,
                 datasets: datasets
             },
             options: {
                 responsive: true,
                 maintainAspectRatio: false,
                 scales: {
                     y: {
                         title: {
                             display: true,
                             text: 'Value'
                         }
                     },
                     x: {
                         title: {
                             display: true,
                             text: 'Day'
                         }
                     }
                 }
             }
         });
     }
 });

        //$(document).ready(function () {

        //    var salesData = {
        //        labels: ["Beluga Gold Martini", "Beef Sliders", "cheese board", "Dim Sum", "French Fries1", "Grilled Squid", "Pork Belly", "Service Charge", "Whisky Espresso Martini", "Beluga Gold Martini"],
        //        datasets: [{
        //            label: "Sales Amount",
        //            data: [1000, 1500, 800, 2000, 1200, 1800, 1600, 1400, 1300, 1100],
        //            lineColor: "rgba(75, 192, 192, 1)",
        //            fill: false,
        //        }],
        //    };

        //    var ctx = document.getElementById("sales-line-chart").getContext("2d");

        //    var chart = new Chart(ctx, {
        //        type: "line",
        //        data: salesData,
        //        options: {
        //            responsive: true,
        //            scales: {
        //                x: {
        //                    title: {
        //                        display: true,
        //                        text: "Items",
        //                    },
        //                },
        //                y: {
        //                    title: {
        //                        display: true,
        //                        text: "Sales Amount",

        //                    },
        //                },
        //            },
        //            plugins: {
        //                legend: {
        //                    display: true,
        //                    labels: {
        //                        usePointStyle: true,
        //                        pointStyle: "line",
        //                    },
        //                },
        //            },
        //        },
        //    });
        //});

        $(document).ready(function () {
            var ctx = document.getElementById("sales-line-chart").getContext("2d");

            var chart = new Chart(ctx, {
                type: "bar",  
                data: {
                    labels: salesLineChartData.labels,
                    datasets: [
                        {
                            label: 'Sales Amount',
                            data: salesLineChartData.datasets[0].data,
                            backgroundColor: 'rgba(54, 162, 235, 0.8)',
                            borderColor: 'rgba(0, 123, 255, 1)', 
                            borderWidth: 1
                        },
                        {
                            label: 'Sales Quantity',
                            data: salesLineChartData.datasets[1].data,
                            backgroundColor: 'rgba(255, 159, 64, 0.8)',
                            borderColor: 'rgba(255, 193, 7, 3)', 
                            borderWidth: 1
                        }
                    ]
                },
                options: {
                    responsive: true,
                    scales: {
                        x: {
                            title: {
                                display: true,
                                text: "Items",
                            },
                        },
                        y: {
                            title: {
                                display: true,
                                text: "Sales Quantity",
                            },
                        },
                    },
                    plugins: {
                        legend: {
                            display: true,
                            labels: {
                                usePointStyle: true,
                                pointStyle: "line",
                            },
                        },
                    },
                },
            });
        });


    </script>


    <div class="page-header pull-left">
        <div class="page-title">Dashboard</div>
    </div>
    <div class="page-header pull-left" style="float: right !important">
        <div class="page-title">
            <table border="0" width="100%" cellspacing="0">
                <tbody>
                    <tr>
                        <td>
                            <asp:Image ID="imgPhoto" ImageUrl="" runat="server" Height="10px" Width="10px" />&nbsp;<asp:Label runat="server" ID="lblstatus" Style="font-size: 10px;" Text="Sync Record"></asp:Label>
                        </td>
                        <td>&nbsp;
                            <asp:Button runat="server" ID="btnSendMail" Height="30px" Width="60px" CssClass="btn btn-primary" Style="font-size: 10px; text-align: left;" Text="Support" OnClick="btnSendMail_Click" Visible="false" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">




    <div class="clearfix"></div>
    <br />
    <div class="col-md-6">
        <div class="col-md-6 ">
            Duration : &nbsp;
                               <telerik:RadComboBox ID="radDuration" runat="server" AutoPostBack="true">
                                   <Items>
                                       <telerik:RadComboBoxItem Text="Today" Value="Today" />
                                       <telerik:RadComboBoxItem Text="This Week" Value="This Week" Selected="true" />
                                       <telerik:RadComboBoxItem Text="This Month" Value="This Month" />
                                       <telerik:RadComboBoxItem Text="This Year" Value="This Year" />
                                   </Items>
                               </telerik:RadComboBox>
        </div>
        <div class="col-md-6 ">
            Venue : &nbsp;&nbsp;&nbsp;
            <telerik:RadComboBox ID="radVenue" runat="server" AutoPostBack="true">
            </telerik:RadComboBox>
        </div>
    </div>
    <div class="clearfix"></div>
    <br />





    <div class="col-md-6">
        <div class="col-md-12" style="border-width: 1px; border-style: solid; height:300px;" >

            <telerik:RadHtmlChart runat="server" ID="tillchart" Transitions="true" Skin="Silk" Height="300px">
                <PlotArea>
                    <Series>
                        <telerik:ColumnSeries Name="Till Summmary Report" DataFieldY="NettTotal" Stacked="false">
                            <TooltipsAppearance DataFormatString="£{0:N}" />
                            <LabelsAppearance Visible="False">
                                <ClientTemplate>
                                #= kendo.format(dataItem.currency + \'{0:N}\', dataItem.NettTotal)#
                                </ClientTemplate>
                            </LabelsAppearance>
                        </telerik:ColumnSeries>
                    </Series>
                    <XAxis Color="black" Reversed="false">
                        <LabelsAppearance RotationAngle="90" />
                        <MajorGridLines Visible="false" />
                        <MinorGridLines Visible="false" />
                    </XAxis>
                    <YAxis AxisCrossingValue="0" Color="black"
                        Reversed="false">
                    </YAxis>
                </PlotArea>
                <Legend>
                    <Appearance Visible="false" Position="Bottom" />
                </Legend>
                <ChartTitle Text="Till Summary Report">
                </ChartTitle>
            </telerik:RadHtmlChart>
        </div>
    </div>
    <div class="col-md-2">
        <asp:RadioButtonList ID="rbtDisplayReport" runat="server" RepeatColumns="4" RepeatDirection="Horizontal" Style="float: left; margin-right: 10px" AutoPostBack="true" Visible="false">
            <asp:ListItem Text="Tills" Value="0" Selected="True"></asp:ListItem>
            <asp:ListItem Text="Location" Value="1"></asp:ListItem>
            <asp:ListItem Text="Venue" Value="2"></asp:ListItem>
            <asp:ListItem Text="Operater" Value="3"></asp:ListItem>
        </asp:RadioButtonList>
    </div>
    <div class="col-md-6">
        <div class="col-md-12" style="border-width: 1px; border-style: solid">
           <h3 style="text-align: center; font: 16px Arial,Helvetica,sans-serif; color:black;">Total Sales By Department(Day)</h3>
            <canvas id="line-chart" width="400" height="277.5px;"></canvas>
        </div>
    </div>


    <div class="clearfix"></div>
    <br />

    <div class="col-md-6" style="">
        <div class="col-md-12" style="border-width: 1px;border-style: solid ;">
             <h3 style="text-align: center; font: 16px Arial,Helvetica,sans-serif; color:black;">Total Sales By Department</h3>
           <div class="col-md-12" style="height: 300px; display: flex; justify-content: center; align-items: center;">
            <canvas id="pie-chart" ></canvas>
           </div>
        </div>
    </div>

    <div class="col-md-6">
        <div class="col-md-12" style="border-width: 1px; border-style: solid;  height: 350px;" ">
            <div class="sales-summary">
                <h3 style="text-align: center; font: 16px Arial,Helvetica,sans-serif; color:black;">Top 10 Selling Item - Sales Summary</h3>
                <table class="table">
                </table>
                <canvas id="sales-line-chart" font="font: 12px Arial,Helvetica,sans-serif; "  color="black" height="278.5px;"></canvas>
            </div>
        </div>
    </div>

</asp:Content>







