 nsp='Old browser!';
 dl=document.layers;
 oe=window.opera?1:0;
 da=document.all&&!oe;
 ge=document.getElementById;
 ws=window.sidebar?true:false;
 tN=navigator.userAgent.toLowerCase();
 izN=tN.indexOf('netscape')>=0?true:false;
 zis=tN.indexOf('msie 7')>=0?true:false;
 zis8=tN.indexOf('msie 8')>=0?true:false;
 zis|=zis8;
 if(ws&&!izN)
 {
   quogl='iuy'
 };
 var msg='';
 function nem()
 {
   return true
 };
 window.onerror = nem;
 zOF=window.location.protocol.indexOf("file")!=-1?true:false;
 i7f=zis&&!zOF?true:false;
 var download=[];
 var upload=[];
 var total=[];
 $(function()
 {
   var lines = data.split("\n");
   $.each(lines, function(lineNo, line)
   {
     var index = line.split(",");
     var dDate = index[0];
     var dMB = index[1]/1048576;
     var uMB = index[2]/1048576;
     var tMB = dMB+uMB;
     var points = dDate.split('-');
     var UTCDate = Date.UTC(points[2],(points[1]-1),points[0],points[3]);
     if (dMB=='null')
     {
       download.push([UTCDate,null]);
     }
     else
     {
       download.push([UTCDate,parseFloat(dMB.toFixed(2))]);
     }
     if (uMB=='null')
     {
       upload.push([UTCDate,null]);
     }
     else
     {
       upload.push([UTCDate,parseFloat(uMB.toFixed(2))]);
     }
     if (tMB=='null')
     {
       total.push([UTCDate,null]);
     }
     else
     {
       total.push([UTCDate,parseFloat(tMB.toFixed(2))]);
     }
   }
   );
   if(data.isOk == false)
   {
     alert("Error loading data file!");
   }
   else
   {
     createChart();
   }
 }
 );
 function createChart()
 {
   chart = new Highcharts.StockChart(
   {
     chart:
     {
       renderTo: 'container', zoomType: 'x'
     }
     , exporting:
     {
       width: 1000
     }
     , navigation:
     {
       buttonOptions:
       {
         align: 'right'
       }
     }
     , title:
     {
       text: 'Network Meter'
     }
     , subtitle:
     {
       text: 'Data Usage Chart'
     }
     , rangeSelector:
     {
       buttons: [
       {
         type: 'week', count: 1, text: '1w'
       }
       ,
       {
         type: 'month', count: 1, text: '1m'
       }
       ,
       {
         type: 'month', count: 6, text: '6m'
       }
       ,
       {
         type: 'year', count: 1, text: '1y'
       }
       ,
       {
         type: 'all', text: 'All'
       }
       ], selected: 1
     }
     , tooltip:
     {
       valueSuffix: ' MB',
     }
     , yAxis:
     {
       min: 0, labels:
       {
         formatter: function()
         {
           return this.value +' MB';
         }
       }
       , title:
       {
         text: 'Values'
       }
     }
     , series: [
     {
       name: 'Total', data: total, connectNulls: false, pointStart: Date.UTC(1970, 0, 1), pointInterval: 3600 * 1000
     }
     ,
     {
       name: 'Download', data: download, connectNulls: false, pointStart: Date.UTC(1970, 0, 1), pointInterval: 3600 * 1000
     }
     ,
     {
       name: 'Upload', data: upload, connectNulls: false, pointStart: Date.UTC(1970, 0, 1), pointInterval: 3600 * 1000
     }
     ]
   }
   );
 }
 