<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebCam.aspx.cs" Inherits="СreditСonveyor.WebCam" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <script src="/js/webcam.js"></script>
      
    <style>
#my_camera{
 width: 320px;
 height: 240px;
 border: 1px solid black;
}
</style>
</head>
<body>
    <form id="form1" runat="server">
        <div>

             <div id="my_camera"></div>
<input type=button value="Сфотогорафировать" onclick="take_snapshot()">
        <asp:Button ID="btnSavePhoto" runat="server" Text="Сохранить" OnClick="btnSavePhoto_Click" />
        <br />
        <br />
        <asp:HiddenField ID="HiddenField1" runat="server" ClientIDMode="Static" />
        <div id="results">

     <%--<img id="imgCam" runat="server" src="'+data_uri+'"/>--%>
     <%--<asp:Image ID="Image1" runat="server" />--%>
       <%-- <asp:Literal ID="Literal1" runat="server"></asp:Literal>--%>
    <%--<asp:HiddenField ID="HiddenField1" runat="server" />--%>
    
</div>

    <asp:Literal ID="Literal1" runat="server" ClientIDMode="Static"></asp:Literal>

        </div>
    </form>
</body>
</html>


<!-- Code to handle taking the snapshot and displaying it locally -->
<script language="JavaScript">

 // Configure a few settings and attach camera
 Webcam.set({
  width: 320,
  height: 240,
  image_format: 'jpeg',
  jpeg_quality: 90
 });
 Webcam.attach( '#my_camera' );

 // preload shutter audio clip
 //var shutter = new Audio();
 //shutter.autoplay = true;
 //shutter.src = navigator.userAgent.match(/Firefox/) ? 'shutter.ogg' : 'shutter.mp3';

function take_snapshot() {
 // play sound effect
 //shutter.play();

 // take snapshot and get image data
 Webcam.snap( function(data_uri) {
 // display results in page
     document.getElementById('results').innerHTML = '<img id="imgCam" src="' + data_uri + '"/>';
     document.getElementById('HiddenField1').value = data_uri.replace(/^data\:image\/\w+\;base64\,/, '');
     
 } );
}
</script>