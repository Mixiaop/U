﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Result.aspx.cs" Inherits="U.Tests.Web.AsposeWords.Result" %>


<!DOCTYPE html>
<!--[if lt IE 7]> <html class="lt-ie9 lt-ie8 lt-ie7" lang="zh-CN"> <![endif]-->
<!--[if IE 7]>    <html class="lt-ie9 lt-ie8" lang="zh-CN"> <![endif]-->
<!--[if IE 8]>    <html class="lt-ie9" lang="zh-CN"> <![endif]-->
<!--[if gt IE 8]><!-->
<html lang="zh-CN">
<!--<![endif]-->
<head>

    <script src="http://192.168.1.165:14480/libs/jquery/jquery-1.9.min.js" type="text/javascript"></script>
    <script src="http://192.168.1.165:14480/libs/requirejs/highcharts/highcharts3.js" type="text/javascript"></script>


</head>
<body>



    <div id="resultColumn" class="mt10 mb50" style="width:840px;height: 420px;"></div>




    <script>
        $(function () {
            var result = "<%= StrResult%>"
            var list = result.split(',');
            $('#resultColumn').highcharts({
                chart: {
                    type: 'bar',
                    height: 420
                },
                title: {
                    text: ''
                },
                xAxis: {
                    categories: list,
                    title: {
                        text: null
                    }
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: '',
                        align: 'high'
                    },
                    labels: {
                        overflow: 'justify'
                    }
                },
                tooltip: {
                    valueSuffix: ' 分'
                },
                plotOptions: {
                    bar: {
                        pointWidth: 12,
                        dataLabels: {
                            enabled: true,
                            allowOverlap: true
                        }
                    }
                },
                legend: {
                    enabled: false
                },
                credits: {
                    enabled: false
                },
                series: [{
                    name: '得分',
                    color: '#518afe',
                    data: [30, 12, 25, 22, 31, 9, 24, 15]
                }]
            });
        })

    </script>

</body>
</html>
